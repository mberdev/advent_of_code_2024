using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Explorer
    {
        public IntBasedGrid Grid { get; }

        // Same data as a double-linked graph of nodes, but organized for speed.
        //public Dictionary<Position, HashSet<Position>> ancestorsMap { get; } = new();
        //public Dictionary<Position, HashSet<Position>> descendantsMap { get; } = new();

        public List<Position> Trailheads { get; }
        //public HashSet<Position> TrailEnds { get; } = new();

        public Explorer(IntBasedGrid grid)
        {
            this.Grid = grid;
            this.Trailheads = grid.FindAll(grid, 0);
        }

        public int FindTrailsAndTheirScores()
        {
            
            int allTrailsScores = 0;
            foreach (var trailHead in Trailheads)
            {
                HashSet<Position> found = new();

                //descendantsMap.Add(trailHead, new());
                int altitude = 0;
                ExploreNeighbours(trailHead, altitude, found);

                System.Console.WriteLine("    Trail score: " + found.Count + "("+string.Join(",", found.ToList())+")");
                System.Console.WriteLine("");
                System.Console.WriteLine("");

                allTrailsScores += found.Count;
            }

            System.Console.WriteLine("Trailheads count : " + Trailheads.Count);
            //System.Console.WriteLine("TrailEnds count : " + TrailEnds.Count);

            return allTrailsScores;
        }
        private void ExploreNeighbours(Position pos, int altitude, HashSet<Position> found)
        {
            System.Console.WriteLine(new string(' ', altitude * 4) + "Explore: " + pos + "  : "+altitude+"");

            if (altitude == 9)
            {
                found.Add(pos);

                //TrailEnds.Add(pos);

                System.Console.WriteLine(new string(' ', altitude*4) + "TRAIL END.");

                return;
            }

            if (!Grid.IsInGrid(pos))
            {
                return;
            }

            var neighbours = new List<Position>
            {
                new Position(pos.X, pos.Y - 1),
                new Position(pos.X, pos.Y + 1),
                new Position(pos.X - 1, pos.Y),
                new Position(pos.X + 1, pos.Y)
            }.Where(p => Grid.IsInGrid(p) && Grid.GetAt(p) == altitude + 1).ToList();

            //int neighboursSum = 0;

            foreach (var neighbour in neighbours)
            {
                //if (!descendantsMap.ContainsKey(pos))
                //{
                //    descendantsMap.Add(pos, new());
                //}
                //descendantsMap[pos].Add(neighbour);

                //if (!ancestorsMap.ContainsKey(neighbour))
                //{
                //    ancestorsMap.Add(neighbour, new());
                //}
                //ancestorsMap[neighbour].Add(pos);

                /*neighboursSum +=*/ ExploreNeighbours(neighbour, Grid.GetAt(neighbour), found);

            }

            //return neighboursSum;
        }


    }
}
