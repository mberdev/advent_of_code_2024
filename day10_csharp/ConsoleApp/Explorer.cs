namespace ConsoleApp
{
    public class Explorer
    {
        public IntBasedGrid Grid { get; }

        public List<Position> Trailheads { get; }

        public Explorer(IntBasedGrid grid)
        {
            Grid = grid;
            Trailheads = grid.FindAll(grid, 0);
        }

        public void FindTrailsAndTheirScores()
        {
            int allTrailsScores = 0;
            int ratingsSum = 0;
            foreach (var trailHead in Trailheads)
            {
                HashSet<Position> found = new();

                int altitude = 0;
                int rating = ExploreNeighbours(trailHead, altitude, found);

                //System.Console.WriteLine("    Trail score: " + found.Count + "("+string.Join(",", found.ToList())+")");
                //System.Console.WriteLine("    Trail rating: " + rating);
                //System.Console.WriteLine("");
                //System.Console.WriteLine("");

                allTrailsScores += found.Count;
                ratingsSum += rating;
            }

            System.Console.WriteLine("Trailheads count : " + Trailheads.Count);
            System.Console.WriteLine("");
            System.Console.WriteLine("TOTAL RATING : " + ratingsSum);
            System.Console.WriteLine("TOTAL TRAILS SCORE : " + allTrailsScores);
        }
        private int ExploreNeighbours(Position pos, int altitude, HashSet<Position> found)
        {
            if (altitude == 9)
            {
                found.Add(pos);
                return 1; // rating
            }

            if (!Grid.IsInGrid(pos))
            {
                return 0; // rating
            }

            var neighbours = new List<Position>
            {
                new Position(pos.X, pos.Y - 1),
                new Position(pos.X, pos.Y + 1),
                new Position(pos.X - 1, pos.Y),
                new Position(pos.X + 1, pos.Y)
            }.Where(p => Grid.IsInGrid(p) && Grid.GetAt(p) == altitude + 1).ToList();

            int neighboursRatingsSum = 0;

            foreach (var neighbour in neighbours)
            {
                neighboursRatingsSum += ExploreNeighbours(neighbour, Grid.GetAt(neighbour), found);
            }

            return neighboursRatingsSum;
        }


    }
}
