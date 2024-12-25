
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

var grid = new TextBasedGrid(lines);

// Control
//Console.WriteLine(data);


//part1(grid);
//part2(grid);

Console.WriteLine("Done.");


static void part1(TextBasedGrid grid)
{
}

static void part2(TextBasedGrid grid)
{
}

