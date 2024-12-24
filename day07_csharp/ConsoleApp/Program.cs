
using System.Collections.Generic;
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

//var total = part1(grid, new PositionState(start, Direction.Up));
//Console.WriteLine($"Part 1: {total}");

//var total = part2(grid, new PositionState(start, Direction.Up));
//Console.WriteLine($"Part 2: {total}");

//Console.WriteLine("Done.");


//static int part1(TextBasedGrid grid, PositionState start)
//{

//}

//static int part2(TextBasedGrid grid, PositionState start)
//{

//}
