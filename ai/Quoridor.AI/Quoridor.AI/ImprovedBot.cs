using Quoridor.Core;
using Quoridor.Core.Models;
using Quoridor.Core.Logic;
using System;
using Newtonsoft.Json;

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
            var path = pathFinder.BFS(Player.Position.X + (Player.Position.Y * 9), 
                pathFinder.GetPlayerWinPositionsInInt(Player.Id), state);
            var enemy = state.Players[0];
            if (Player.Id == 1)
            {
                enemy = state.Players[1];
            }
            var pathEnemy = pathFinder.BFS(enemy.Position.X + (enemy.Position.Y * 9), 
                pathFinder.GetPlayerWinPositionsInInt(enemy.Id), state);
            var nextPoint = path[1];
            if (pathEnemy.Length < path.Length && Player.WallsCount > 0)
            {
                Wall[] possibleWalls = pathFinder.GetAvailableWalls(state);
                Wall wall = null;
                int maxLength = int.MinValue;
                for (int i = 0; i < possibleWalls.Length; i++)
                {
                    State newState = Copier.CopyState(state);
                    newState.AddWall(possibleWalls[i]);
                    var curPath = pathFinder.BFS(enemy.Position.X + (enemy.Position.Y * 9),
                        pathFinder.GetPlayerWinPositionsInInt(enemy.Id), newState);
                    if (curPath.Length > maxLength)
                    {
                        maxLength = curPath.Length;
                        wall = possibleWalls[i];
                    } 
                }
                gameEngine.MakeMove(wall);
                return;
            }
            gameEngine.MakeMove(new Point(nextPoint % 9, nextPoint / 9));
            
        }

        class Copier
        {
            public static State CopyState(State state)
            {
                var player1 = state.Players[0];
                var player2 = state.Players[1];
                var walls = state.Walls;
                State newState = new State(2);
                newState.AddPlayer();
                newState.AddPlayer();
                newState.Players[0].Position.X = player1.Position.X;
                newState.Players[0].Position.Y = player1.Position.Y;
                newState.Players[0].wallsCount = player1.wallsCount;
                newState.Players[1].Position.X = player2.Position.X;
                newState.Players[1].Position.Y = player2.Position.Y;
                newState.Players[1].wallsCount = player2.wallsCount;
                for (int i = 0; i < walls.Length; i++)
                {
                    newState.AddWall(walls[i]);
                }
                return newState;
            }
        }
        
    }
}
