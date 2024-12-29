
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

var numbers = InputParser.ParseInput(lines);

part1(numbers);

void part1(List<long> numbers)
{
    var steps = 2000;
    long total = 0;
    foreach (var number in numbers)
    {
        Console.WriteLine(number + ":");
        var secretNumber = number;
        for (var i = 0; i < steps; i++)
        {
            secretNumber = SecretNumber.Next(secretNumber);
            //Console.WriteLine("   (" + (i + 1) + ") " + secretNumber);
        }
        Console.WriteLine("   -> " + secretNumber);
        total += secretNumber;
    }

    Console.WriteLine();
    Console.WriteLine("Total: " + total);
}

Console.WriteLine("Done.");




