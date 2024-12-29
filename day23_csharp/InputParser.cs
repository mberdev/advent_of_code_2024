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

        public static Dictionary<string, string> ParseInput(string[] lines)
        {
            Dictionary<string, string> parsed = new();
            foreach (var line in lines)
            {
                if(string.IsNullOrEmpty(line))
                {
                    continue;
                }

                var parts = line.Split("-");
                var key = parts[0];
                var value = parts[1];
                //Console.WriteLine($"Key: {key}, Value: {value}");
                parsed.Add(key, value); 
            }
            return parsed;
        }
    }
}
