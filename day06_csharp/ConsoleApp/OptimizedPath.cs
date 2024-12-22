using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class OptimizedPath
    {

        public Dictionary<int, Dictionary<int, HashSet<Direction>>> d { get; init; } = new();

        public void Push(PositionState pos)
        {

            if (!d.TryGetValue(pos.Position.X, out var d2))
            {
                d.Add(pos.Position.X, new Dictionary<int, HashSet<Direction>>());
                d2 = d[pos.Position.X];
            }

            if (!d2.TryGetValue(pos.Position.Y, out var h))
            {
                d2.Add(pos.Position.Y, new HashSet<Direction>());
                h = d2[pos.Position.Y];
            }

            h.Add(pos.Direction);
        }

        public bool Contains(PositionState pos)
        {

            if (!d.TryGetValue(pos.Position.X, out var d2))
            {
                return false;
            }

            if (!d2.TryGetValue(pos.Position.Y, out var h))
            {
                return false;
            }

            return h.Contains(pos.Direction);
        }

        public bool ContainsPosition(Position pos)
        {

            if (!d.TryGetValue(pos.X, out var d2))
            {
                return false;
            }

            return d2.ContainsKey(pos.Y);
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
            var result = new List<PositionState>();
            foreach (var i in d)
            {
                foreach (var j in i.Value)
                {
                    foreach (var k in j.Value)
                    {
                        result.Add(new PositionState(i.Key, j.Key, k));
                    }
                }
            }
            return result;

        }
    }
}
