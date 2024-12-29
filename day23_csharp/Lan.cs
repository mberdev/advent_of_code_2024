using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{


    public class NSet : SortedSet<string>
    {
        public override string ToString()
        {
            return string.Join(",", this);
        }

        public bool hasStartsWithT()
        {
            return this.Any(x => x.StartsWith("t"));
        }


        public override bool Equals(object? obj)
        {
            if (obj is SortedSet<string> otherSet)
            {
                return this.SetEquals(otherSet);
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 0;
            foreach (var item in this)
            {
                hash ^= item?.GetHashCode() ?? 0;
            }
            return hash;
        }
    }


    public class Lan
    {
        public Dictionary<string, HashSet<string>> d1 { get; private set; }



        public Lan(Dictionary<string, HashSet<string>> d)
        {
            d1 = d;


            //optimize
            foreach (var k in d1.Keys)
            {
                foreach (var v in d1[k])
                {
                    if (d1.ContainsKey(v))
                    {
                        d1[v].Add(k);
                    }
                    else
                    {
                        d1.Add(v, new HashSet<string> { k });
                    }
                }
            }

            //Control
            //foreach (var k in d1.Keys)
            //{
            //    Console.WriteLine($"List of connected devices to {k}: {string.Join(", ", d1[k])}");
            //}
        }

        public bool ConnectionExists(string s1, string s2)
        {
            return d1.ContainsKey(s1) && d1[s1].Contains(s2);
        }

        //public HashSet<string> ListConnected(string s)
        //{
        //    var explored = new HashSet<string>();
        //    var h = new HashSet<string>() { s };
        //    ListConnectedHelper(h, explored, s);

        //    return h;
        //}

        //private void ListConnectedHelper(HashSet<string> h, HashSet<string> explored, string current)
        //{
        //    if (explored.Contains(current))
        //    {
        //        return;
        //    }

        //    explored.Add(current);

        //    foreach (var neighbour in d1[current])
        //    {
        //        h.Add(neighbour);
        //        ListConnectedHelper(h, explored, neighbour);
        //    }
        //}

        public HashSet<NSet> FindTriplets(string s)
        {
            var result = new HashSet<NSet>();
            var first = s;
            foreach (var second in d1[s])
            {
                var candidateThirds = d1[second].Except(new List<string>() { s });
                foreach (var third in candidateThirds)
                {
                    if (d1[third].Contains(first))
                    {
                        result.Add(new NSet() { first, second, third });
                    }
                }
            }
            return result;
        }

        public NSet FindNSet(string s)
        {
            var result = new NSet() { s };
            var explored = new SortedSet<string>();

            FindNSets(result, explored, s);

            return result;
        }



        private void FindNSets(NSet h, SortedSet<string> explored, string current)
        {
            if (explored.Contains(current))
            {
                return;
            }

            explored.Add(current);

            var connections = d1[current];
            if (h.Any(inSet => inSet != current && !connections.Contains(inSet)))
                return;

            h.Add(current);
            foreach(var connection in connections)
            {
                FindNSets(h, explored, connection);
            }
        }
    }
}
