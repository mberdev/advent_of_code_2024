

namespace ConsoleApp
{
    public class RecordBasedGrid
    {
        public const int WALL = -2;
        public const int DEADEND = -3;


        public OrientedScore[,] Grid { get; private set; }
        public int Width => Grid.GetLength(0);
        public int Height => Grid.GetLength(1);

        public RecordBasedGrid(string[] lines)
        {
            this.Grid = new OrientedScore[lines[0].Length, lines.Length];
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[0].Length; x++)
                {
                    if (!char.IsDigit(lines[y][x]))
                    {
                        this.Grid[x, y] = new OrientedScore(-1L, Direction.Up);
                    }
                    else
                    {
                        long value = long.Parse(lines[y][x].ToString());
                        this.Grid[x, y] = new OrientedScore(value, Direction.Up);
                    }
                }
            }
        }

        public RecordBasedGrid(TextBasedGrid grid)
        {
            this.Grid = new OrientedScore[grid.Width, grid.Height];
            for (int y = 0; y < grid.Height; y++)
            {
                for (int x = 0; x < grid.Width; x++)
                {
                    int value = (int)grid.GetAt(x, y);
                    this.Grid[x, y] = new OrientedScore(value, Direction.Up);
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

        public bool IsWall(Position p)
        {
            return IsWall(p.X, p.Y);
        }

        public bool IsWall(int x, int y)
        {
            var value = GetAt(x, y);
            if (value == null)
            {
                return true;
            }

            return value.Score == WALL;
        }

        public bool IsMax(Position p)
        {
            return IsWall(p.X, p.Y);
        }

        public bool IsMax(int x, int y)
        {
            var value = GetAt(x, y);
            if (value == null)
            {
                return false;
            }

            return value.Score == long.MaxValue;
        }

        public OrientedScore? GetAt(int x, int y)
        {
            if (!IsInGrid(x, y))
            {
                return null;
            }
            return Grid[x, y];
        }

        public OrientedScore? GetAt(Position position)
        {
            return GetAt(position.X, position.Y);
        }

        public void SetAt(int x, int y, OrientedScore value)
        {
            if (!IsInGrid(x, y))
            {
                return;
            }
            Grid[x, y] = value;
        }

        public void SetAt(Position position, OrientedScore value)
        {
            SetAt(position.X, position.Y, value);
        }


        public void ReplaceAll(long value, long newValue)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (GetAt(x, y) != null && GetAt(x,y).Score == value)
                    {
                        SetAt(x, y, new OrientedScore(newValue, Direction.Up));
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
                    OrientedScore value = GetAt(x, y);
                    if (value == null)
                    {
                        Console.Write(new string(' ', padding));
                    }
                    if (value.Score == WALL)
                    {
                        Console.Write("#".PadLeft(padding));
                    }
                    else if (value.Score == long.MaxValue)
                    {
                        Console.Write("MAX".PadLeft(padding));
                    }
                    else
                    {
                        Console.Write(GetAt(x, y).Score.ToString().PadLeft(padding));
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
