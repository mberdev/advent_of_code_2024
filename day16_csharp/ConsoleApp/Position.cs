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
    }

    public record LongPosition
    {
        public LongPosition(long x, long y)
        {
            X = x;
            Y = y;
        }
        public long X { get; }
        public long Y { get; }
        public override string ToString()
        {
            return $"({X}, {Y})";
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

    public record FourScores
    {
        public long[] Scores = new long[4];


        public FourScores(long defaultValue)
        {
            for (int i = 0; i < 4; i++)
            {
                Scores[i] = defaultValue;
            }
        }

        public void SetScore(Direction direction, long score)
        {
            Scores[(int)direction] = score;
        }

        public long GetScore(Direction direction)
        {
            return Scores[(int)direction];
        }


        public Direction oppositeDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    return Direction.Right;
                case Direction.Right:
                    return Direction.Left;
                case Direction.Up:
                    return Direction.Down;
                case Direction.Down:
                    return Direction.Up;
                default:
                    throw new Exception("");
            };
        }


        //public long ScoreWhenReoriented(Direction d)
        //{
        //    if(Score == long.MaxValue)
        //    {
        //        return long.MaxValue;
        //    }

        //    if(Score < 0)
        //    {
        //        throw new Exception("Should not be a wall");
        //    }

        //    int rotationsRequired = Math.Abs((int)d - (int)Direction);
        //    long rotateCost = rotationsRequired switch
        //    {
        //        0 => 0,
        //        1 => 1000,
        //        2 => 2000,
        //        3 => 1000,
        //        _ => throw new Exception("Invalid turn cost")
        //    };

        //    return Score + rotateCost;
        //}

        private string scoreToString(long score)
        {
            if (score == long.MaxValue)
                return "MAX";
            
            return score.ToString();
        }
        public override string ToString()
        {
            return $"{scoreToString(Scores[0])}/{scoreToString(Scores[1])}/{scoreToString(Scores[2])}/{scoreToString(Scores[3])}";
        }

    }
}
