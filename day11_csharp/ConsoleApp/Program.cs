
using System.Collections.Generic;
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
    var d = new OneStep();
    int BLINKS = 75;

    InitOneStep(stones, d, BLINKS);

    Console.WriteLine("One-step Map built");
    //printMap(d);

    //int count = countStones(stones.ToList(), d, SMALLER_BLINK);
    //Console.WriteLine("Smaller count: " + count);

    //int STEPS = BLINKS / FifteenSteps.LIMIT_15;
    //if (STEPS != Counter.LIMIT_5)
    //{
    //    throw new Exception("steps mismatch");
    //}

    //var d15 = new FifteenSteps(d);
    //InitFifteenSteps(stones, STEPS, d15);

    //Console.WriteLine("Fiteen-steps Map built");

    var counter = new Counter(d, 75);

    long total = 0;
    foreach (var s in stones)
    {
        total += counter.GetCountForStone(s);
    }

    Console.WriteLine("Stones count: " + total);

}

//static int countStonesHelper(long stone, FifteenSteps d15, int depth, int limit)
//{
//    if (depth == limit)
//    {
//        return 1;
//    }

//    int subTotal = 0;
//    var stones = d.GetStone(stone);
//    foreach (var s in stones)
//    {
//        subTotal += countStonesHelper(s, d, depth + 1, limit);
//    }
//    return subTotal;
//}

//static int countStones(List<long> stones, FifteenSteps d15, int depthLimit)
//{
//    int total = 0;
//    foreach (var stone in stones)
//    {
//        total += countStonesHelper(stone, d, 0, depthLimit);
//    }
//    return total;
//}



//static void computeAhead(long stone, int fromWhere, int howDeep, Dictionary<long, List<long>>[] ahead)
//{
//    if (howDeep == 0)
//    {
//        return;
//    }

//    if (ahead[fromWhere] == null)
//    {
//        ahead[fromWhere] = new Dictionary<long, List<long>>();
//    }

//    if (ahead[fromWhere].ContainsKey(stone))
//    {
//        return;
//    }

//    var stones = ProcessStone(stone).ToList();
//    ahead[fromWhere].Add(stone, stones);

//    foreach (var newStone in stones)
//    {
//        computeAhead(newStone, fromWhere, howDeep - 1, ahead);
//    }
//}



static void Print(bool print, string s)
{
    if (print)
    {
        Console.WriteLine(s);
    }
}

static void InitOneStep(long[] stones, OneStep d, int BLINKS)
{
    //1st pass : stones from input
    foreach (long stone in stones)
    {
        d.GetStone(stone);
    }


    //2nd pass : stones from 1st pass
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

static void InitFifteenSteps(long[] stones, int STEPS, FifteenSteps d15)
{
    //1st pass : stones from input
    foreach (long stone in stones)
    {
        d15.GetStone(stone);
    }

    //2nd pass : stones from 1st pass
    for (int i = 0; i < STEPS; i++)
    {
        var originalStones = d15.Keys.ToList();
        foreach (var stone in originalStones)
        {
            var newStones = d15.GetStone(stone);
            foreach (var newStone in newStones)
            {
                d15.GetStone(newStone);
            }
        }
    }
}