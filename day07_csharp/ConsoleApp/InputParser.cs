using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class InputParser
    {

        public static List<(long, List<long>)> ParseInput(string[] data)
        {
            var result = new List<(long, List<long>)>();
            foreach (var line in data)
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                var parts = line.Split(':');
                if (parts.Length != 2)
                {
                    throw new Exception($"Invalid input: {line}");
                }
                long beforeColon = long.Parse(parts[0]);
                List<long> afterColon = parts[1].Split(' ').Where(s => !string.IsNullOrEmpty(s)).Select(long.Parse).ToList();
                result.Add((beforeColon, afterColon));
            
            }
            return result;
        }
    }
}
