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

        public static List<(Position, Vector)> ParseInput(string[] lines)
        {
            List<(Position, Vector)> data = new();
            string pattern = @"p=(\d+),(\d+) v=(-?\d+),(-?\d+)";
            Regex regex = new(pattern);

            foreach (var line in lines)
            {
                Match match = regex.Match(line);
                if (match.Success)
                {
                    int x1 = int.Parse(match.Groups[1].Value);
                    int y1 = int.Parse(match.Groups[2].Value);
                    int x2 = int.Parse(match.Groups[3].Value);
                    int y2 = int.Parse(match.Groups[4].Value);

                    var position = new Position(x1, y1);
                    var vector = new Vector(x2, y2);
                    data.Add((position, vector));
                }
            }


            // Control
            //foreach (var item in data)
            //{
            //    Console.WriteLine($"{item.Item1} {item.Item2}");
            //}
            return data;
        }
    }
}
