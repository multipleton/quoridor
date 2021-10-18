using System.Collections.Generic;
using Quoridor.Core.Models;
using Quoridor.Core.Exceptions;

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

        public GameEngine() { }

        private State state;
        private List<Connection> connections;
        private int currentConnectionIndex;
        private bool gameFinished;

        private Connection CurrentConnection => connections[currentConnectionIndex];

        public void Initialize(int playersCount)
        {
            state = new State((short)playersCount);
            connections = new List<Connection>(playersCount);
            currentConnectionIndex = 0;
            gameFinished = false;
        }

        public void Connect(Connection connection)
        {
            if (gameFinished) throw new GameFinishedException();
            connection.PlayerId = (short)state.AddPlayer();
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
        }

        public virtual void MakeMove(Point point)
        {
            if (gameFinished) throw new GameFinishedException();
            Connection connection = CurrentConnection;
            Player player = state.GetPlayer(connection.PlayerId);
            if (IsValidMove(point))
            {
                player.Move(point);
                if (IsPlayerWin())
                {
                    Finish();
                }
                else
                {
                    NextConnection();
                    connections.ForEach(entry => entry.OnMove(connection, CurrentConnection));
                    connections.ForEach(entry => entry.OnUpdate(state));
                    CurrentConnection.OnWaitingForMove();
                }
            }
            else
            {
                connection.OnInvalidMove();
            }
        }

        public virtual void MakeMove(Point[] start, Point[] end)
        {
            if (gameFinished) throw new GameFinishedException();
            Connection connection = CurrentConnection;
            Player player = state.GetPlayer(connection.PlayerId);
            if (IsValidMove(start, end) && player.ReduceWallsCount())
            {
                state.AddWall(start, end);
                NextConnection();
                connections.ForEach(entry => entry.OnMove(connection, CurrentConnection));
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
            if (currentConnectionIndex > connections.Count)
            {
                currentConnectionIndex = 0;
            }
        }

        private bool IsPlayerWin()
        {
            return false; // TODO
        }

        private bool IsValidMove(Point point)
        {
            return true; // TODO
        }

        private bool IsValidMove(Point[] start, Point[] end)
        {
            return true; // TODO
        }

        private void Finish()
        {
            gameFinished = true;
            connections.ForEach(entry => entry.OnFinish(CurrentConnection));
        }
    }
}
