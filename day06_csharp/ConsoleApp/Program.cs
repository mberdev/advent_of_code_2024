
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
var start = grid.Find('^');
if (start == null)
{
    throw new Exception("Start position not found.");
}
grid.ReplaceAll('^', '.');

// Control
//Console.WriteLine(data);


long knownBestScore = part2(grid, new PositionState(start, Direction.Up));
//part2(grid, start, end, knownBestScore);

Console.WriteLine("Done.");


static int part2(TextBasedGrid grid, PositionState start)
{
    int total = 0;
    bool PRINT = false;

    return total;

}



static void Print(bool print, string s)
{
    if (print)
    {
        Console.WriteLine(s);
    }
}
