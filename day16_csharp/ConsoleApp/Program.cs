
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



// Control
//Console.WriteLine(data);


long knownBestScore = part1(grid, start, end); // Needed for part2
part2(grid, start, end, knownBestScore);

Console.WriteLine("Done.");


static long part1(TextBasedGrid grid, Position start, Position end)
{
    bool PRINT = false;

    var grid2 = new LongBasedGrid(grid);
    grid2.ReplaceAll('#', LongBasedGrid.WALL);
    grid2.ReplaceAll('.', long.MaxValue);

    long score = exploreIterativePart1(grid2, new PositionState(start, Direction.Right), end, PRINT);

    Console.WriteLine($"Best score: {score}");

    return score;
}

static void part2(TextBasedGrid textGrid, Position start, Position end, long knownBestScore)
{
    bool PRINT = false;

    var grid = new RecordBasedGrid(textGrid, '#', long.MaxValue);


    var paths = exploreIterativePart2(grid, new PositionState(start, Direction.Right), end, PRINT, knownBestScore);

    grid.Print(17);

    foreach (var p in paths)
    {
        textGrid.SetAt(p, '0');
    }
    textGrid.Print();

    Console.WriteLine($"total unique positions on best paths: {paths.Count}");
}

static long exploreIterativePart1(LongBasedGrid grid, PositionState start, Position end, bool PRINT)
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


static HashSet<Position> exploreIterativePart2(RecordBasedGrid grid, PositionState start, Position end, bool PRINT,  long knownBestScore)
{
    HashSet<Position> paths = new();

    Stack<(PositionState current, long score)> stack = new();
    stack.Push((start, 0));

    while (stack.Count > 0)
    {
        var (current, score) = stack.Pop();

        long score2 = grid.GetAt(current.Position, current.Direction);

        // The current path is a worse path
        if (score2 < score)
        {
            Print(PRINT, "  Worse path");
            continue;
        }

        // The current path is a better or equal path
        grid.SetAt(current.Position, current.Direction, score);

        if (current.Position == end)
        {
            Print(PRINT, "  End at position " + current + " with score " + score);

            if (score == knownBestScore)
            {
                //Console.WriteLine("Will backtrack on grid:");
                //grid.Print(17);
                Console.WriteLine();

                var pathsForThisSolution = computeBacktrackPath(grid, start, current, score);

                foreach (var p in pathsForThisSolution)
                {
                    paths.Add(p);
                }
            }

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

            if (grid.Exists(candidate))
            {
                long orientedScore = score + turnCost;
                stack.Push((new PositionState(candidate, current.Direction), orientedScore + 1));
            }
            current = current.turnRight();
        }
    }

    return paths;
}



static void Print(bool print, string s)
{
    if (print)
    {
        Console.WriteLine(s);
    }
}

static HashSet<Position> computeBacktrackPath(RecordBasedGrid grid, PositionState start, PositionState end, long endScore)
{
    Stack<(PositionState current, long score)> stack = new();
    stack.Push((end, endScore));

    HashSet<Position> positions = new();


    //trace back path
    while (stack.Count > 0)
    {
        var (current, score) = stack.Pop();

        positions.Add(current.Position);

        if (current == start)
        {
            continue;
        }

        var candidates = new List<PositionState>();
        for (int origin = 0; origin <4; origin++)
        {
            PositionState candidate = current.Behind();
            current = current.turnLeft();

            if (grid.Exists(candidate.Position))
            {
                candidates.Add(candidate);
            }
        }

        foreach (var candidate in candidates)
        {
            for (int candidateEdge = 0; candidateEdge < 4; candidateEdge++)
            {
                int rotationsNeeded = (candidateEdge - (int)candidate.Direction + 4) % 4;
                int turnCost = rotationsNeeded switch
                {
                    0 => 0,
                    1 => 1000,
                    2 => 2000,
                    3 => 1000,
                    _ => throw new Exception("Invalid turn cost")
                };

                long edgeScore = grid.GetAt(candidate.Position, (Direction)candidateEdge);

                if (edgeScore + turnCost + 1 == score)
                {
                    stack.Push((candidate, edgeScore));
                }

            }


        }

    }

    return positions;
}