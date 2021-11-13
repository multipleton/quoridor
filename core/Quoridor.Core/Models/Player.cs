namespace Quoridor.Core.Models
{
    public class Player
    {
        private readonly int id;
        private readonly Point position;
        private int wallsCount;

        public int Id => id;
        public Point Position => position;
        public int WallsCount => wallsCount;

        public Player(int id, Point position, int wallsCount)
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
