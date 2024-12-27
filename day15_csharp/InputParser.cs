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

        public static (TextBasedGrid, List<Direction>) ParseInput(string[] lines)
        {
            // all lines until first empty line
            var gridLines = new List<string>();
            var i = 0;
            while (i < lines.Length && !string.IsNullOrWhiteSpace(lines[i]))
            {
                gridLines.Add(lines[i]);
                i++;
            }
            var grid = new TextBasedGrid(gridLines.ToArray());

            //remaining lines
            var remainingLines = lines.Skip(i + 1).ToArray();
            List<Direction> directions = new();

            foreach (var line in remainingLines)
            {
                foreach (var c in line)
                {
                    switch(c)
                    {
                        case '^':
                            directions.Add(Direction.Up);
                            break;
                        case '>':
                            directions.Add(Direction.Right);
                            break;
                        case 'v':
                            directions.Add(Direction.Down);
                            break;
                        case '<':
                            directions.Add(Direction.Left);
                            break;
                        default:
                            throw new Exception($"Invalid character '{c}'");
                    }
                }
            }

            return (grid, directions);
        }
    }
}
