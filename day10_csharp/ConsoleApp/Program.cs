
using System.Collections.Immutable;
using ConsoleApp;

var lines = InputParser.ParseInput("./input_data/input.txt");

// Control
//foreach (var line in lines)
//{
//    Console.WriteLine(line);
//}

var grid = new IntBasedGrid(lines);

part1(grid);
//part2(grid);

Console.WriteLine("Done.");

static void part1(IntBasedGrid grid)
{
    var explorer = new Explorer(grid);

    System.Console.WriteLine("All trails scores sum: " + explorer.FindTrailsAndTheirScores());
}

static void part2(TextBasedGrid grid)
{

}

