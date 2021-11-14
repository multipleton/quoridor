using Quoridor.Core;
using Quoridor.Core.Models;
using Quoridor.Core.Logic;
using System;

namespace Quoridor.AI
{
    public class ImprovedBot : Connection
    {
        private readonly GameEngine gameEngine;
        private State state;
        private PathFinder pathFinder;
        private readonly Random random;
        private Prediction prediction;

        public ImprovedBot(GameEngine gameEngine) : base("Improved Bot")
        {
            this.gameEngine = gameEngine;
            random = new Random();
            pathFinder = new PathFinder();
            prediction = new Prediction();
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

        public override void OnMove(Connection previous, Connection current, Point oldPoint, Point point, Wall wall) {}

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
        private void MakeMove()
        {
            
            Point move = prediction.GetMove(state, Player); 
            gameEngine.MakeMove(move);
            /* Wall[] possibleWalls = pathFinder.GetAvailableWalls(state);
             Wall wall = possibleWalls[random.Next(0, possibleWalls.Length)];
             gameEngine.MakeMove(wall);*/
        }
    }
}
