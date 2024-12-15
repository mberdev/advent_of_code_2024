
using System.Collections.Immutable;
using System.Security.Cryptography.X509Certificates;
using ConsoleApp;
using File = ConsoleApp.File;


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


char[] cdata = lines[0].ToCharArray();
int[] data = Enumerable.Range(0, cdata.Length).ToArray().Select(i => (int)(cdata[i]-'0')).ToArray();


// Control
//Console.WriteLine(data);


//part1(data);
part2(data);

Console.WriteLine("Done.");


static void part1(int[] data)
{
    bool PRINT_DEBUG = false;

    int LENGTH = data.Length;

    //algorithm needs odd number of compressed blocks
    if (LENGTH %2 != 1)
    {
        throw new Exception("Invalid input length");
    }

    long checksum = 0;
    int sourcePos = 0;
    int targetPos = 0;
    int dataLength = LENGTH;
    Queue<int> buffer = new();

    while (sourcePos < dataLength)
    {
        // non-gap
        if (sourcePos % 2 == 0)
        {
            int fileID = (sourcePos / 2) /* % 10 */;
            int size = data[sourcePos];
            int targetEnd = targetPos + size;

            if (size > 0) {
                Print(PRINT_DEBUG, "Block at " + sourcePos + " has size " + size + ". Will unpack value " + fileID + " into positions " + targetPos + "-" + (targetEnd - 1));
            }
            else {
                Print(PRINT_DEBUG, "Block at " + sourcePos + " has size " + size + ". Nothing to unpack.");
            }
            while (targetPos < targetEnd)
            {
                Print(PRINT_DEBUG, targetPos + "*" + fileID);
                long increment = targetPos * fileID;
                if (long.MaxValue - increment >= checksum)
                    Console.WriteLine("Overflow");
                checksum += increment;
                targetPos++;
            }

        }
        //gap to fill
        else
        {
            int gapSize = data[sourcePos];
            int targetEnd = targetPos + gapSize;
            if (gapSize > 0) {
                Print(PRINT_DEBUG, "Gap at " + sourcePos + " has size " + gapSize + ". Will fill positions " + targetPos + "-" + (targetEnd - 1));
            } else {
                Print(PRINT_DEBUG, "Gap at " + sourcePos + " has size " + gapSize + ". Nothing to fill.");
            }
            
            //fill the gap
            while (targetPos < targetEnd)
            {
                //we ran out of material to fill the gap. Let's refill.
                if (buffer.Count == 0)
                {
                    int endDataSize = 0;
                    while(endDataSize == 0) //In case the file ends on an empty block
                    {
                        //if file currently ends on a gap, remove it
                        if ((dataLength-1) % 2 == 1)
                        {
                            dataLength--;
                        }

                        //if we have reached the "middle" of the file.
                        if (dataLength<=sourcePos)
                        {
                            break;
                        }

                        // unpack non-gap data into queue
                        int endFileID = ((dataLength-1) / 2) /* % 10 */;
                        endDataSize = data[dataLength-1];

                        // Can be zero!
                        for (int i = 0; i < endDataSize; i++)
                        {
                            buffer.Enqueue(endFileID);
                        }
                        dataLength--;

                        //if we have reached the "middle" of the file.
                        if (dataLength <= sourcePos)
                        {
                            break;
                        }
                    }
                }

                //the buffer is empty because we have reached the "middle" of the file while trying to refill.
                if(buffer.Count == 0)
                {
                    break;
                }

                //take a value, put it into the gap
                int movedFileID = buffer.Dequeue();

                Print(PRINT_DEBUG, targetPos + "*" + movedFileID);

                checksum += targetPos * movedFileID;
                targetPos++;
            }
        }

        sourcePos++;
    }

    //If there's still some in the buffer, purge the remainder
    while (buffer.Count > 0)
    {
        int movedFileID = buffer.Dequeue();
        Print(PRINT_DEBUG, targetPos + "*" + movedFileID);
        checksum += targetPos * movedFileID;
        targetPos++;
    }


    Console.WriteLine("Checksum: " + checksum);
}

static void Print(bool print, string s)
{
    if (print)
    {
        Console.WriteLine(s);
    }
}

// For part 2 I got lazy and didn't care about a smart in-place solution.
// Immutable objects and duplication all the way, baby.
static void part2(int[] iData)
{
    List<Item> output = new();

    var input = InputParser.ParseInput(iData);

    var unpacked = Rearrange(input);

    //PrintUnpacked(unpacked);
    long checksum = 0;
    int blockPosition = 0;
    foreach(var item in unpacked)
    {
        if (item.IsGap)
        {
            blockPosition += item.Gap.Size;
            continue;
        }

        for (int i=0; i < item.File.Size; i++)
        {
            long increment = blockPosition * item.File.ID;
            if (long.MaxValue - increment <= checksum)
                Console.WriteLine("Overflow");
            checksum += increment;
            blockPosition++;
        }
    }

    Console.WriteLine("Checksum: " + checksum);

}

static void PrintUnpacked(List<Item> unpacked)
{
    foreach (var item in unpacked)
    {
        if (item.IsGap)
        {
            Console.Write(new string('.', item.Gap.Size));
        }
        else
        {
            char c = (char)((item.File.ID % 10) + '0');
            Console.Write(new string(c, item.File.Size));
        }
    }
    Console.WriteLine();
}

static List<Item> Rearrange(List<Item> input)
{
    //PrintUnpacked(input);

    //iterate right to left
    int processed = 0;
    while(processed < input.Count)
    {
        int i = input.Count - 1 - processed;
        var item = input[i];
        if (item.IsGap)
        {
            processed++;
            continue;
        }

        if (!item.IsFile)
            throw new Exception("Invalid input");

        if(item.File.Moved)
        {
            processed++;
            continue;
        }

        //find gap to fit this file (iterate left to right)
        for (int j = 0; j<i; j++)
        {
            var item2 = input[j];
            if(item2.IsGap && item2.Gap.Size >= item.File.Size)
            {
                int newGapSize = item2.Gap.Size - item.File.Size;

                //Console.WriteLine("Moving file " + item.File.ID + " of size " + item.File.Size + " from position " + i + " to position " + j + " into gap of size" + item2.Gap.Size + ".");
                input.RemoveAt(i);
                input.Insert(i, new Item(new Gap { Size = item.File.Size }));

                input.RemoveAt(j);
                input.Insert(j, item);
                item.File.Moved = true;

                if (newGapSize > 0)
                {
                    input.Insert(j + 1, new Item(new Gap() { Size = newGapSize }));
                }
                j = i; // exit loop
            }
        }

        processed++;
        //PrintUnpacked(input);
    }

    return input;
}

static ConsoleApp.File? FindAndRemoveFile(List<Item> items, int size)
{
    for (int i = items.Count - 1; i >= 0; i--)
    {
        var item = items[i];
        if(item.IsFile && item.File.Size == size)
        {
            items.RemoveAt(i);
            return item.File;
        }
    }
    return null;
}
