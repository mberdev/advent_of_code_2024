
using System.Collections.Generic;
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

var data = InputParser.ParseInput(lines);


// Control
//Console.WriteLine(data);

//part1(data);
part2(data);

void part1(List<(long, List<long>)> data)
{
    long calibration = 0;
    foreach (var item in data)
    {
        var testValue = item.Item1;
        var equationOperands = item.Item2;

        //Control
        //System.Console.WriteLine($"{testValue} : {string.Join(", ", equationÖperands)}");
        var operators = Solve(testValue, equationOperands);

        if (operators != null)
        {
            System.Console.WriteLine($"{testValue} can be solved with {string.Join(",", operators)}");
            calibration += testValue;
        }
        else
        {
            System.Console.WriteLine($"   X {testValue}");
        }
    }
    Console.WriteLine($"Total: {calibration}");

}

void part2(List<(long, List<long>)> data)
{
    long calibration = 0;
    foreach (var item in data)
    {
        var testValue = item.Item1;
        var equationOperands = item.Item2;

        //Control
        //System.Console.WriteLine($"{testValue} : {string.Join(", ", equationÖperands)}");
        var operators = Solve(testValue, equationOperands);

        if (operators != null)
        {
            System.Console.WriteLine($"{testValue} can be solved with {string.Join(",", operators)}");
            calibration += testValue;
        }
        else
        {
            System.Console.WriteLine($"   X {testValue}");
        }
    }
    Console.WriteLine($"Total: {calibration}");

}

Stack<char>? Solve(long testValue, List<long> operands)
{
    return TryStep(testValue, operands, null);

}

Stack<char>? TryStep(long value, List<long> operands, char? oprator)
{
    Stack<char>? operators;

    var operand = operands.Last();

    if (operands.Count == 1)
    {
        if (oprator == '*' && operand == value)
        {
            operators = new Stack<char>();
            operators.Push((char)oprator!);
            return operators;
        }
        else if (oprator == '+' && operand == value)
        {
            operators = new Stack<char>();
            operators.Push((char)oprator!);
            return operators;
        }
        
        return null;
    }

    var operandsClone = new List<long>(operands);
    operandsClone.RemoveAt(operandsClone.Count - 1); // We don't use Stack because cloning a stack reverts it (!) and I have no time for this kind of bs.

    if (value % operand == 0)
    {
        operators = TryStep(value / operand, operandsClone, '*');
        if (operators != null)
        {
            if (oprator != null)
            {
                operators.Push((char)oprator!);
            }
            return operators;
        }
    }

    operators = TryStep(value - operand, operandsClone, '+');
    if (operators != null)
    {
        if (oprator != null)
        {
            operators.Push((char)oprator!);
        }
        return operators;
    }

    return null;
}



//static int part2(TextBasedGrid grid, PositionState start)
//{

//}
