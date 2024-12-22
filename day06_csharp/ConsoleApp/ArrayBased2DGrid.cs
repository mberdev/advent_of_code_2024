using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class ArrayBased2DGrid<V> where V:IEquatable<V>
    {
        public ArrayBased2DGrid(V[][] lines)
        {
            this.Lines = lines;
            this.Width = Lines[0].Length;
            this.Height = Lines.Length;    
        }

        public V[][] Lines { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public bool Is(Position pos, V value) => value.Equals(GetAt(pos));
        public bool IsNot(Position pos, V value) => !value.Equals(pos);
        public bool IsInGrid(int x, int y) => x >= 0 && x < Width && y >= 0 && y < Height;
        public bool IsInGrid(Position pos) => IsInGrid(pos.X, pos.Y);

        public V? GetAt(int x, int y)
        {
            if (!IsInGrid(x, y))
            {
                return default(V);
            }
            return Lines[y][x];
        }

        public V? GetAt(Position position)
        {
            return GetAt(position.X, position.Y);
        }

        public void SetAt(int x, int y, V value)
        {
            if (!IsInGrid(x, y))
            {
                return;
            }

            Lines[y][x] = value;
        }

        public void SetAt(Position position, V value)
        {
            SetAt(position.X, position.Y, value);
        }

        public Position? Find(V c)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (c.Equals(GetAt(x, y)))
                    {
                        return new Position(x, y);
                    }
                }
            }
            return null;
        }

        public void ReplaceAll(V c, V newValue)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (c.Equals(GetAt(x, y)))
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

