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

var numbers = InputParser.ParseInput(lines);

//part1(numbers);
part2(numbers);

void part1(List<long> numbers)
{
    var steps = 2000;
    long total = 0;
    foreach (var number in numbers)
    {
        Console.WriteLine(number + ":");
        var secretNumber = number;
        for (var i = 0; i < steps; i++)
        {
            secretNumber = SecretNumber.Next(secretNumber);
            //Console.WriteLine("   (" + (i + 1) + ") " + secretNumber);
        }
        Console.WriteLine("   -> " + secretNumber);
        total += secretNumber;
    }

    Console.WriteLine();
    Console.WriteLine("Total: " + total);
}

void part2(List<long> buyersNumbers)
{
    var STEPS = 2000;
    int THRESHOLD = 8; // Assumption : the winning sequence probably has a high change value. 8 or higher.


    /////////////// FIRST PASS : FIND PROMISING SEQUENCES ///////////////

    HashSet<int> promisingSequences = new();
    List<Dictionary<int,int>> allPrices = new();

    //long total = 0;
    foreach (var firstNumber in buyersNumbers)
    {
        var price = Price(firstNumber);
        Dictionary<int, int> priceForSequence = new();
        priceForSequence.Add(-1, price);


        Queue<int> sequence = new();

        var secretNumber = firstNumber;
        for (var i = 0; i < STEPS; i++)
        {
            long nextNumber = SecretNumber.Next(secretNumber);
            var nextPrice = Price(nextNumber);
            var change = (int)(nextPrice - price);

            sequence.Enqueue(change);
            if (sequence.Count > 4)
            {
                sequence.Dequeue();
            }

            var sequenceHash = sequence.Count > 3 ? SequenceHash(sequence) : -1;
            //Console.WriteLine($"{nextNumber}: {nextPrice} ({change}), h:{sequenceHash}");

            if (nextPrice >= THRESHOLD && sequenceHash >= 0)
            {
                //Console.WriteLine("  Promising sequence: " + string.Join(",", sequence.ToList()) + $"({sequenceHash})");
                promisingSequences.Add(sequenceHash);
            }

            price = nextPrice;
            secretNumber = nextNumber;

            if (!priceForSequence.ContainsKey(sequenceHash))
            {
                priceForSequence.Add(sequenceHash, price);
            }

        }

        allPrices.Add(priceForSequence);
        //Console.WriteLine();
    }

    Console.WriteLine();
    Console.WriteLine("Promising sequences:" + promisingSequences.Count());


    /////////////// SECOND PASS : FIND ACTUAL BEST SEQUENCE ///////////////


    int bestTotal = 0;
    int bestSequence = -1;
    foreach (var sequence in promisingSequences)
    {
        int sequenceTotal = 0;
        for (int buyer = 0; buyer < allPrices.Count; buyer++)
        {
            var priceForSequence = allPrices[buyer];
            if(priceForSequence.ContainsKey(sequence))
            {
                sequenceTotal += priceForSequence[sequence];
            }
        }

        if (sequenceTotal > bestTotal)
        {
            Console.WriteLine("New best sequence: " + string.Join(",", ReverseHash(sequence)) + " with total " + sequenceTotal);
            bestTotal = sequenceTotal;
            bestSequence = sequence;
        }
    }

    Console.WriteLine("Best sequence: " + string.Join(",", ReverseHash(bestSequence)) + " with total " + bestTotal);
}

static int Price(long number)
{
    return (int)(number % 10);
}

static int SequenceHash(Queue<int> q)
{
    return q.Select((v, i) => (int)Math.Pow(100, i) * (v + 10)).Sum();
}

static List<int> ReverseHash(int hash)
{
    return Enumerable.Range(0, 4).Select(i => (hash / (int)Math.Pow(100, i)) % 100 - 10).ToList();
}
    Console.WriteLine("Done.");




