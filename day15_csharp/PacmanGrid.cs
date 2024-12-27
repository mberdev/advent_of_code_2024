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

        public void Print(List<Position> allRobots, List<Position> robotsToHighlight)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (allRobots.Any(r => r.X == x && r.Y == y))
                    {
                        if (robotsToHighlight.Any(r => r.X == x && r.Y == y))
                        {
                            Console.Write("0");
                        }
                        else
                        {
                            Console.Write("#");
                        }
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
