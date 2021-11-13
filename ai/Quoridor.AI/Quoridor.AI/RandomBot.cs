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

        public RandomBot(GameEngine gameEngine) : base("Random Bot")
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
                possibleMoves.Add(new Point(position.X + 1, position.Y));
                possibleMoves.Add(new Point(position.X - 1, position.Y));
                possibleMoves.Add(new Point(position.X, position.Y + 1));
                possibleMoves.Add(new Point(position.X, position.Y - 1));
                possibleMoves.Add(new Point(position.X + 1, position.Y + 1));
                possibleMoves.Add(new Point(position.X + 1, position.Y - 1));
                possibleMoves.Add(new Point(position.X - 1, position.Y + 1));
                possibleMoves.Add(new Point(position.X - 1, position.Y - 1));
                gameEngine.MakeMove(possibleMoves[random.Next(0, possibleMoves.Count)]);
            }
            else
            {
                int startX = random.Next(0, 9);
                int startY = random.Next(0, 9);
                bool direction = random.Next(0, 2) == 0;
                int offsetX = direction ? 0 : 1;
                int offsetY = direction ? 1 : 0;
                Point start = new Point(startX, startY);
                List<Point> possibleEnds = new List<Point>();
                possibleEnds.Add(new Point(start.X + 1, start.Y));
                possibleEnds.Add(new Point(start.X, start.Y + 1));
                Point end = possibleEnds[random.Next(0, possibleEnds.Count)];
                Point[] startArray =
                {
                    start,
                    new Point(start.X + offsetX, start.Y + offsetY)
                };
                Point[] endArray =
                {
                    end,
                    new Point(end.X + offsetX, end.Y + offsetY)
                };
                Wall wall = new Wall(startArray, endArray);
                gameEngine.MakeMove(wall);
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

        public override void OnMove(Connection previous, Connection current, Point point, Wall wall) { }

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
