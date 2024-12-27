
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

part1(grid, robot, moves);

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

long Gps(Position p)
{
    return 100*p.Y+p.X;
}

Position Simulate(TextBasedGrid grid, Position robot, Direction move)
{
    Position targetPosition = robot.RelativePosition(move);

    if (!grid.IsInGrid(targetPosition))
    {
        throw new Exception("How did you get past the walls all around the grid?");
    }

    var objectAtTarget = grid.GetAt(targetPosition);

    // can move
    if (objectAtTarget == '.')
    {
        robot = targetPosition;
        return robot;
    }

    // can't move because wall
    if (objectAtTarget == '#')
    {
        return robot;
    }

    // can't move because box
    if (objectAtTarget == 'O')
    {
        int actualShift = grid.PushBox(targetPosition, move);
        if (actualShift == 0)
        {
            return robot;
        }
        robot = targetPosition;
        return robot;
    }

    // Defensive programming
    throw new Exception("Forgot case");
}

Console.WriteLine("Done.");




