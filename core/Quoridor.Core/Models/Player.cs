namespace Quoridor.Core.Models
{
    public class Player
    {
        private readonly short id;
        private readonly Point position;
        private short wallsCount;

        public short Id => id;
        public Point Position => position;
        public short WallsCount => wallsCount;

        internal Player(short id, Point position, short wallsCount)
        {
            this.id = id;
            this.position = position;
            this.wallsCount = wallsCount;
        }

        internal void Move(short x, short y)
        {
            Position.X = x;
            Position.Y = y;
        }

        internal bool ReduceWallsCount()
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
