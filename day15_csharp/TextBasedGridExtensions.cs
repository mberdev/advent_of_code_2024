using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace ConsoleApp;

public static class TextBasedGridExtensions
    {
    public static int Push(this TextBasedGrid grid, Position boxPushing, Direction d, bool doMove)
    {

        var objectToMove = grid.GetAt(boxPushing);

        if (objectToMove == '.')
        {
            return 1;
        }

        if (objectToMove == '#')
        {
            return 0;
        }

        Position moveWhere = boxPushing.RelativePosition(d);

        // there can be more than one object to move
        var boxes = new List<Position>() { boxPushing };
        if ((d == Direction.Up || d == Direction.Down) && (objectToMove == '[' || objectToMove == ']'))
        {
            Position box2; // The other side of the same box
            if (objectToMove == '[')
            {
                box2 = new Position(boxPushing.X + 1, boxPushing.Y);
            }
            else
            {
                box2 = new Position(boxPushing.X - 1, boxPushing.Y);
            }
            boxes.Add(box2);
        }

        bool canMoveAll = true;

        foreach (var box in boxes)
        {
            objectToMove = grid.GetAt(box);
            moveWhere = box.RelativePosition(d);
            var objectAtDestination = grid.GetAt(moveWhere);

            if (!grid.IsInGrid(moveWhere))
            {
                throw new Exception("How did you get past the walls all around the grid?");
            }

            // can push
            if (objectAtDestination == '.')
            {
                //nothing to do
            }

            // can't push because wall
            if (objectAtDestination == '#')
            {
                canMoveAll = false;
            }

            // can't push because box
            if (objectAtDestination == 'O' || objectAtDestination == '[' || objectAtDestination == ']')
            {
                var actualShift = Push(grid, moveWhere, d, doMove);
                if (actualShift == 0)
                {
                    canMoveAll = false;
                }
            }
        }

        if (!canMoveAll)
        {
            return 0;
        }
        else
        {
            if (doMove)
            {
                foreach (var box in boxes)
                {
                    objectToMove = grid.GetAt(box);
                    moveWhere = box.RelativePosition(d);
                    grid.SetAt(box, '.');
                    grid.SetAt(moveWhere, objectToMove);
                }
            }
            return 1;
        }

        // Defensive programming
        throw new Exception("Forgot case");
    }

    public static TextBasedGrid ScaleUp(this TextBasedGrid grid)
    {
        List<String> scaledLines = new();
        foreach (var line in grid.Lines)
        {
            var newLine = string.Join("", line.ToCharArray().Select(c =>
            {
                switch(c)
                {
                    case '.':
                        return "..";
                    case '#':
                        return "##";
                    case 'O':
                        return "[]";
                    case '@':
                        return "@.";
                    case '[':
                    case ']':
                        throw new Exception("Already scaled up");
                    default:
                        return c.ToString();
                }
            }));

            scaledLines.Add(newLine);
        }

        return new TextBasedGrid(scaledLines.ToArray());
    }
}