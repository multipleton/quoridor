namespace Quoridor.Core
{
    class Point
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
    }
}
