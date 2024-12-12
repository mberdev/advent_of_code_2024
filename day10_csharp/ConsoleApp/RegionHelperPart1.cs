using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class RegionHelperPart1
    {
        public static int countHorizontalFences(Queue<Position> regionPlots)
        {
            int fencesCount = 0;

            var regionVerticalStripes = regionPlots.GroupBy(p => p.X).ToList();
            foreach (var verticalStripe in regionVerticalStripes)
            {
                if (verticalStripe.Count() == 0)
                {
                    throw new Exception("This should not happen.");
                }

                int x = verticalStripe.Key;
                var plotsSortedTopToBottom = verticalStripe.OrderBy(p => p.Y).ToList();

                bool isOutside = true;
                int startY = plotsSortedTopToBottom.First().Y - 1;
                int endY = plotsSortedTopToBottom.Last().Y + 1;

                for (int y = startY; y <= endY; y++)
                {
                    Position p = new Position(x, y);
                    if (plotsSortedTopToBottom.Contains(p))
                    {
                        if (isOutside)
                        {
                            isOutside = false;
                            fencesCount++;
                            //Console.WriteLine($"Found horizontal fence at {p}.");

                        }
                    }
                    else
                    {
                        if (!isOutside)
                        {
                            isOutside = true;
                            fencesCount++;
                            //Console.WriteLine($"Found horizontal fence at {p}.");
                        }
                    }
                }
            }
            return fencesCount;

        }

        public static int countVerticalFences(Queue<Position> regionPlots)
        {
            int fencesCount = 0;
            var regionHorizontalStripes = regionPlots.GroupBy(p => p.Y).ToList();
            foreach (var horizontalStripe in regionHorizontalStripes)
            {
                if (horizontalStripe.Count() == 0)
                {
                    throw new Exception("This should not happen.");
                }
                int y = horizontalStripe.Key;
                var plotsSortedLeftToRight = horizontalStripe.OrderBy(p => p.X).ToList();
                bool isOutside = true;
                int startX = plotsSortedLeftToRight.First().X - 1;
                int endX = plotsSortedLeftToRight.Last().X + 1;
                for (int x = startX; x <= endX; x++)
                {
                    Position p = new Position(x, y);
                    if (plotsSortedLeftToRight.Contains(p))
                    {
                        if (isOutside)
                        {
                            isOutside = false;
                            fencesCount++;
                            //Console.WriteLine($"Found vertical fence at {p}.");
                        }
                    }
                    else
                    {
                        if (!isOutside)
                        {
                            isOutside = true;
                            fencesCount++;
                            //Console.WriteLine($"Found vertical fence at {p}.");
                        }
                    }
                }
            }
            return fencesCount;
        }

    }
}
