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
    }
}

