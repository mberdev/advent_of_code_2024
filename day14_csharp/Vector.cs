using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Vector
    {
        public Vector(Position start, Position end)
        {
            X = end.X - start.X;
            Y = end.Y - start.Y;
        }

        public Vector(int x , int y)
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
}
