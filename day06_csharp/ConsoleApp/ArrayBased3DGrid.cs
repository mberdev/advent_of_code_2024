//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ConsoleApp
//{
//    public class ArrayBased3DGrid<T, V> where V:IEquatable<V>
//    {
//        public ArrayBased3DGrid(Dictionary<T, V>?[][] lines)
//        {
//            this.Lines = lines;
//            this.Width = Lines[0].Length;
//            this.Height = Lines.Length;    
//        }

//        public Dictionary<T,V>?[][] Lines { get; private set; }
//        public int Width { get; private set; }
//        public int Height { get; private set; }

//        public bool Is(Position pos,  T d, V value) => value.Equals(GetAt(pos));
//        public bool IsNot(Position pos, T value) => !value.Equals(pos);
//        public bool IsInGrid(int x, int y) => x >= 0 && x < Width && y >= 0 && y < Height;
//        public bool IsInGrid(Position pos) => IsInGrid(pos.X, pos.Y);

//        private Dictionary<T, V>? GridGetAt(int x, int y)
//        {
//            if (!IsInGrid(x, y))
//            {
//                return null;
//            }
//            return Lines[y][x];

//        }

//        public Dictionary<T, V>? GridGetAt(Position position)
//        {
//            return GetAt(position.X, position.Y);
//        }

//        public T? GetAt(int x, int y)
//        {
//            if (!IsInGrid(x, y))
//            {
//                return default(T);
//            }
//            return Lines[y][x];
//        }

//        public T? GetAt(Position position)
//        {
//            return GetAt(position.X, position.Y);
//        }

//        public void SetAt(int x, int y, T value)
//        {
//            if (!IsInGrid(x, y))
//            {
//                return;
//            }

//            Lines[y][x] = value;
//        }

//        public void SetAt(Position position, T value)
//        {
//            SetAt(position.X, position.Y, value);
//        }

//        public Position? Find(T c)
//        {
//            for (int y = 0; y < Height; y++)
//            {
//                for (int x = 0; x < Width; x++)
//                {
//                    if (c.Equals(GetAt(x, y)))
//                    {
//                        return new Position(x, y);
//                    }
//                }
//            }
//            return null;
//        }

//        public void ReplaceAll(T c, T newValue)
//        {
//            for (int y = 0; y < Height; y++)
//            {
//                for (int x = 0; x < Width; x++)
//                {
//                    if (c.Equals(GetAt(x, y)))
//                    {
//                        SetAt(x, y, newValue);
//                    }
//                }
//            }
//        }

//        public void Print()
//        {
//            for (int y = 0; y < Height; y++)
//            {
//                for (int x = 0; x < Width; x++)
//                {
//                    Console.Write(GetAt(x, y));
//                }
//                Console.WriteLine("");
//            }
//        }
//    }
//}

