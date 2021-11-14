using Quoridor.Core.Logic;
using Quoridor.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quoridor.AI
{
    public class Prediction
    {
        private PathFinder pathFinder;
        public Prediction() {
            pathFinder = new PathFinder();
        }
        public Point GetMove(State state, Player player)
        {
            var path = pathFinder.BFS(player.Position.X + (player.Position.Y * 9), pathFinder.GetPlayerWinPositionsInInt(player.Id), state);
            var nextPoint = path[1];
            return new Point(nextPoint % 9, nextPoint / 9);
        }
    }
}
