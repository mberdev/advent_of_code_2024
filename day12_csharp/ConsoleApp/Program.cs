
using ConsoleApp;

var lines = InputParser.ParseInput("./input_data/test_input2.txt");

// Control
//foreach (var line in lines)
//{
//    Console.WriteLine(line);
//}

var grid = new TextBasedGrid(lines);

List<Queue<Position>> regions = new List<Queue<Position>>();

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
        Console.WriteLine($"Region at {p} has {regionPlots.Count} plots.");

        regions.Add(regionPlots);
    }
}

Console.WriteLine($"There are {regions.Count} regions.");

Console.WriteLine("Done.");
