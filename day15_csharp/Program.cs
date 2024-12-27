
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


// Control
//Console.WriteLine(data);

(var grid, var moves) = InputParser.ParseInput(lines);

if(!filePath.Contains("test_input"))
{
    grid = grid.ScaleUp();
}

grid.Print();

var robot = grid.Find('@');
grid.SetAt(robot, '.');

//List<Position> boxes = new();
//Position? box = grid.Find('O');
//while (box != null)
//{
//    boxes.Add(box);
//    grid.SetAt(box, '.');
//    box = grid.Find('O');
//}

//part1(grid, robot, moves);
part2(grid, robot, moves);

void part1(TextBasedGrid grid, Position robot, List<Direction> moves)
{
    foreach (var move in moves)
    {
        robot = Simulate(grid, robot, move);
    }

    grid.SetAt(robot, '@');
    grid.Print();

    var boxes = grid.FindAll('O');
    var gpsSum = boxes.Select(b => Gps(b)).Sum();
    Console.WriteLine("Gps sum: " + gpsSum);
}

void part2(TextBasedGrid grid, Position robot, List<Direction> moves)
{
    grid.Print(robot, '@');
    Console.WriteLine();

    foreach (var move in moves)
    {
        //Console.WriteLine("Move: " + move);
        robot = Simulate(grid, robot, move);
        //grid.Print(robot, '@');
        //Console.WriteLine();
    }


    grid.Print(robot, '@');

    var boxes = grid.FindAll('[');
    var gpsSum = boxes.Select(b => Gps2(b, grid)).Sum();
    Console.WriteLine("Gps sum: " + gpsSum);
}

long Gps(Position p)
{
    return 100*p.Y+p.X;
}

long Gps2(Position p, TextBasedGrid grid)
{
    int distanceX = Math.Min(p.X, grid.Width - (p.X + 1));
    int distanceY = Math.Min(p.Y, grid.Height - (p.Y + 1));
    return 100 * p.Y + p.X;
}

Position Simulate(TextBasedGrid grid, Position robot, Direction move)
{
    Position targetPosition = robot.RelativePosition(move);

    //if (!grid.IsInGrid(targetPosition))
    //{
    //    throw new Exception("How did you get past the walls all around the grid?");
    //}

    var objectAtTarget = grid.GetAt(targetPosition);

    // try push
    int actualShift = grid.Push(targetPosition, move, false);
    if (actualShift == 0)
    {
        return robot;
    }

    // actual push
    grid.Push(targetPosition, move, true);
    robot = targetPosition;
    return robot;
}

Console.WriteLine("Done.");




