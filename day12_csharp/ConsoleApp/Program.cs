
using System.Collections.Immutable;
using ConsoleApp;

var lines = InputParser.ParseInput("./input_data/input.txt");

// Control
//foreach (var line in lines)
//{
//    Console.WriteLine(line);
//}

var grid = new TextBasedGrid(lines);

part1(grid);

Console.WriteLine("Done.");

static void part1(TextBasedGrid grid)
{

    //List<Queue<Position>> regions = new List<Queue<Position>>();

    int price = 0;

    for (int y = 0; y < grid.Height; y++)
    {
        for (int x = 0; x < grid.Width; x++)
        {
            Position p = new Position(x, y);
            if (grid.GetAt(p) == ' ')
            {
                continue;
            }

            var regionPlots = grid.floodFill(new Position(x, y), ' ');

            int regionFences = 0;
            regionFences += RegionHelperPart1.countHorizontalFences(regionPlots);
            regionFences += RegionHelperPart1.countVerticalFences(regionPlots);

            price += regionPlots.Count * regionFences;
            //Console.WriteLine($"Region at {p} has {regionPlots.Count} plots and {regionFences} fences.");

            //regions.Add(regionPlots);
        }
    }

    //Console.WriteLine($"There are {regions.Count} regions.");

    Console.WriteLine($"The price is {price}.");
}