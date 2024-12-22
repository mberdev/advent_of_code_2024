namespace ConsoleApp
{



    public static class LongBasedGridExtensions
    {
        public static bool IsWall(this LongBasedGrid grid, Position pos)
        {
            return grid.GetAt(pos) == LongBasedGrid.WALL;
        }

        public static bool IsVisited(this LongBasedGrid grid, Position pos)
        {
            return grid.GetAt(pos) >= 0;
        }
    }
}
