
using System.Collections.Immutable;
using ConsoleApp;

var lines = InputParser.ParseInput("./input_data/input.txt");

// Control
//foreach (var line in lines)
//{
//    Console.WriteLine(line);
//}

var grid = new TextBasedGrid(lines);

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
        regionFences += countHorizontalFences(regionPlots);
        regionFences += countVerticalFences(regionPlots);

        price += regionPlots.Count * regionFences;
        //Console.WriteLine($"Region at {p} has {regionPlots.Count} plots and {regionFences} fences.");

        //regions.Add(regionPlots);
    }
}

//Console.WriteLine($"There are {regions.Count} regions.");

Console.WriteLine($"The price is {price}.");

Console.WriteLine("Done.");

static int countHorizontalFences(Queue<Position> regionPlots)
{
    int fencesCount = 0;

    var regionVerticalStripes = regionPlots.GroupBy(p => p.X).ToList();
    foreach (var verticalStripe in regionVerticalStripes)
    {
        if (verticalStripe.Count() == 0)
        {
            throw new Exception("This should not happen.");
        }

        int x = verticalStripe.Key;
        var plotsSortedTopToBottom = verticalStripe.OrderBy(p => p.Y).ToList();

        bool isOutside = true;
        int startY = plotsSortedTopToBottom.First().Y - 1;
        int endY = plotsSortedTopToBottom.Last().Y + 1;

        for (int y = startY; y <= endY; y++)
        {
            Position p = new Position(x, y);
            if (plotsSortedTopToBottom.Contains(p))
            {
                if (isOutside)
                {
                    isOutside = false;
                    fencesCount++;
                    //Console.WriteLine($"Found horizontal fence at {p}.");

                }
            }
            else
            {
                if (!isOutside)
                {
                    isOutside = true;
                    fencesCount++;
                    //Console.WriteLine($"Found horizontal fence at {p}.");
                }
            }
        }
    }
    return fencesCount;

}

static int countVerticalFences(Queue<Position> regionPlots)
{
    int fencesCount = 0;
    var regionHorizontalStripes = regionPlots.GroupBy(p => p.Y).ToList();
    foreach (var horizontalStripe in regionHorizontalStripes)
    {
        if (horizontalStripe.Count() == 0)
        {
            throw new Exception("This should not happen.");
        }
        int y = horizontalStripe.Key;
        var plotsSortedLeftToRight = horizontalStripe.OrderBy(p => p.X).ToList();
        bool isOutside = true;
        int startX = plotsSortedLeftToRight.First().X - 1;
        int endX = plotsSortedLeftToRight.Last().X + 1;
        for (int x = startX; x <= endX; x++)
        {
            Position p = new Position(x, y);
            if (plotsSortedLeftToRight.Contains(p))
            {
                if (isOutside)
                {
                    isOutside = false;
                    fencesCount++;
                    //Console.WriteLine($"Found vertical fence at {p}.");
                }
            }
            else
            {
                if (!isOutside)
                {
                    isOutside = true;
                    fencesCount++;
                    //Console.WriteLine($"Found vertical fence at {p}.");
                }
            }
        }
    }
    return fencesCount;
}