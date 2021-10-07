using System.Collections.Generic;
using Quoridor.Core.Models;

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

        private GameEngine() { }

        private State state;
        private List<Connection> connections;
        private int currentConnectionIndex = 0;

        private Connection CurrentConnection => connections[currentConnectionIndex];

        public void Initialize(int playersCount)
        {
            connections = new List<Connection>(playersCount);
            state = new State((short)playersCount);
        }

        public void Connect(Connection connection)
        {
            connection.PlayerId = (short)state.AddPlayer();
            connections.ForEach(entry => entry.OnNewConnection(connection));
            connections.Add(connection);
            connection.OnConnected();
        }

        private void SwitchConnection()
        {
            currentConnectionIndex += 1;
            if (currentConnectionIndex > connections.Count)
            {
                currentConnectionIndex = 0;
            }
        }

        public void Start()
        {
            connections.ForEach(entry => entry.OnStart(CurrentConnection));
            connections.ForEach(entry => entry.OnUpdate(state));
            CurrentConnection.OnWaitingForMove();
        }

        public void MakeMove(Point point)
        {
            Connection connection = CurrentConnection;
            Player player = state.GetPlayer(connection.PlayerId);
            if (true) // TODO: check is valid move
            {
                player.Move(point);
                CheckIsPlayerWin();
                SwitchConnection();
                connections.ForEach(entry => entry.OnMove(connection, CurrentConnection));
                connections.ForEach(entry => entry.OnUpdate(state));
                CurrentConnection.OnWaitingForMove();
            }
            else
            {
                connection.OnInvalidMove();
            }
        }

        public void MakeMove(Point[] start, Point[] end)
        {
            Connection connection = CurrentConnection;
            Player player = state.GetPlayer(connection.PlayerId);
            if (true && player.ReduceWallsCount()) // TODO: check is valid move
            {
                state.AddWall(start, end);
                SwitchConnection();
                connections.ForEach(entry => entry.OnMove(connection, CurrentConnection));
                connections.ForEach(entry => entry.OnUpdate(state));
                CurrentConnection.OnWaitingForMove();
            }
            else
            {
                connection.OnInvalidMove();
            }
        }

        private void CheckIsPlayerWin()
        {
            // TODO
            if (true)
            {
                connections.ForEach(entry => entry.OnFinish(CurrentConnection));
            }
        }

        private Point[] GetAvailablePlayerPositions()
        {
            return null; // TODO
        }

        private Point[,] GetAvailableWallPositions()
        {
            return null; // TODO
        }
    }
}
