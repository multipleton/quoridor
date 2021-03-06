using System.Text.Json;

namespace Quoridor.Core.Models
{
    public class Player
    {
        private readonly int id;
        private readonly Point position;
        public int wallsCount; // TODO: refactor

        public int Id => id;
        public Point Position => new Point(position.X, position.Y);
        public int WallsCount => wallsCount;

        public Player(int id, Point position, int wallsCount)
        {
            this.id = id;
            this.position = position;
            this.wallsCount = wallsCount;
        }

        public void Move(Point point)
        {
            position.X = point.X;
            position.Y = point.Y;
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

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
