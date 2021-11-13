using System;
using System.Text.Json;

namespace Quoridor.Core.Models
{
    public class Point
    {
        private int x;
        private int y;

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }

        public Point(int x, int y)
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

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
