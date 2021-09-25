namespace Quoridor.Core
{
    class Player
    {
        private readonly Point position;
        private short wallsCount;

        public Point Position => position;
        public short WallsCount => wallsCount;

        public Player(Point position, short wallsCount)
        {
            this.position = position;
            this.wallsCount = wallsCount;
        }

        public void Move(short x, short y)
        {
            Position.X = x;
            Position.Y = y;
        }

        public bool ReduceWallsCount()
        {
            if (wallsCount > 0)
            {
                wallsCount--;
                return true;
            }
            return false;
        }
    }
}
