using System.Collections.Generic;
using Quoridor.Core.Exceptions;

namespace Quoridor.Core.Models
{
    public class State
    {
        private const int TOTAL_WALLS = 20;
        private readonly Point[] PLAYER_STARTUP_POSITIONS =
        {
            new Point(4, 8),
            new Point(4, 0),
            new Point(0, 4),
            new Point(8, 4),
        };

        private readonly int playersCount;

        private readonly List<Player> players;
        private readonly List<Wall> walls;

        public Point[] PlayerStartupPositions => PLAYER_STARTUP_POSITIONS;

        public Player[] Players => players.ToArray();
        public Wall[] Walls => walls.ToArray();

        public State(int playersCount)
        {
            if (playersCount != 2 && playersCount != 4)
            {
                throw new InvalidPlayersCountException(playersCount);
            }
            this.playersCount = playersCount;
            players = new List<Player>(playersCount);
            walls = new List<Wall>(TOTAL_WALLS);
        }

        public Player AddPlayer()
        {
            if (players.Count == playersCount)
            {
                throw new PlayerLimitReachedException(playersCount);
            }
            int index = players.Count;
            int id = index + 1;
            Point position = PLAYER_STARTUP_POSITIONS[index];
            int wallsCount = TOTAL_WALLS / playersCount;
            Player player = new Player(id, position, wallsCount);
            players.Add(player);
            return player;
        }

        public void AddWall(Wall wall)
        {
            if (walls.Count == TOTAL_WALLS)
            {
                throw new WallLimitReachedException(TOTAL_WALLS);
            }
            walls.Add(wall);
        }
    }
}
