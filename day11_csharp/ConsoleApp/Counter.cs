using System.Text.Json.Serialization.Metadata;

namespace ConsoleApp
{
    public class Counter
    {
        private OneStepMap d1;
        private int blinksTarget;

        private Dictionary<long, long>[] countPerDepth;

        public Counter(OneStepMap d1, int blinksTarget)
        {
            this.d1 = d1;
            this.blinksTarget = blinksTarget;
            countPerDepth = new Dictionary<long, long>[this.blinksTarget];

            for (int i = 0; i < blinksTarget; i++)
            {
                countPerDepth[i] = new Dictionary<long, long>();
            }
        }

        public long GetCountForStone(long stone)
        {
            return countStonesHelper(stone, 0);
        }

        private long countStonesHelper(long stone, int depth)
        {
            if (depth == blinksTarget)
            {
                return 1;
            }

            var stones = d1[stone];

            if (depth < 2)
            {
                Console.WriteLine($"{new string(' ', depth * 2)}stone: " + stone + " ("+stones.Count+")");
            }

            var d = countPerDepth[depth];
            if (!d.ContainsKey(stone))
            {
                long total = 0;
                foreach (var s in stones)
                {
                    total += countStonesHelper(s, depth + 1);
                }
                d.Add(stone, total);

            }

            return d[stone];
        }
    }
}
