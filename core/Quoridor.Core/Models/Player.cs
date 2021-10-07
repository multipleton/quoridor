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

        public Player(short id, Point position, short wallsCount)
        {
            this.id = id;
            this.position = position;
            this.wallsCount = wallsCount;
        }

        public void Move(Point point)
        {
            Position.X = point.X;
            Position.Y = point.Y;
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
