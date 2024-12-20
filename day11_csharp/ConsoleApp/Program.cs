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
    var d = new OneStepMap();
    int BLINKS = 75;

    InitOneStep(stones, d, BLINKS);

    Console.WriteLine("Map built");
    //printMap(d);

    var counter = new Counter(d, 75);

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

static void InitOneStep(long[] stones, OneStepMap d, int BLINKS)
{
    //1st pass : stones from input
    foreach (long stone in stones)
    {
        d.GetStone(stone);
    }

    //BLINKS extra passes (each pass gets fed the previous pass result)
    for (int i = 0; i < BLINKS; i++)
    {
        var originalStones = d.Keys.ToList();
        foreach (var stone in originalStones)
        {
            var newStones = d.GetStone(stone);
            foreach (var newStone in newStones)
            {
                d.GetStone(newStone);
            }
        }
    }
}
