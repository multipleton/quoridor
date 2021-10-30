using System;

namespace Quoridor.Core.Models
{
    public class Point
    {
        private short x;
        private short y;

        public short X { get => x; set => x = value; }
        public short Y { get => y; set => y = value; }

        public Point(short x, short y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            return obj is Point point
                && X == point.X
                && Y == point.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }
    }
}
