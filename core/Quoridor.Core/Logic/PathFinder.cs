using Quoridor.Core.Models;
using System;
using System.Collections.Generic;

namespace Quoridor.Core.Logic
{
    public class PathFinder
    {
        private Dictionary<int, int[]> adjacencyGraph;
        private int[] marked;
        private int[,] fieldState;

        public Wall[] GetAvailableWalls(State state)
        {
            int[,] matrix = AddWallsAndPlayersToMatrix(state);
            List<Wall> walls = new List<Wall>();
            for (int i = 1; i < 17; i = i + 2)
            {
                for (int j = 0; j < 17; j = j + 2)
                {
                    int x = (i - 1) / 2;
                    int y = j / 2;
                    if (j + 2 < 17 && matrix[i, j] == 0 && matrix[i, j + 1] == 0 && matrix[i, j + 2] == 0)
                    {
                        Wall wall = new Wall(
                            new Point[] { new Point(x, y), new Point(x, y + 1) },
                            new Point[] { new Point(x + 1, y), new Point(x + 1, y + 1) });
                        if (HasAvailablePaths(state, wall))
                        {
                            walls.Add(new Wall(
                            new Point[] { new Point(y, x), new Point(y + 1, x) },
                            new Point[] { new Point(y, x + 1), new Point(y + 1, x + 1) }));
                        }
                    }
                }
            }
            for (int i = 0; i < 17; i = i + 2)
            {
                for (int j = 1; j < 17; j = j + 2)
                {
                    int x = i / 2;
                    int y = (j - 1) / 2;
                    if (i + 2 < 17 && matrix[i, j] == 0 && matrix[i + 1, j] == 0 && matrix[i + 2, j] == 0)
                    {
                        Wall wall = new Wall(
                            new Point[] { new Point(x, y), new Point(x + 1, y) },
                            new Point[] { new Point(x, y + 1), new Point(x + 1, y + 1) });
                        if (HasAvailablePaths(state, wall))
                        {
                            walls.Add(new Wall(
                            new Point[] { new Point(y, x), new Point(y, x + 1) },
                            new Point[] { new Point(y + 1, x), new Point(y + 1, x + 1) }));
                        }
                    }
                }
            }
            return walls.ToArray();
        }

        public object BFS<T>(T t)
        {
            throw new NotImplementedException();
        }

        public Point[] GetPlayerWinPositions(int index)
        {
            List<Point> positions = new List<Point>();
            int y = index == 0 ? 0 : 8;
            for (int i = 0; i < 9; i++)
            {
                positions.Add(new Point(i, y));
            }
            return positions.ToArray();
        }

        public int[] GetPlayerWinPositionsInInt(int index)
        {
            if (index == 2)
            {
                return new int[] { 72, 73, 74, 75, 76, 77, 78, 79, 80 };
            }
            return new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
        }

        public Point[] GetAvailableMoves(State state, int y, int x)
        {
            fieldState = AddWallsAndPlayersToMatrix(state);
            int i = x * 2;
            int j = y * 2;
            List<Point> value = new List<Point>();
            // 1 []
            if (j + 2 < 17 && fieldState[i, j + 1] == 0 && fieldState[i, j + 2] == 0)
            {
                value.Add(new Point(y + 1, x));
            }
            // [] 1
            if (j - 2 >= 0 && fieldState[i, j - 1] == 0 && fieldState[i, j - 2] == 0)
            {
                value.Add(new Point(y - 1, x));
            }
            // 1
            // []
            if (i + 2 < 17 && fieldState[i + 1, j] == 0 && fieldState[i + 2, j] == 0)
            {
                value.Add(new Point(y, x + 1));
            }
            // []
            // 1
            if (i - 2 >= 0 && fieldState[i - 1, j] == 0 && fieldState[i - 2, j] == 0)
            {
                value.Add(new Point(y, x - 1));
            }
            if (j + 4 < 17 && fieldState[i, j + 1] == 0 && fieldState[i, j + 2] != 0)
            {
                // 1 2 []
                if (fieldState[i, j + 3] == 0)
                {
                    value.Add(new Point(y + 2, x));
                }
                else
                {
                    // 1 2 |
                    //   [] 
                    if (i + 2 < 17 && fieldState[i + 1, j + 2] == 0 && fieldState[i + 2, j + 2] == 0)
                    {
                        value.Add(new Point(y + 1, x + 1));
                    }
                    //   [] 
                    // 1 2 |
                    if (i - 2 >= 0 && fieldState[i - 1, j + 2] == 0 && fieldState[i - 2, j + 2] == 0)
                    {
                        value.Add(new Point(y + 1, x - 1));
                    }
                }
            }
            if (j - 4 >= 0 && fieldState[i, j - 1] == 0 && fieldState[i, j - 2] != 0)
            {
                // [] 2 1
                if (fieldState[i, j - 3] == 0)
                {
                    value.Add(new Point(y - 2, x));
                }
                else
                {
                    // | 2 1
                    //  []
                    if (i + 2 < 17 && fieldState[i + 1, j - 2] == 0 && fieldState[i + 2, j - 2] == 0)
                    {
                        value.Add(new Point(y - 1, x + 1));
                    }
                    //  [] 
                    // | 2 1
                    if (i - 2 >= 0 && fieldState[i - 1, j - 2] == 0 && fieldState[i - 2, j - 2] == 0)
                    {
                        value.Add(new Point(y - 1, x - 1));
                    }
                }
            }
            if (i + 4 < 17 && fieldState[i + 1, j] == 0 && fieldState[i + 2, j] != 0)
            {
                // 1
                // 2
                // []
                if (fieldState[i + 3, j] == 0)
                {
                    value.Add(new Point(y, x + 2));
                }
                else
                {
                    // 1
                    // 2 []
                    // --
                    if (j + 2 < 17 && fieldState[i + 2, j + 1] == 0 && fieldState[i + 2, j + 2] == 0)
                    {
                        value.Add(new Point(y + 1, x + 1));
                    }
                    //    1
                    // [] 2 
                    //    --
                    if (j - 2 >= 0 && fieldState[i + 2, j - 1] == 0 && fieldState[i + 2, j - 2] == 0)
                    {
                        value.Add(new Point(y - 1, x + 1));
                    }
                }
            }
            if (i - 4 >= 0 && fieldState[i - 1, j] == 0 && fieldState[i - 2, j] != 0)
            {
                // []
                // 2
                // 1
                if (fieldState[i - 3, j] == 0)
                {
                    value.Add(new Point(y, x - 2));
                }
                else
                {
                    // --
                    // 2 []
                    // 1
                    if (j + 2 < 17 && fieldState[i - 2, j + 1] == 0 && fieldState[i - 2, j + 2] == 0)
                    {
                        value.Add(new Point(y + 1, x - 1));
                    }
                    //    --
                    // [] 2
                    //    1
                    if (j - 2 >= 0 && fieldState[i - 2, j - 1] == 0 && fieldState[i - 2, j - 2] == 0)
                    {
                        value.Add(new Point(y - 1, x - 1));
                    }
                }
            }

            if (j == 2 && fieldState[i, 1] == 0 && fieldState[i, 0] != 0)
            {
                // || 1 2
                // []
                if (i + 1 < 17 && fieldState[i + 1, 0] == 0)
                {
                    value.Add(new Point(0, x + 1));
                }
                // []
                // || 1 2
                if (i - 2 >= 0 && fieldState[i - 1, 0] == 0)
                {
                    value.Add(new Point(0, x - 1));
                }
            }
            if (j == 14 && fieldState[i, 15] == 0 && fieldState[i, 16] != 0)
            {
                // 1 2 ||
                //   []
                if (i + 1 < 17 && fieldState[i + 1, 16] == 0)
                {
                    value.Add(new Point(8, x + 1));
                }
                //   []
                // 1 2 ||
                if (i - 2 >= 0 && fieldState[i - 1, 16] == 0)
                {
                    value.Add(new Point(8, x - 1));
                }
            }
            if (i == 2 && fieldState[1, j] == 0 && fieldState[0, j] != 0)
            {
                // ---
                // ---
                // 1 []
                // 2
                if (j + 1 < 17 && fieldState[0, j + 1] == 0)
                {
                    value.Add(new Point(y + 1, 0));
                }
                // -----
                // -----
                // [] 1
                //    2
                if (j - 2 >= 0 && fieldState[0, j - 1] == 0)
                {
                    value.Add(new Point(y - 1, 0));
                }
            }
            if (i == 14 && fieldState[15, j] == 0 && fieldState[16, j] != 0)
            {
                // 2
                // 1 []
                // ---
                // ---
                if (j + 1 < 17 && fieldState[16, j + 1] == 0)
                {
                    value.Add(new Point(y + 1, 8));
                }
                //    2
                // [] 1
                // -----
                // -----
                if (j - 2 >= 0 && fieldState[16, j - 1] == 0)
                {
                    value.Add(new Point(y - 1, 8));
                }
            }
            Point[] result = value.ToArray();
            return result;
        }

        public int[] BFS(int start, int[] playerWinPositions, State state)
        {
            fieldState = AddWallsAndPlayersToMatrix(state);
            adjacencyGraph = TransformFieldStateToAdjacencyMatrix(fieldState, state);
            Queue<int> q = new Queue<int>();
            int[] p = new int[81];
            p[start] = -1;
            int[] dist = new int[81];
            marked = new int[81];
            q.Enqueue(start);
            dist[start] = 0;
            marked[start] = 1;
            while (q.Count > 0)
            {
                int v = q.Dequeue();
                for (int i = 0; i < adjacencyGraph[v].Length; i++)
                {
                    if (marked[adjacencyGraph[v][i]] == 0)
                    {
                        dist[adjacencyGraph[v][i]] = dist[v] + 1;
                        p[adjacencyGraph[v][i]] = v;
                        marked[adjacencyGraph[v][i]] = 1;
                        q.Enqueue(adjacencyGraph[v][i]);
                    }
                }
            }
            int shortestNode = playerWinPositions[0];
            for (int i = 0; i < playerWinPositions.Length; i++)
            {
                if (dist[playerWinPositions[i]] < dist[shortestNode])
                {
                    shortestNode = playerWinPositions[i];
                }
            }
            Queue<int> queue = new Queue<int>();
            while (shortestNode != -1)
            {
                queue.Enqueue(shortestNode);
                shortestNode = p[shortestNode];
            }
            var path = queue.ToArray();
            int[] reversedPath = new int[path.Length];
            for (int i = 0; i < path.Length; i++)
            {
                reversedPath[i] = path[path.Length - 1 - i];
            }
            return reversedPath;
        }

        private bool HasAvailablePaths(State state, Wall wall)
        {
            fieldState = AddWallsAndPlayersToMatrix(state, wall);
            adjacencyGraph = TransformFieldStateToAdjacencyMatrix(fieldState, state);
            Player[] players = state.Players;
            int[] firstWinCases = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            int[] secondWinCases = new int[] { 72, 73, 74, 75, 76, 77, 78, 79, 80 };
            marked = new int[81];
            int x = players[0].Position.Y;
            int y = players[0].Position.X;
            if (!DFS(x * 9 + y, firstWinCases)) return false;
            marked = new int[81];
            int x2 = players[1].Position.Y;
            int y2 = players[1].Position.X;
            if (!DFS(x2 * 9 + y2, secondWinCases)) return false;
            return true;
        }

        private Dictionary<int, int[]> TransformFieldStateToAdjacencyMatrix(int[,] matrix, State state)
        {
            Dictionary<int, int[]> result = new Dictionary<int, int[]>();
            for (int i = 0; i < matrix.GetLength(0); i = i + 2)
            {
                for (int j = 0; j < matrix.GetLength(1); j = j + 2)
                {
                    int x = i / 2;
                    int y = j / 2;
                    int key = x * 9 + y;
                    var res = GetAvailableMoves(state, y, x);
                    List<int> value = new List<int>();
                    for (int k = 0; k < res.Length; k++)
                    {
                        value.Add(res[k].X + res[k].Y * 9);
                    }
                    if (j + 1 < 17 && matrix[i, j + 1] == 0)
                    {
                        value.Add(x * 9 + y + 1);
                    }
                    if (j - 1 > 0 && matrix[i, j - 1] == 0)
                    {
                        value.Add(x * 9 + y - 1);
                    }
                    if (i + 1 < 17 && matrix[i + 1, j] == 0)
                    {
                        value.Add((x + 1) * 9 + y);
                    }
                    if (i - 1 > 0 && matrix[i - 1, j] == 0)
                    {
                        value.Add((x - 1) * 9 + y);
                    }
                    result.Add(key, value.ToArray());
                }
            }
            return result;
        }

        private bool DFS(int current, int[] winCases)
        {
            if (marked[current] != 0)
            {
                return false;
            }
            marked[current] = 1;
            for (int i = 0; i < winCases.Length; i++)
            {
                if (current == winCases[i]) return true;
            }
            for (int i = 0; i < adjacencyGraph[current].Length; i++)
            {
                if (DFS(adjacencyGraph[current][i], winCases)) return true;
            }
            return false;
        }

        private int[,] AddWallsAndPlayersToMatrix(State state, Wall wall = null)
        {
            int[,] matrix = new int[17, 17];
            Player[] players = state.Players;
            List<Wall> wallsList = new List<Wall>();
            for (int i = 0; i < state.Walls.Length; i++)
            {
                wallsList.Add(wallAdapter(state.Walls[i]));
            }
            if (wall != null) wallsList.Add(wall);
            Wall[] walls = wallsList.ToArray();
            for (int i = 0; i < players.Length; i++)
            {
                int x = players[i].Position.Y;
                int y = players[i].Position.X;
                matrix[x * 2, y * 2] = i + 1;
            }
            for (int i = 0; i < walls.Length; i++)
            {
                Point[] start = walls[i].Start;

                int x1 = start[0].X;
                int y1 = start[0].Y;
                int x2 = start[1].X;

                if (x1 == x2)
                {
                    matrix[x1 * 2 + 1, y1 * 2] = 8;
                    matrix[x1 * 2 + 1, y1 * 2 + 1] = 8;
                    matrix[x1 * 2 + 1, y1 * 2 + 2] = 8;
                }
                else
                {
                    matrix[x1 * 2, y1 * 2 + 1] = 9;
                    matrix[x1 * 2 + 1, y1 * 2 + 1] = 9;
                    matrix[x1 * 2 + 2, y1 * 2 + 1] = 9;
                }
            }
            return matrix;
        }

        private Wall wallAdapter(Wall wall)
        {
            int x1 = wall.Start[0].X;
            int y1 = wall.Start[0].Y;
            int x2 = wall.Start[1].X;
            int y2 = wall.Start[1].Y;
            int x3 = wall.End[0].X;
            int y3 = wall.End[0].Y;
            int x4 = wall.End[1].X;
            int y4 = wall.End[1].Y;
            return new Wall(new Point[] { new Point(y1, x1), new Point(y2, x2) }, new Point[] { new Point(y3, x3), new Point(y4, x4) });
        }
    }
}
