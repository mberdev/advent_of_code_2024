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

        public bool IsInGrid(Position position)
        {
            return position.X >= 0 && position.X < Width && position.Y >= 0 && position.Y < Height;
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

        public Position? Find(char c)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (GetAt(x, y) == c)
                    {
                        return new Position(x, y);
                    }
                }
            }
            return null;
        }

        public List<Position> FindAll(char c)
        {
            List<Position> result = new List<Position>();
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (GetAt(x, y) == c)
                    {
                        result.Add(new Position(x, y));
                    }
                }
            }
            return result;
        }


        public void ReplaceAll(char c, char newValue)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (GetAt(x, y) == c)
                    {
                        SetAt(x, y, newValue);
                    }
                }
            }
        }

        public void Print()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Console.Write(GetAt(x, y));
                }
                Console.WriteLine("");
            }
        }

        public void Print(Position specialPosition, char temporaryValue)
        {
            char oldValue = GetAt(specialPosition);
            SetAt(specialPosition, temporaryValue);
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Console.Write(GetAt(x, y));
                }
                Console.WriteLine("");
            }
            SetAt(specialPosition, oldValue);
        }
    }
}

