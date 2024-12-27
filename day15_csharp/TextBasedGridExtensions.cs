using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public static class TextBasedGridExtensions
    {
        public static int PushBox(this TextBasedGrid grid, Position box, Direction d)
        {
            Position targetPosition = box.RelativePosition(d);

            if(!grid.IsInGrid(targetPosition))
            {
                throw new Exception("How did you get past the walls all around the grid?");
            }

            var objectAtTarget = grid.GetAt(targetPosition);

            // can push
            if (objectAtTarget == '.')
            {
                grid.SetAt(box, '.');
                grid.SetAt(targetPosition, 'O');

                return 1;
            }

            // can't push because wall
            if (objectAtTarget == '#')
            {
                return 0;
            }

            // can't push because box
            if (objectAtTarget == 'O')
            {
                int actualShift = PushBox(grid, targetPosition, d);
                if (actualShift == 0)
                {
                    return 0;
                }

                grid.SetAt(box, '.');
                grid.SetAt(targetPosition, 'O');
                return 1;
            }

            // Defensive programming
            throw new Exception("Forgot case");
        }
    }
}
