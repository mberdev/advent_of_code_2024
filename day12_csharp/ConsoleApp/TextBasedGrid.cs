using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class TextBasedGrid
    {
        public TextBasedGrid(string[] lines)
        {
            this.Lines = lines;
        }
        public string[] Lines { get; private set; }
        public int Width => Lines[0].Length;
        public int Height => Lines.Length;

        public char GetAt(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
            {
                return '\0';
            }
            return Lines[y][x];
        }

        public char GetAt(Position position)
        {
            return GetAt(position.X, position.Y);
        }

        public void SetAt(int x, int y, char value)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
            {
                return;
            }
            var line = Lines[y].ToCharArray();
            line[x] = value;
            Lines[y] = new string(line);
        }

        public void SetAt(Position position, char value)
        {
            SetAt(position.X, position.Y, value);
        }

        public Queue<Position> floodFill(Position p, char newColor)
        {
            char colorToReplace = GetAt(p);

            if (colorToReplace == newColor)
                throw new Exception("Already floodfilled");

            Queue<Position> queue = new Queue<Position>();
            Queue<Position> filled = new Queue<Position>();
            queue.Enqueue(p);

            while (queue.Count > 0)
            {
                Position current = queue.Dequeue();
                if (GetAt(current) == colorToReplace)
                {
                    filled.Enqueue(current);
                    SetAt(current, newColor);

                    var positionsAround = new List<Position>
                    {
                        new Position(current.X + 1, current.Y),
                        new Position(current.X - 1, current.Y),
                        new Position(current.X, current.Y + 1),
                        new Position(current.X, current.Y - 1)
                    };

                    foreach (var direction in positionsAround)
                    {
                        if (GetAt(direction) == colorToReplace)
                        {
                            queue.Enqueue(direction);
                        }
                    }
                }
            }

            return filled;
        }
    }
}
