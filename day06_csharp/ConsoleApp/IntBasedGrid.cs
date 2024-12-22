
namespace ConsoleApp
{
    public class IntBasedGrid
    {
        public IntBasedGrid(string[] lines)
        {
            this.Grid = new int[lines[0].Length, lines.Length];
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[0].Length; x++)
                {
                    if (!char.IsDigit(lines[y][x]))
                    {
                        this.Grid[x, y] = -1;
                    } else
                    {
                        this.Grid[x, y] = int.Parse(lines[y][x].ToString()); this.Grid[x, y] = int.Parse(lines[y][x].ToString());
                    }
                }
            }
        }

        public IntBasedGrid(TextBasedGrid grid)
        {
            this.Grid = new int[grid.Width, grid.Height];
            for (int y = 0; y < grid.Height; y++)
            {
                for (int x = 0; x < grid.Width; x++)
                {
                    this.Grid[x, y] = (int)grid.GetAt(x, y);
                }
            }
        }

        public int[,] Grid { get; private set; }
        public int Width => Grid.GetLength(0);
        public int Height => Grid.GetLength(1);

        public bool IsInGrid(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        public bool IsInGrid(Position p)
        {
            return IsInGrid(p.X, p.Y);
        }

        public int GetAt(int x, int y)
        {
            if (!IsInGrid(x, y))
            {
                return -1;
            }
            return Grid[x, y];
        }

        public int GetAt(Position position)
        {
            return GetAt(position.X, position.Y);
        }

        public void SetAt(int x, int y, int value)
        {
            if(!IsInGrid(x, y))
            {
                return;
            }
            Grid[x, y] = value;
        }

        public void SetAt(Position position, int value)
        {
            SetAt(position.X, position.Y, value);
        }

        public List<Position> FindAll(IntBasedGrid grid, int value)
        {
            var trailHeads = new List<Position>();

            for (int y = 0; y < grid.Height; y++)
            {
                for (int x = 0; x < grid.Width; x++)
                {
                    if (grid.GetAt(x, y) == value)
                    {
                        trailHeads.Add(new Position(x, y));
                    }
                }
            }

            return trailHeads;
        }

        public void Replaceall(int value, int newValue)
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
    }
}
