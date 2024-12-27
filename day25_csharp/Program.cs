
using System.ComponentModel.DataAnnotations;
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

(var keys, var locks) = InputParser.ParseInput(lines);

part1(keys, locks);

void part1(List<LockOrKey> keys, List<LockOrKey> locks)
{
    int count = 0;

    foreach(var lok in locks) {
        foreach (var key in keys)
        {
            int i = 0;
            bool fits = true;
            while (i < 5)
            {
                if (lok.Values[i] + key.Values[i] > 5)
                {
                    fits = false;
                    break;
                }
                i++;
            }

            if(fits)
            {
                count++;
            }
        }
    }

    Console.WriteLine("Total fits:" + count);
}

Console.WriteLine("Done.");




