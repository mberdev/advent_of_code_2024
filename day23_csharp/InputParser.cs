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

        public static Dictionary<string, HashSet<string>> ParseInput(string[] lines)
        {
            Dictionary<string, HashSet<string>> parsed = new();
            foreach (var line in lines)
            {
                if(string.IsNullOrEmpty(line))
                {
                    continue;
                }

                var parts = line.Split("-");
                var key = parts[0];
                var value = parts[1];

                if (parsed.ContainsKey(key))
                {
                    parsed[key].Add(value);
                }
                else
                {
                    parsed.Add(key, new HashSet<string> { value });
                }
            }
            return parsed;
        }
    }
}
