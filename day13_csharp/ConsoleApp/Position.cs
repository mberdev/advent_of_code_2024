using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public record Position
    {
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; }
        public int Y { get; }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }

    public record LongPosition
    {
        public LongPosition(long x, long y)
        {
            X = x;
            Y = y;
        }
        public long X { get; }
        public long Y { get; }
        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
