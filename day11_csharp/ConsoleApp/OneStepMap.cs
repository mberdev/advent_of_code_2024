namespace ConsoleApp
{
    public class OneStepMap : Dictionary<long, List<long>>
    {
        public void Build(long[] stones, int passes)
        {
            //1st pass : stones from input
            foreach (long stone in stones)
            {
                GetOrCreateStone(stone);
            }

            //BLINKS extra passes (each pass gets fed the previous pass result)
            for (int i = 0; i < passes; i++)
            {
                var originalStones = Keys.ToList();
                foreach (var stone in originalStones)
                {
                    var newStones = GetOrCreateStone(stone);
                    foreach (var newStone in newStones)
                    {
                        GetOrCreateStone(newStone);
                    }
                }
            }
        }

        public List<long> GetOrCreateStone(long stone)
        {
            if (!this.ContainsKey(stone))
            {
                var stones = ProcessStone(stone).ToList();
                this.Add(stone, stones);
            }

            return this[stone];
        }

        public static long[] ProcessStone(long stone)
        {
            if (stone == 0)
                return new long[] { 1 };

            if (hasEvenDigits(stone))
            {
                return splitInTwo(stone);
            }

            return new long[] { multiplyBy2024(stone) };
        }


        const long MULTIPLY_LIMIT = long.MaxValue / 2024;

        private static long multiplyBy2024(long stone)
        {
            if (stone > MULTIPLY_LIMIT)
            {
                throw new Exception("overflow");
            }

            return stone * 2024;
        }

        private static bool hasEvenDigits(long stone)
        {
            if (stone < 10)
                return false;
            if (stone < 100)
                return true;
            if (stone < 1000)
                return false;
            if (stone < 10000)
                return true;
            if (stone < 100000)
                return false;
            if (stone < 1000000)
                return true;
            if (stone < 10000000)
                return false;
            if (stone < 100000000)
                return true;
            if (stone < 1000000000)
                return false;
            if (stone < 10000000000)
                return true;
            if (stone < 100000000000)
                return false;
            if (stone < 1000000000000)
                return true;

            return stone.ToString().Length % 2 == 0;
        }

        private static long[] splitInTwo(long stone)
        {
            //if (stone < 100)
            //    return new long[] { stone / 10, stone-stone/10*10 };

            // TODO: optimized version, don't rely on string conversion
            string stoneStr = stone.ToString();
            int mid = stoneStr.Length / 2;
            string leftDigits = stoneStr.Substring(0, mid);
            string rightDigits = stoneStr.Substring(mid);

            return new long[] { long.Parse(leftDigits), long.Parse(rightDigits) };
        }

        void Print()
        {
            Console.WriteLine("Keys count:" + this.Keys.Count);
            foreach (var entry in this)
            {
                Console.WriteLine(entry.Key + " -> " + string.Join(",", entry.Value));
            }
        }
    }
}
