using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class LockOrKey
    {
        public TextBasedGrid Grid { get; private set; }
        public int[] Values { get; private set; }
        public bool IsLock { get; }

        public LockOrKey(TextBasedGrid grid, bool isLock) {
            Grid = grid;
            Values = Enumerable.Range(0, grid.Width).Select(x => {
                return Enumerable.Range(0, grid.Height).Where(y => grid.GetAt(x, y) != '.').Count() -1;
            }).ToArray();
            IsLock = isLock;
        }

        public void Print()
        {
            Grid.Print();
            var prefix = IsLock ? "Lock: " : "Key: ";
            Console.WriteLine($"{prefix}"+string.Join(",", Values));
            Console.WriteLine();
        }
    }
}
