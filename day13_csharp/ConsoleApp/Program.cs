
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
//foreach (var line in lines)
//{
//    Console.WriteLine(line);
//}

var grid = new IntBasedGrid(lines);

//part1(grid);
part2(grid);

Console.WriteLine("Done.");

static void part1(IntBasedGrid grid)
{
    new Explorer(grid).FindTrailsAndTheirScores();
}

static void part2(IntBasedGrid grid)
{
    new Explorer(grid).FindTrailsAndTheirScores();
}

