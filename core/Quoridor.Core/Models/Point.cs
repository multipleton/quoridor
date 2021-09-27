namespace Quoridor.Core.Models
{
    public class Point
    {
        private short x;
        private short y;

        public short X { get => x; set => x = value; }
        public short Y { get => y; set => y = value; }

        internal Point(short x, short y)
        {
            X = x;
            Y = y;
        }
    }
}
