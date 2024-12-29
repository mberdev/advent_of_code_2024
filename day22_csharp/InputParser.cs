using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class InputParser
    {

        public static List<long> ParseInput(string[] lines)
        {
            var data = new List<long>();
            foreach (var line in lines)
            {
                if(string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                data.Add(long.Parse(line));
            }
            return data;
        }
    }
}
