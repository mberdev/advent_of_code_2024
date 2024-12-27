
using ConsoleApp;



var filePath = "./input_data/test_input1.txt";
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

List<Position> boxes = new();
Position? box = grid.Find('O');
while (box != null)
{
    boxes.Add(box);
    grid.SetAt(box, '.');
    box = grid.Find('O');
}

part1(grid, robot, boxes, moves);

void part1(TextBasedGrid grid, Position robot, List<Position> boxes, List<Direction> moves)
{
    throw new NotImplementedException();
}

Console.WriteLine("Done.");




