﻿using System.Collections.Generic;
using Quoridor.Core.Exceptions;

namespace Quoridor.Core.Models
{
    public class State
    {
        private const short TOTAL_WALLS = 20;
        private readonly Point[] PLAYER_STARTUP_POSITIONS =
        {
            new Point(5, 0),
            new Point(5, 9),
            new Point(0, 5),
            new Point(9, 5),
        };

        private readonly short playersCount;

        private readonly List<Player> players;
        private readonly List<Wall> walls;

        public Player[] Players => players.ToArray();
        public Wall[] Walls => walls.ToArray();

        internal State(short playersCount)
        {
            if (playersCount != 2 || playersCount != 4)
            {
                throw new InvalidPlayersCountException(playersCount);
            }
            this.playersCount = playersCount;
            players = new List<Player>(playersCount);
            walls = new List<Wall>(TOTAL_WALLS);
        }

        internal void AddPlayer()
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

        internal Player GetPlayer(short id)
        {
            Player result = players.Find(player => player.Id == id);
            if (result == null)
            {
                throw new PlayerNotFoundException(id);
            }
            return result;
        }

        internal void AddWall(Point[] start, Point[] end)
        {
            Wall wall = new Wall(start, end);
            walls.Add(wall);
        }
    }
}