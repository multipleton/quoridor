using System;
using System.Collections.Generic;
using Quoridor.Core;
using Quoridor.Core.Models;

namespace Quoridor.AI
{
    public class RandomBot : Connection
    {
        private readonly Random random;
        private readonly GameEngine gameEngine;
        private State state;

        public RandomBot(GameEngine gameEngine) : base("Bot")
        {
            random = new Random();
            this.gameEngine = gameEngine;
        }

        private void MakeMove()
        {
            int moveType = random.Next(0, 2);
            if (moveType == 0)
            {
                Player player = state.GetPlayer(PlayerId);
                Point position = player.Position;
                List<Point> possibleMoves = new List<Point>();
                possibleMoves.Add(new Point((short)(position.X + 1), position.Y));
                possibleMoves.Add(new Point((short)(position.X - 1), position.Y));
                possibleMoves.Add(new Point(position.X, (short)(position.Y + 1)));
                possibleMoves.Add(new Point(position.X, (short)(position.Y - 1)));
                possibleMoves.Add(new Point((short)(position.X + 1), (short)(position.Y + 1)));
                possibleMoves.Add(new Point((short)(position.X + 1), (short)(position.Y - 1)));
                possibleMoves.Add(new Point((short)(position.X - 1), (short)(position.Y + 1)));
                possibleMoves.Add(new Point((short)(position.X - 1), (short)(position.Y - 1)));
                gameEngine.MakeMove(possibleMoves[random.Next(0, possibleMoves.Count)]);
            }
            else
            {
                Point start = new Point((short)random.Next(0, 9), (short)random.Next(0, 9));
                List<Point> possibleEnds = new List<Point>();
                possibleEnds.Add(new Point((short)(start.X + 1), start.Y));
                possibleEnds.Add(new Point(start.X, (short)(start.Y + 1)));
                Point end = possibleEnds[random.Next(0, possibleEnds.Count)];
                bool direction = start.X == end.X;
                int offsetX = direction ? 0 : 1;
                int offsetY = direction ? 1 : 0;
                Point[] startArray =
                {
                    start,
                    new Point((short)(start.X + offsetX), (short)(start.Y + offsetY))
                };
                Point[] endArray =
                {
                    end,
                    new Point((short)(end.X + offsetX), (short)(end.Y + offsetY))
                };
                gameEngine.MakeMove(startArray, endArray);
            }
        }

        public override void OnConnected() { }

        public override void OnFinish(Connection winner)
        {
            state = null;
        }

        public override void OnInvalidMove()
        {
            if (state != null)
            {
                MakeMove();
            }
        }

        public override void OnMove(Connection previous, Connection current) { }

        public override void OnNewConnection(Connection connection) { }

        public override void OnStart(Connection current) { }

        public override void OnUpdate(State state)
        {
            this.state = state;
        }

        public override void OnWaitingForMove()
        {
            if (state != null)
            {
                MakeMove();
            }
        }
    }
}
