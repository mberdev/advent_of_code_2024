
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




//part1(grid, robot, moves);

void part1()
{

}

Console.WriteLine("Done.");




