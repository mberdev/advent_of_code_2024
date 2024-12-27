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

        public static (List<LockOrKey>, List<LockOrKey>) ParseInput(string[] lines)
        {
            var grids = new List<TextBasedGrid>();
            var currentGridLines = new List<string>();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    if (currentGridLines.Count > 0)
                    {
                        grids.Add(new TextBasedGrid(currentGridLines.ToArray()));
                        currentGridLines.Clear();
                    }
                }
                else
                {
                    currentGridLines.Add(line);
                }
            }

            if (currentGridLines.Count > 0)
            {
                grids.Add(new TextBasedGrid(currentGridLines.ToArray()));
            }

            List<LockOrKey> keys = new();
            List<LockOrKey> locks = new();

            foreach (var grid in grids)
            {
                if(grid.GetAt(0,0) == '#')
                {
                    locks.Add(new LockOrKey(grid, true));
                }
                else if(grid.GetAt(0,0) == '.')
                {
                    keys.Add(new LockOrKey(grid, false));
                }
            }

            return (keys, locks);
        }
    }
}
