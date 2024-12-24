using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class OptimizedPath
    {
        private static int GetHash(int x, int y)
        {
            return x * 10000 + y;
        }

        public Dictionary<int, bool[]> d { get; init; } = new();

        public void Push(PositionState pos)
        {
            int hash = GetHash(pos.Position.X, pos.Position.Y);

            if (!d.TryGetValue(hash, out var h))
            {
                h = new bool[4];
                d[hash] = h;
            }

            h[(int)pos.Direction] = true;
        }

        public bool Contains(PositionState pos)
        {
            int hash = GetHash(pos.Position.X, pos.Position.Y);

            if (!d.TryGetValue(hash, out var h))
            {
                return false;
            }

            return h[(int)pos.Direction];
        }

        public bool ContainsPosition(Position pos)
        {

            int hash = GetHash(pos.X, pos.Y);
            return d.ContainsKey(hash);
        }

        //public OptimizedPath Copy()
        //{
        //    var d2 = new Dictionary<int, Dictionary<int, HashSet<int>>>();
        //    foreach (var i in this.d)
        //    {

        //    }
        //    return new OptimizedPath() { d = this.d.}
        //}

        public List<PositionState> Get()
        {
            var positions = new List<PositionState>();
            foreach (var entry in d)
            {
                int h = entry.Key;
                int x = h / 10000;
                int y = h % 10000;

                for (int i = 0; i < 4; i++)
                {
                    if (entry.Value[i])
                    {
                        positions.Add(new PositionState(x, y, (Direction)i));
                    }
                }
            }
            return positions;

        }
    }
}
