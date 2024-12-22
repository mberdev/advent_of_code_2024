
using System.Collections.Generic;
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

//var total = part1(grid, new PositionState(start, Direction.Up));
//Console.WriteLine($"Part 1: {total}");

var total = part2(grid, new PositionState(start, Direction.Up));
Console.WriteLine($"Part 2: {total}");

Console.WriteLine("Done.");


static int part1(TextBasedGrid grid, PositionState start)
{

    var path = explore1(grid, start);

    // remove duplicates (same position, different direction)
    int total = path.GroupBy(position => position.Position).Count();
    return total;
}

static int part2(TextBasedGrid grid, PositionState start)
{

    var obstaclesWhichCreateLoop = explore2(grid, start);

    // remove duplicates (same position, different direction)
    int loopsCount = obstaclesWhichCreateLoop.GroupBy(position => position.Position).Count();

    return loopsCount;
}




static Stack<PositionState> explore1(TextBasedGrid grid, PositionState start)
{
    Stack<PositionState> path = new();

    var current = start;
    path.Push(current);

    while (true)
    {
        path.Push(current);

        //Console.WriteLine($"Exploring {current}");

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
            path.Push(current);

            candidate = current.InFront();
        }

        
        current = candidate;
       
    }

    throw new Exception("No path found.");
}



static List<PositionState> explore2(TextBasedGrid grid, PositionState start)
{
    List<PositionState> obstaclesWhichCreateLoop = new();

    int progress = 0;

    //Stack<PositionState> path = new();
    OptimizedPath path = new();

    var current = start;

    while (true)
    {
        // For control
        if(progress%500 == 499)
        {
            Console.WriteLine("Progress: " + progress + "...");
        }

        path.Push(current);

        //Console.WriteLine($"Exploring {current}");

        PositionState nextMoveCandidate = current.InFront();

        if (!grid.IsInGrid(nextMoveCandidate.Position))
        {
            // Exited maze: finished
            return obstaclesWhichCreateLoop;
        }

        int attempt = 0;
        while(grid.IsObstacle(nextMoveCandidate.Position))
        {
            if(!grid.IsObstacle(nextMoveCandidate.Position))
            {
                break;
            }

            // Obstacle
            current = current.turnRight();
            path.Push(current);

            nextMoveCandidate = current.InFront();

            if (!grid.IsInGrid(nextMoveCandidate.Position))
            {
                // Exited maze: finished
                return obstaclesWhichCreateLoop;
            }

            attempt++;
            if (attempt == 4)
            {
                throw new Exception("Defensive programming.");
            }
        }

        //May not put obstacle on start position.
        if (nextMoveCandidate.Position == start.Position)
        {
            current = nextMoveCandidate;
            continue;
        }

        bool obstacleAlreadyTried = path.ContainsPosition(nextMoveCandidate.Position);

        if (!obstacleAlreadyTried)
        {
            // Found a valid next move. Pretend that there's an obstacle there.
            grid.SetAt(nextMoveCandidate.Position, '#');

            var detectedLoop = findLoop(grid, current, path);
            if (detectedLoop != null)
            {
                obstaclesWhichCreateLoop.Add(nextMoveCandidate);
            }

            // Revert imaginary obstacle.
            grid.SetAt(nextMoveCandidate.Position, '.');
        }

        current = nextMoveCandidate;

        progress++;
    }

    throw new Exception("No path found.");
}

static OptimizedPath? findLoop(TextBasedGrid grid, PositionState start, OptimizedPath path)
{
    var limit = 0;
    var current = start;

    OptimizedPath loopPath = new();

    while (limit < 10000)
    {
        loopPath.Push(current);

        //Console.WriteLine($"Exploring {current}");

        PositionState nextMoveCandidate = current.InFront();

        if (!grid.IsInGrid(nextMoveCandidate.Position))
        {
            // Exited maze: finished
            return null;
        }

        int attempt = 0;
        while(grid.IsObstacle(nextMoveCandidate.Position))
        {
            if (!grid.IsObstacle(nextMoveCandidate.Position))
            {
                break;
            }

            // Obstacle
            current = current.turnRight();

            if (path.Contains(current) || loopPath.Contains(current))
            {
                return loopPath;
            }

            loopPath.Push(current);

            nextMoveCandidate = current.InFront();

            if (!grid.IsInGrid(nextMoveCandidate.Position))
            {
                // Exited maze: finished
                return null;
            }

            attempt++;
            if (attempt == 4)
            {
                throw new Exception("Defensive programming.");
            }
        }

        if (path.Contains(current) || loopPath.Contains(nextMoveCandidate))
        {
            return loopPath;
        }

        current = nextMoveCandidate;
        limit++;

    }

    throw new Exception("Overflow in loop search");
}


//static void PrintPath(TextBasedGrid grid, OptimizedPath path, PositionState start)
//{
//    var grid2 = grid.Copy();

//    var p = path.Get();
//    var cStart = DirectionToChar(start);
//    grid2.SetAt(start.Position, cStart[0]);
//    foreach (var i in p.GroupBy(p=> p.Position))
//    {
//        if (i.Key == start.Position)
//        {
//            continue;
//        }

//        bool hasVertical = i.Any(p => p.Direction == Direction.Up || p.Direction == Direction.Down);
//        bool hasHorizontal = i.Any(p => p.Direction == Direction.Left || p.Direction == Direction.Right);
//        string c = "";
//        if (hasHorizontal && hasVertical)
//        {
//            c = "+";
//        } else if (hasHorizontal)
//        {
//            c = "-";
//        } else if (hasVertical)
//        {
//            c = "|";
//        } else
//        {
//            throw new Exception("Defensive programming.");
//        }
//        grid2.SetAt(i.Key, c[0]);
//    }

//    grid2.Print();
//}



//static string DirectionToChar(PositionState start)
//{
//    if (start.Direction == Direction.Up)
//    {
//        return "^";
//    }
//    if (start.Direction == Direction.Down)
//    {
//        return "v";
//    }
//    if (start.Direction == Direction.Left)
//    {
//        return "<";
//    }
//    if (start.Direction == Direction.Right)
//    {
//        return ">";
//    }
//    throw new Exception("Invalid direction");
//}


//static void Print(bool print, string s)
//{
//    if (print)
//    {
//        Console.WriteLine(s);
//    }
//}
