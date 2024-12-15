using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class InputParser
    {

        public static List<Item> ParseInput(int[] data)
        {
            List<Item> output = new();

            for (int i = 0; i < data.Length; i++)
            {
                if (i % 2 == 0)
                {
                    File file = new File()
                    {
                        ID = i / 2,
                        Size = data[i]
                    };
                    output.Add(new Item(file));
                }
                else
                {
                    Gap gap = new Gap()
                    {
                        Size = data[i]
                    };
                    output.Add(new Item(gap));

                }
            }

            return output;
        }
    }
}
