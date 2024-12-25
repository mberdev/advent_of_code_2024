using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class PacmanGrid
    {
        public PacmanGrid(int width, int height)
        {
            Width = width;
            Height = height;
        }
        public int Width { get; }
        public int Height { get; }


        public Position Move(Position p , Vector v)
        {
            var moved = p.Add(v);
            moved.Modulo(Width, Height);
            return moved;

        }

        //public void Print()
        //{
        //    for (int y = 0; y < Height; y++)
        //    {
        //        for (int x = 0; x < Width; x++)
        //        {
        //            Console.Write(Grid[x, y]);
        //        }
        //        Console.WriteLine();
        //    }
        //}
    }
}
