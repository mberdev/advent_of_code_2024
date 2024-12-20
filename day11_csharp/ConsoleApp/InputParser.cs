using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class InputParser
    {

        public static long[] ParseInput(string data)
        {
            return data.Split([' ', '\n', '\r'], StringSplitOptions.RemoveEmptyEntries)
                .Where(s => !string.IsNullOrEmpty(s))
                .Select(long.Parse)
                .ToArray();
        }
    }
}
