
using System.ComponentModel.DataAnnotations;
using ConsoleApp;



var filePath = "./input_data/input.txt";
string[] lines;
if (System.IO.File.Exists(filePath))
{
    lines = System.IO.File.ReadAllLines(filePath);
}
else
{
    throw new FileNotFoundException("The specified file was not found.", filePath);
}


// Control
//Console.WriteLine(data);

var data = InputParser.ParseInput(lines);

part1(data);

void part1(Dictionary<string, Register> data)
{
    var toResolve = data.Keys.Where(k => k.StartsWith("z")).OrderBy(x => x).ToList();
    foreach (var key in toResolve)
    {
        Resolve(key, data);
        Console.WriteLine($"{key}: {data[key].Value}");
    }

    toResolve.Reverse();
    var binaryNumber = string.Join("", toResolve.Select(k => data[k].Value!.Value ? "1":"0").ToList());
    Console.WriteLine($"Number: {binaryNumber}");
    var decimalNumber = Convert.ToInt64(binaryNumber, 2);
    Console.WriteLine($"Decimal: {decimalNumber}");
}

void Resolve(string key, Dictionary<string, Register> data)
{
    if (data[key].Value != null)
        return;

    // Defensive programming
    if (data[key].Exp == null)
        throw new Exception();

    var left = data[key].Exp!.Left;
    Resolve(left, data);

    var right = data[key].Exp!.Right;
    Resolve(right, data);

    bool val1 = data[left].Value ?? throw new Exception();
    bool val2 = data[right].Value ?? throw new Exception();

    data[key].Value = data[key].Exp!.Op switch
    {
        OP.AND => val1 & val2,
        OP.OR => val1 | val2,
        OP.XOR => val1 ^ val2,
        _ => throw new Exception()
    };
}

Console.WriteLine("Done.");




