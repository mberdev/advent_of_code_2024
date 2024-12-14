
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
int[] data = Enumerable.Range(0, cdata.Length).ToArray().Select(i => (int)cdata[i]).ToArray();


// Control
//Console.WriteLine(data);


part1(data);
//part2(data);

Console.WriteLine("Done.");

static void part1(int[] data)
{
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
            int fileID = (sourcePos / 2) % 10;
            int size = data[sourcePos];
            int targetEnd = targetPos + size;
            while (targetPos < targetEnd)
            {
                checksum += targetPos * fileID;
                targetPos++;
            }

        }
        //gap to fill
        else
        {
            int gapSize = data[sourcePos];
            int targetEnd = targetPos + gapSize;
            while (targetPos < targetEnd)
            {
                if (buffer.Count == 0)
                {
                    //look at end of file
                    int lastFilePos = dataLength - 1;
                    //if file currently ends on a gap, remove it
                    if (lastFilePos % 2 == 1)
                    {
                        lastFilePos--;
                        dataLength--;
                    }
                    // unpack non-gap data into queue
                    int endFileID = (lastFilePos / 2) % 10;
                    int dataSize = data[lastFilePos];

                    for (int i = 0; i < dataSize; i++)
                    {
                        buffer.Enqueue(endFileID);
                    }
                    lastFilePos--;
                    dataLength--;
                }

                //take a value, put it into the gap
                int movedFileID = buffer.Dequeue();

                checksum += targetPos * movedFileID;
                targetPos++;
            }
        }

        sourcePos++;
    }
}

