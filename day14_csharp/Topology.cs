using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Topology
    {
        public static (int, int)? FindHorizontalContinuousLine(List<int> xValues, int minLineLength, int minX, int maxX)
        {
            xValues.Sort();
            int currentLineLength = 1;
            int startX = xValues[0];
            for (int i = 1; i < xValues.Count; i++)
            {
                if (xValues[i] == startX + currentLineLength)
                {
                    currentLineLength++;
                    if (currentLineLength >= minLineLength)
                    {
                        return (startX, xValues[i]);
                    }
                }
                else
                {
                    currentLineLength = 1;
                    startX = xValues[i];
                }
            }
            return null;
        }
    }
}
