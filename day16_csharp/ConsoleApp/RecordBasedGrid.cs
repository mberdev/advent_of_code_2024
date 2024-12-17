

namespace ConsoleApp
{
    public class RecordBasedGrid
    {
        public FourScores?[,] Grid { get; private set; }
        public int Width => Grid.GetLength(0);
        public int Height => Grid.GetLength(1);

        //public RecordBasedGrid(string[] lines)
        //{
        //    this.Grid = new FourOriginsScore[lines[0].Length, lines.Length];
        //    for (int y = 0; y < lines.Length; y++)
        //    {
        //        for (int x = 0; x < lines[0].Length; x++)
        //        {
        //            if (!char.IsDigit(lines[y][x]))
        //            {
        //                this.Grid[x, y] = new FourOriginsScore(-1L, Direction.Up);
        //            }
        //            else
        //            {
        //                long value = long.Parse(lines[y][x].ToString());
        //                this.Grid[x, y] = new FourOriginsScore(value, Direction.Up);
        //            }
        //        }
        //    }
        //}

        public RecordBasedGrid(TextBasedGrid grid, char wall, long defaultValue)
        {
            this.Grid = new FourScores[grid.Width, grid.Height];
            for (int y = 0; y < grid.Height; y++)
            {
                for (int x = 0; x < grid.Width; x++)
                {
                    char c = grid.GetAt(x, y);
                    if (c != wall)
                    {
                        this.Grid[x, y] = new FourScores(defaultValue);
                    }
                }
            }
        }


        public bool Exists(Position p)
        {
            return GetAt(p) != null;
        }

        public bool IsInGrid(Position p)
        {
            return IsInGrid(p.X, p.Y);
        }


        public bool IsInGrid(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }


        public FourScores? GetAt(int x, int y)
        {
            if (!IsInGrid(x, y))
            {
                return null;
            }
            return Grid[x, y];
        }

        public FourScores? GetAt(Position position)
        {
            return GetAt(position.X, position.Y);
        }

        public long GetAt(Position position, Direction d)
        {
            var x = GetAt(position.X, position.Y);
            return x?.Scores[(int)d] ?? throw new Exception("null");
        }

        public void SetAt(int x, int y, FourScores value)
        {
            if (!IsInGrid(x, y))
            {
                return;
            }
            Grid[x, y] = value;
        }

        public void SetAt(Position position, FourScores value)
        {
            SetAt(position.X, position.Y, value);
        }

        public void SetAt(Position position, Direction d, long value)
        {
            var x = GetAt(position.X, position.Y);
            if (x == null)
            {
                throw new Exception("null");
            }

            x.SetScore(d, value);
        }


        //public void ReplaceAll(long value, long newValue)
        //{
        //    for (int y = 0; y < Height; y++)
        //    {
        //        for (int x = 0; x < Width; x++)
        //        {
        //            if (GetAt(x, y) != null && GetAt(x,y).Score == value)
        //            {
        //                SetAt(x, y, new FourScores(newValue, Direction.Up));
        //            }
        //        }
        //    }
        //}

        public void Print(int padding)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Console.Write((GetAt(x, y)?.ToString() ?? "#").PadLeft(padding));
                }

                Console.WriteLine();
            }
        }
    }
}
