using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class InputParser
    {

        public static Dictionary<char, List<Position>> ParseInput(TextBasedGrid grid)
        {
            Dictionary<char, List<Position>> d = new();

            for (int y = 0; y < grid.Height; y++)
            {
                for (int x = 0; x < grid.Width; x++)
                {
                    var c = grid.GetAt(x, y);
                    if (c != '.')
                    {
                        if (!d.ContainsKey(c))
                        {
                            d[c] = new List<Position>();
                        }
                        d[c].Add(new Position(x, y));
                    }
                }
            }

            return d;
        }
    }
}
