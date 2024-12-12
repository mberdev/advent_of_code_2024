using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{

    public class RegionHelperPart2
    {
        public static class Horizontal
        {
            public static int countHorizontalSidesInRegion(Queue<Position> regionPlots)
            {
                List<Position> outwardsFences = new List<Position>();
                List<Position> inwardsFences = new List<Position>();

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
                                outwardsFences.Add(p);
                                //Console.WriteLine($"Found horizontal fence at {p}.");

                            }
                        }
                        else
                        {
                            if (!isOutside)
                            {
                                isOutside = true;
                                //Console.WriteLine($"Found horizontal fence at {p}.");
                                inwardsFences.Add(p);
                            }
                        }
                    }
                }

                int fencesCount = 0;
                fencesCount += countHorizontalSidesFromFences(inwardsFences);
                fencesCount += countHorizontalSidesFromFences(outwardsFences);

                return fencesCount;

            }

            private static int countHorizontalSidesFromFences(List<Position> inwardsFences)
            {
                int inwardsSidesCount = 0;
                inwardsFences.GroupBy(p => p.Y).ToList().ForEach(group =>
                {
                    int y = group.Key;

                    int sidesCountForThisY = countContinuousHorizontalSections(group, y);

                    inwardsSidesCount += sidesCountForThisY;
                });

                return inwardsSidesCount;
            }

            private static int countContinuousHorizontalSections(IGrouping<int, Position> group, int y)
            {
                int sidesCountForThisY = 0;
                var fencesSortedByX = group.OrderBy(p => p.X).ToList();
                int startX = fencesSortedByX.First().X - 1;
                int endX = fencesSortedByX.Last().X + 1;
                bool touching = false;
                for (int x = startX; x <= endX; x++)
                {
                    Position p = new Position(x, y);
                    if (fencesSortedByX.Contains(p))
                    {
                        // Start of a continuous horizontal section of fence
                        if (!touching)
                        {
                            touching = true;
                            sidesCountForThisY++;
                        }
                    }
                    else
                    {
                        // End of a continuous horizontal section of fence
                        if (touching)
                        {
                            touching = false;
                        }
                    }
                }

                return sidesCountForThisY;
            }
        }

        public static class Vertical
        {
            public static int countVerticalSidesInRegion(Queue<Position> regionPlots)
            {
                List<Position> outwardsFences = new List<Position>();
                List<Position> inwardsFences = new List<Position>();
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
                                outwardsFences.Add(p);
                                //Console.WriteLine($"Found vertical fence at {p}.");
                            }
                        }
                        else
                        {
                            if (!isOutside)
                            {
                                isOutside = true;
                                inwardsFences.Add(p);
                                //Console.WriteLine($"Found vertical fence at {p}.");
                            }
                        }
                    }
                }
                int fencesCount = 0;
                fencesCount += countVerticalSidesFromFences(inwardsFences);
                fencesCount += countVerticalSidesFromFences(outwardsFences);
                return fencesCount;
            }
            private static int countVerticalSidesFromFences(List<Position> inwardsFences)
            {
                int inwardsSidesCount = 0;
                inwardsFences.GroupBy(p => p.X).ToList().ForEach(group =>
                {
                    int x = group.Key;
                    int sidesCountForThisX = countContinuousVerticalSections(group, x);
                    inwardsSidesCount += sidesCountForThisX;
                });
                return inwardsSidesCount;
            }

            private static int countContinuousVerticalSections(IGrouping<int, Position> group, int x)
            {
                int sidesCountForThisX = 0;
                var fencesSortedByY = group.OrderBy(p => p.Y).ToList();
                int startY = fencesSortedByY.First().Y - 1;
                int endY = fencesSortedByY.Last().Y + 1;
                bool touching = false;
                for (int y = startY; y <= endY; y++)
                {
                    Position p = new Position(x, y);
                    if (fencesSortedByY.Contains(p))
                    {
                        // Start of a continuous vertical section of fence
                        if (!touching)
                        {
                            touching = true;
                            sidesCountForThisX++;
                        }
                    }
                    else
                    {
                        // End of a continuous vertical section of fence
                        if (touching)
                        {
                            touching = false;
                        }
                    }
                }
                return sidesCountForThisX;
            }
        }
    }
}
