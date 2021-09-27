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
    }
}
