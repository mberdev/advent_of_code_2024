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

var stones = InputParser.ParseInput(lines[0]);

// Control
Console.WriteLine(string.Join(",", stones));

part2(stones);

Console.WriteLine("Done.");

static void part2(long[] stones)
{
    int BLINKS = 75;

    var d = new OneStepMap();
    d.Build(stones, BLINKS);

    Console.WriteLine("Map built");
    //printMap(d);

    var counter = new Counter(d, BLINKS);

    long total = 0;
    foreach (var s in stones)
    {
        total += counter.GetCountForStone(s);
    }

    Console.WriteLine("Stones count: " + total);

}

static void Print(bool print, string s)
{
    if (print)
    {
        Console.WriteLine(s);
    }
}

