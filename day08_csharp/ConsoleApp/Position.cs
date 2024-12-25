using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public enum Direction
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    };


    public record Position
    {
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; }
        public int Y { get; }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        public Position Add(Vector v)
        {
            return new Position(X + v.X, Y + v.Y);
        }

        public Position Subtract(Vector v)
        {
            return new Position(X - v.X, Y - v.Y);
        }
    }



    public record PositionState
    {
        public PositionState(Position position, Direction direction)
        {
            Position = position;
            Direction = direction;
        }

        public PositionState(int x, int y, Direction direction)
        {
            Position = new Position(x, y);
            Direction = direction;
        }

        public Position Position { get; }
        public Direction Direction { get; }

        public override string ToString()
        {
            return $"{Position} {Direction}";
        }


        public Position InFront()
        {
            return Direction switch
            {
                Direction.Up => new Position(Position.X, Position.Y - 1),
                Direction.Down => new Position(Position.X, Position.Y + 1),
                Direction.Left => new Position(Position.X - 1, Position.Y),
                Direction.Right => new Position(Position.X + 1, Position.Y),
                _ => throw new Exception("Invalid direction")
            };
        }

        public PositionState Behind()
        {
            return Direction switch
            {
                Direction.Up => new PositionState(Position.X, Position.Y + 1, Direction),
                Direction.Down => new PositionState(Position.X, Position.Y - 1, Direction),
                Direction.Left => new PositionState(Position.X + 1, Position.Y, Direction),
                Direction.Right => new PositionState(Position.X - 1, Position.Y, Direction),
                _ => throw new Exception("Invalid direction")
            };
        }

        public PositionState turnRight()
        {
            return new PositionState(Position, (Direction)(((int)Direction + 1) % 4));
        }

        public PositionState turnLeft()
        {
            return new PositionState(Position, (Direction)(((int)Direction + 4 - 1) % 4));
        }
    }

}