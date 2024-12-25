
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
var antennas = InputParser.ParseInput(grid);

foreach (var (key, value) in antennas)
{
    Console.WriteLine($"Antenna {key}: {string.Join(", ", value)}");
}
// Control
//Console.WriteLine(data);


//long knownBestScore = part1(grid, start, end); // Needed for part2
//part2(grid, start, end, knownBestScore);

Console.WriteLine("Done.");


//static long part1(TextBasedGrid grid, Position start, Position end)
//{
//}

//static void part2(TextBasedGrid textGrid, Position start, Position end, long knownBestScore)
//{
//}
