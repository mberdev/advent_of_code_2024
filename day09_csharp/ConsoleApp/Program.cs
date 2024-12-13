
using System.Collections.Immutable;
using ConsoleApp;


var filePath = "./input_data/input.txt";
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
//Console.WriteLine(data);


part1(data);
//part2(data);

Console.WriteLine("Done.");

static void part1(Data data)
{

    Console.WriteLine("TOTAL TOKENS: " + totalTokensSpent);
}

