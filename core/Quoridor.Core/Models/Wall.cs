using System;
using System.Linq;

namespace Quoridor.Core.Models
{
    public class Wall
    {
        private readonly Point[] start;
        private readonly Point[] end;

        public Point[] Start => start;
        public Point[] End => end;

        public Wall(Point[] start, Point[] end)
        {
            this.start = start;
            this.end = end;
        }

        public override bool Equals(object obj)
        {
            return obj is Wall wall
                && Start.SequenceEqual(wall.Start)
                && End.SequenceEqual(wall.End);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(start, end);
        }
    }
}
