
using System.Collections.Immutable;
using ConsoleApp;


var filePath = "./input_data/test_input1.txt";
string[] lines;
if (File.Exists(filePath))
{
    lines = File.ReadAllLines(filePath);
}
else
{
    throw new FileNotFoundException("The specified file was not found.", filePath);
}

var data = InputParser.ParseInput(lines);

// Control
//foreach (var line in lines)
//{
//    Console.WriteLine(line);
//}

part1();
//part2(grid);

Console.WriteLine("Done.");

static void part1()
{
}

static void part2()
{
}

