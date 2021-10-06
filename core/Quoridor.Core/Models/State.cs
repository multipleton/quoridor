using System.Collections.Generic;
using Quoridor.Core.Exceptions;

namespace Quoridor.Core.Models
{
    public class State
    {
        private const short TOTAL_WALLS = 20;
        private readonly Point[] PLAYER_STARTUP_POSITIONS =
        {
            new Point(4, 0),
            new Point(4, 8),
            new Point(0, 4),
            new Point(8, 4),
        };

        private readonly short playersCount;

        private readonly List<Player> players;
        private readonly List<Wall> walls;

        public Player[] Players => players.ToArray();
        public Wall[] Walls => walls.ToArray();

        public State(short playersCount)
        {
            if (playersCount != 2 && playersCount != 4)
            {
                throw new InvalidPlayersCountException(playersCount);
            }
            this.playersCount = playersCount;
            players = new List<Player>(playersCount);
            walls = new List<Wall>(TOTAL_WALLS);
        }

        public void AddPlayer()
        {
            if (players.Count == playersCount)
            {
                throw new PlayerLimitReachedException(playersCount);
            }
            int index = players.Count;
            short id = (short)(index + 1);
            Point position = PLAYER_STARTUP_POSITIONS[index];
            short wallsCount = (short)(TOTAL_WALLS / playersCount);
            Player player = new Player(id, position, wallsCount);
            players.Add(player);
        }

        public Player GetPlayer(short id)
        {
            Player result = players.Find(player => player.Id == id);
            if (result == null)
            {
                throw new PlayerNotFoundException(id);
            }
            return result;
        }

        public void AddWall(Point[] start, Point[] end)
        {
            if (walls.Count == TOTAL_WALLS)
            {
                throw new WallLimitReachedException(TOTAL_WALLS);
            }
            Wall wall = new Wall(start, end);
            walls.Add(wall);
        }
    }
}
