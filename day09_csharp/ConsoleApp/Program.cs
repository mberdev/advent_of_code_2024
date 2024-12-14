
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


char[] cdata = lines[0].ToCharArray();
int[] data = Enumerable.Range(0, cdata.Length).ToArray().Select(i => (int)(cdata[i]-'0')).ToArray();


// Control
//Console.WriteLine(data);


part1(data);
//part2(data);

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
                checksum += targetPos * fileID;
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

