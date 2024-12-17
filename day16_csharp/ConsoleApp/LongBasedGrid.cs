

namespace ConsoleApp
{
    public class LongBasedGrid
    {
        public const int WALL = -2;
        public const int DEADEND = -3;


        public long[,] Grid { get; private set; }
        public int Width => Grid.GetLength(0);
        public int Height => Grid.GetLength(1);

        public LongBasedGrid(string[] lines)
        {
            this.Grid = new long[lines[0].Length, lines.Length];
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[0].Length; x++)
                {
                    if (!char.IsDigit(lines[y][x]))
                    {
                        this.Grid[x, y] = -1L;
                    }
                    else
                    {
                        this.Grid[x, y] = long.Parse(lines[y][x].ToString()); this.Grid[x, y] = long.Parse(lines[y][x].ToString());
                    }
                }
            }
        }

        public LongBasedGrid(TextBasedGrid grid)
        {
            this.Grid = new long[grid.Width, grid.Height];
            for (int y = 0; y < grid.Height; y++)
            {
                for (int x = 0; x < grid.Width; x++)
                {
                    this.Grid[x, y] = (int)grid.GetAt(x, y);
                }
            }
        }


        public bool IsInGrid(Position p)
        {
            return IsInGrid(p.X, p.Y);
        }

        public bool IsInGrid(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        public long GetAt(int x, int y)
        {
            if (!IsInGrid(x, y))
            {
                return -1;
            }
            return Grid[x, y];
        }

        public long GetAt(Position position)
        {
            return GetAt(position.X, position.Y);
        }

        public void SetAt(int x, int y, long value)
        {
            if (!IsInGrid(x, y))
            {
                return;
            }
            Grid[x, y] = value;
        }

        public void SetAt(Position position, long value)
        {
            SetAt(position.X, position.Y, value);
        }


        public void ReplaceAll(long value, long newValue)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (GetAt(x, y) == value)
                    {
                        SetAt(x, y, newValue);
                    }
                }
            }
        }

        public void Print(int padding)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    long value = GetAt(x, y);
                    if (value == WALL)
                    {
                        Console.Write("#".PadLeft(padding));
                    }
                    else if (value == long.MaxValue)
                    {
                        Console.Write(new string( ' ', padding));
                    }
                    else
                    {
                        Console.Write(GetAt(x, y).ToString().PadLeft(padding));
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
