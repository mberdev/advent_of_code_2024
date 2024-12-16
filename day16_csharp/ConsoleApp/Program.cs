
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
var start = grid.Find('S');
if (start == null)
{
    throw new Exception("Start position not found.");
}
grid.ReplaceAll('S', '.');

var end = grid.Find('E');
if (end == null)
{
    throw new Exception("End position not found.");
}
grid.ReplaceAll('E', '.');


var grid2 = new LongBasedGrid(grid);
grid2.ReplaceAll('#', LongBasedGrid.WALL);
grid2.ReplaceAll('.', long.MaxValue);


// Control
//Console.WriteLine(data);


part1(grid2, start, end);
//part2(data);

Console.WriteLine("Done.");


static void part1(LongBasedGrid grid, Position start, Position end)
{
    bool PRINT = false;

    long score = exploreIterative(grid, new PositionState(start, Direction.Right), end, PRINT);

    Console.WriteLine($"Score: {score}");
}

static long exploreIterative(LongBasedGrid grid, PositionState start, Position end, bool PRINT)
{
    Stack<(PositionState current, long score)> stack = new();
    stack.Push((start, 0));
    long betterScore = long.MaxValue;

    while (stack.Count > 0)
    {
        var (current, score) = stack.Pop();
        Print(PRINT, "Exploring: " + current);

        if (!grid.IsInGrid(current.Position))
        {
            Print(PRINT, "  Out of grid");
            continue;
        }

        if (grid.IsWall(current.Position))
        {
            Print(PRINT, "  Wall");
            continue;
        }

        long score2 = grid.GetAt(current.Position);

        // The current path is a worse path
        if (score2 <= score)
        {
            Print(PRINT, "  Worse path");
            continue;
        }

        // The current path is a better path
        grid.SetAt(current.Position, score);

        if (current.Position == end)
        {
            Print(PRINT, "  End at position " + current + " with score " + score);
            betterScore = Math.Min(betterScore, score);
            continue;
        }

        for (int i = 0; i < 4; i++)
        {
            int turnCost = i switch
            {
                0 => 0,
                1 => 1000,
                2 => 2000,
                3 => 1000,
                _ => throw new Exception("Invalid turn cost")
            };
            Position candidate = current.InFront();

            Print(PRINT, "  Trying to move to " + candidate);

            if (!grid.IsWall(candidate) && grid.IsInGrid(candidate))
            {
                stack.Push((new PositionState(candidate, current.Direction), score + 1 + turnCost));
            }
            current = current.turnRight();
        }
    }

    return betterScore;
}

static void Print(bool print, string s)
{
    if (print)
    {
        Console.WriteLine(s);
    }
}

static void part2(int[] iData)
{
}