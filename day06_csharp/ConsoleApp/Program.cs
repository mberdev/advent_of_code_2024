
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

var grid = new TextBasedGrid(lines);
var start = grid.Find('^');
if (start == null)
{
    throw new Exception("Start position not found.");
}
grid.ReplaceAll('^', '.');


// Control
//Console.WriteLine(data);


var total = part1(grid, new PositionState(start, Direction.Up));
Console.WriteLine($"Part 1: {total}");

//part2(grid, start, end, knownBestScore);



Console.WriteLine("Done.");


static int part1(TextBasedGrid grid, PositionState start)
{
    bool PRINT = false;

    var path = explore(grid, start);

    int total = path.Distinct().ToList().Count;
    return total;
}


static Stack<Position> explore(TextBasedGrid grid, PositionState start)
{
    Queue<PositionState> toExplore = new();
    Stack<Position> path = new();

    toExplore.Enqueue(start);
    path.Push(start.Position);

    while (toExplore.Count > 0)
    {
        var current = toExplore.Dequeue();
        path.Push(current.Position);

        Console.WriteLine($"Exploring {current}");

        PositionState candidate = current.InFront();

        while (!grid.IsValid(candidate.Position))
        {
            if (!grid.IsInGrid(candidate.Position))
            {
                // Exited maze: finished
                return path;
            }

            // Obstacle
            current = current.turnRight();
            path.Push(current.Position);

            candidate = current.InFront();
        }

        
        toExplore.Enqueue(candidate);
       
    }

    throw new Exception("No path found.");
}


static void Print(bool print, string s)
{
    if (print)
    {
        Console.WriteLine(s);
    }
}
