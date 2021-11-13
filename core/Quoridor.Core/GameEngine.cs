using System;
using System.Collections.Generic;
using Quoridor.Core.Models;
using Quoridor.Core.Exceptions;
using Quoridor.Core.Logic;

namespace Quoridor.Core
{
    public class GameEngine
    {
        private static GameEngine instance = null;

        public static GameEngine Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameEngine();
                }
                return instance;
            }
        }

        private GameEngine()
        {
            pathFinder = new PathFinder();
        }

        public event Action OnStart;
        public event Action OnFinish;

        private readonly PathFinder pathFinder;

        private State state;
        private List<Connection> connections;
        private int currentConnectionIndex;
        private bool gameFinished;

        private Connection CurrentConnection => connections[currentConnectionIndex];

        public void Initialize(int playersCount)
        {
            state = new State(playersCount);
            connections = new List<Connection>(playersCount);
            currentConnectionIndex = 0;
            gameFinished = false;
        }

        public void Connect(Connection connection)
        {
            if (gameFinished) throw new GameFinishedException();
            connection.Player = state.AddPlayer();
            connections.ForEach(entry => entry.OnNewConnection(connection));
            connections.Add(connection);
            connection.OnConnected();
        }

        public void Start()
        {
            if (gameFinished) throw new GameFinishedException();
            connections.ForEach(entry => entry.OnStart(CurrentConnection));
            connections.ForEach(entry => entry.OnUpdate(state));
            CurrentConnection.OnWaitingForMove();
            OnStart?.Invoke();
        }

        public void MakeMove(Point point)
        {
            if (gameFinished) throw new GameFinishedException();
            Connection connection = CurrentConnection;
            Player player = connection.Player;
            if (IsValidMove(point))
            {
                player.Move(point);
                if (IsPlayerWin(player))
                {
                    Finish();
                }
                else
                {
                    NextConnection();
                    connections.ForEach(entry =>
                        entry.OnMove(connection, CurrentConnection, point, null));
                    connections.ForEach(entry => entry.OnUpdate(state));
                    CurrentConnection.OnWaitingForMove();
                }
            }
            else
            {
                connection.OnInvalidMove();
            }
        }

        public void MakeMove(Wall wall)
        {
            if (gameFinished) throw new GameFinishedException();
            Connection connection = CurrentConnection;
            Player player = connection.Player;
            if (IsValidMove(wall) && player.ReduceWallsCount())
            {
                state.AddWall(wall);
                NextConnection();
                connections.ForEach(entry => entry
                    .OnMove(connection, CurrentConnection, null, wall));
                connections.ForEach(entry => entry.OnUpdate(state));
                CurrentConnection.OnWaitingForMove();
            }
            else
            {
                connection.OnInvalidMove();
            }
        }

        private void NextConnection()
        {
            currentConnectionIndex += 1;
            if (currentConnectionIndex == connections.Count)
            {
                currentConnectionIndex = 0;
            }
        }

        private bool IsPlayerWin(Player player)
        {
            Point[] winPositions = pathFinder.GetPlayerWinPositions(player.Id - 1);
            Point point = player.Position;
            foreach (var position in winPositions)
            {
                if (position.Equals(point))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsValidMove(Point point)
        {
            Player player = CurrentConnection.Player;
            Point[] availableMoves = pathFinder.GetAvailableMoves(state, player);
            foreach (var position in availableMoves)
            {
                if (position.Equals(point))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsValidMove(Wall wall)
        {
            Wall[] walls = pathFinder.GetAvailableWalls(state);
            foreach (var entry in walls)
            {
                if (entry.Equals(wall))
                {
                    return true;
                }
            }
            return false;
        }

        private void Finish()
        {
            gameFinished = true;
            connections.ForEach(entry => entry.OnFinish(CurrentConnection));
            OnFinish?.Invoke();
        }
    }
}
