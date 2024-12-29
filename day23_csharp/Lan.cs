using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{


    public record Triplet
    {
        public Triplet(HashSet<string> values)
        {
            if(values.Count != 3)
            {
                throw new ArgumentException("Triplet must contain exactly 3 values.");
            }

            var sortedValues = values.OrderBy(x => x).ToList();
            First = sortedValues[0];
            Second = sortedValues[1];
            Third = sortedValues[2];
        }

        public string First { get; }
        public string Second { get; }
        public string Third { get; }

        public override string ToString()
        {
            return $"{First},{Second},{Third}";
        }

        public bool hasStartsWithT()
        {
            return First.StartsWith("t") || Second.StartsWith("t") || Third.StartsWith("t");
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

        public HashSet<Triplet> FindTriplets(string s)
        {
            var result = new HashSet<Triplet>();
            var first = s;
            foreach (var second in d1[s])
            {
                var candidateThirds = d1[second].Except(new List<string>() { s });
                foreach (var third in candidateThirds)
                {
                    if (d1[third].Contains(first))
                    {
                        var items = new HashSet<string>() { first, second, third };
                        result.Add(new Triplet(items));
                    }
                }
            }
            return result;
        }
    }
}
