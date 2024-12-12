
using System.Collections.Immutable;
using ConsoleApp;

var lines = InputParser.ParseInput("./input_data/test_input.txt");

// Control
//foreach (var line in lines)
//{
//    Console.WriteLine(line);
//}

var grid = new TextBasedGrid(lines);

//part1(grid);
part2(grid);

Console.WriteLine("Done.");

static void part1(TextBasedGrid grid)
{

}


static void part2(TextBasedGrid grid)
{

    }

    //Console.WriteLine($"There are {regions.Count} regions.");
}