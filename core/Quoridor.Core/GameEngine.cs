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

        private Node[,] transformFieldStateToGraph(int[,] fieldState)
        {
            var nodes = new Node[9, 9];
            for (int i = 0; i < 17; i = i + 2)
            {
                for (int j = 0; j < 17; j = j + 2)
                {
                    int x = i / 2;
                    int y = j / 2;
                    var node = new Node();
                    node.X = x;
                    node.Y = y;
                    nodes[x, y] = node;

                    if (i == 0)
                    {
                        node.Top = 1;
                    }
                    if (j == 0)
                    {
                        node.Left = 1;
                    }
                    if (i == 16)
                    {
                        node.Bottom = 1;
                    }
                    if (j == 16)
                    {
                        node.Right = 1;
                    }

                    if (j + 1 < 17 && fieldState[i, j + 1] == 9)
                    {
                        node.Right = 1;
                    }
                    if (i + 1 < 17 && fieldState[i + 1, j] == 8)
                    {
                        node.Bottom = 1;
                    }
                    if (j - 1 > 0 && fieldState[i, j - 1] == 9)
                    {
                        node.Left = 1;
                    }
                    if (i - 1 > 0 && fieldState[i - 1, j] == 8)
                    {
                        node.Top = 1;
                    }
                }
            }
            return nodes;
        }
        internal class Node
        {
            private int top = 0;
            private int bottom = 0;
            private int left = 0;
            private int right = 0;
            private int x;
            private int y;

            public int Top { get => top; set => top = value; }
            public int Bottom { get => bottom; set => bottom = value; }
            public int Left { get => left; set => left = value; }
            public int Right { get => right; set => right = value; }
            public int X { get => x; set => x = value; }
            public int Y { get => y; set => y = value; }
        }
    }
    
}

