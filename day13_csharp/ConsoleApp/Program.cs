
using System.Collections.Immutable;
using ConsoleApp;


var filePath = "./input_data/input.txt";
string[] lines;
if (File.Exists(filePath))
{
    lines = File.ReadAllLines(filePath);
}
else
{
    throw new FileNotFoundException("The specified file was not found.", filePath);
}

var data = InputParser.ParseInput(lines);

// Control
//Console.WriteLine(data);


//part1(data);
part2(data);

Console.WriteLine("Done.");

static void part1(Data data)
{
    long totalTokensSpent = 0;

    foreach (var machine in data.Machines)
    {
        //Console.WriteLine(machine);
        var buttonA = machine.Buttons[0];
        var buttonB = machine.Buttons[1];
        var target = machine.Prize.p;

        var solution = EquationSolver2.Solve(
            buttonA.X,
            buttonB.X,
            buttonA.Y,
            buttonB.Y,
            target.X,
            target.Y
        );


        //Console.Write($"Solution : ");
        Console.WriteLine(solution != null ? solution : "no solution");

        if (solution != null)
        {
            long A = solution.Item1;
            long B = solution.Item2;
            long tokensSpent = A * 3 + B * 1;
            //Console.WriteLine($"tokens: {tokensSpent}");
            totalTokensSpent += tokensSpent;
        }

        //Console.WriteLine($"");

    }

    Console.WriteLine("TOTAL TOKENS: "+totalTokensSpent);
}

static void part2(Data data)
{
    long totalTokensSpent = 0;

    foreach (var machine in data.Machines)
    {
        //Console.WriteLine(machine);
        var buttonA = machine.Buttons[0];
        var buttonB = machine.Buttons[1];
        var target = machine.Prize.p;

        long OFFSET = 10000000000000;
        var correctedTarget = new LongPosition(target.X+OFFSET, target.Y+OFFSET);
        var solution = EquationSolver2.Solve(
            buttonA.X,
            buttonB.X,
            buttonA.Y,
            buttonB.Y,
            correctedTarget.X,
            correctedTarget.Y
        );


        //Console.Write($"Solution : ");
        Console.WriteLine(solution != null ? solution : "no solution");

        if (solution != null)
        {
            long A = solution.Item1;
            long B = solution.Item2;
            long tokensSpent = A * 3 + B * 1;
            //Console.WriteLine($"tokens: {tokensSpent}");
            totalTokensSpent += tokensSpent;
        }

        //Console.WriteLine($"");

    }

    Console.WriteLine("TOTAL TOKENS: " + totalTokensSpent);
}

