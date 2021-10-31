using Quoridor.Core.Models;
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
                            new Point[] { new Point((short)x, (short)y), new Point((short)x, (short)(y + 1)) },
                            new Point[] { new Point((short)(x + 1), (short)y), new Point((short)(x + 1), (short)(y + 1)) });
                        if (HasAvailablePaths(state, wall))
                        {
                            walls.Add(new Wall(
                            new Point[] { new Point((short)y, (short)x), new Point((short)(y + 1), (short)x) },
                            new Point[] { new Point((short)y, (short)(x + 1)), new Point((short)(y + 1), (short)(x + 1)) }));
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
                            new Point[] { new Point((short)x, (short)y), new Point((short)(x + 1), (short)y) },
                            new Point[] { new Point((short)x, (short)(y + 1)), new Point((short)(x + 1), (short)(y + 1)) });
                        if (HasAvailablePaths(state, wall))
                        {
                            walls.Add(new Wall(
                            new Point[] { new Point((short)y, (short)x), new Point((short)y, (short)(x + 1)) },
                            new Point[] { new Point((short)(y + 1), (short)x), new Point((short)(y + 1), (short)(x + 1)) }));
                        }
                    }
                }
            }
            return walls.ToArray();
        }

        public Point[] GetPlayerWinPositions(int index)
        {
            List<Point> positions = new List<Point>();
            int y = index == 0 ? 8 : 0;
            for (int i = 0; i < 9; i++)
            {
                positions.Add(new Point((short)i, (short)y));
            }
            return positions.ToArray();
        }

        public Point[] GetAvailableMoves(State state, Player player)
        {
            fieldState = AddWallsAndPlayersToMatrix(state);
            adjacencyGraph = TransformFieldStateToAdjacencyMatrix(fieldState);
            int x = player.Position.X;
            int y = player.Position.Y;
            int i = x * 2;
            int j = y * 2;
            List<Point> value = new List<Point>();
            // 1 []
            if (j + 2 < 17 && fieldState[i, j + 1] == 0 && fieldState[i, j + 2] == 0)
            {
                value.Add(new Point((short)(y + 1), (short)x));
            }
            // [] 1
            if (j - 2 >= 0 && fieldState[i, j - 1] == 0 && fieldState[i, j - 2] == 0)
            {
                value.Add(new Point((short)(y - 1), (short)x));
            }
            // 1
            // []
            if (i + 2 < 17 && fieldState[i + 1, j] == 0 && fieldState[i + 2, j] == 0)
            {
                value.Add(new Point((short)y, (short)(x + 1)));
            }
            // []
            // 1
            if (i - 2 >= 0 && fieldState[i - 1, j] == 0 && fieldState[i - 2, j] == 0)
            {
                value.Add(new Point((short)y, (short)(x - 1)));
            }
            if (j + 4 < 17 && fieldState[i, j + 1] == 0 && fieldState[i, j + 2] != 0)
            {
                // 1 2 []
                if (fieldState[i, j + 3] == 0)
                {
                    value.Add(new Point((short)(y + 2), (short)x));
                }
                else
                {
                    // 1 2 |
                    //   [] 
                    if (i + 2 < 17 && fieldState[i + 1, j + 2] == 0 && fieldState[i + 2, j + 2] == 0)
                    {
                        value.Add(new Point((short)(y + 1), (short)(x + 1)));
                    }
                    //   [] 
                    // 1 2 |
                    if (i - 2 >= 0 && fieldState[i - 1, j + 2] == 0 && fieldState[i - 2, j + 2] == 0)
                    {
                        value.Add(new Point((short)(y + 1), (short)(x - 1)));
                    }
                }
            }
            if (j - 4 >= 0 && fieldState[i, j - 1] == 0 && fieldState[i, j - 2] != 0)
            {
                // [] 2 1
                if (fieldState[i, j - 3] == 0)
                {
                    value.Add(new Point((short)(y - 2), (short)x));
                }
                else
                {
                    // | 2 1
                    //  []
                    if (i + 2 < 17 && fieldState[i + 1, j - 2] == 0 && fieldState[i + 2, j - 2] == 0)
                    {
                        value.Add(new Point((short)(y - 1), (short)(x + 1)));
                    }
                    //  [] 
                    // | 2 1
                    if (i - 2 >= 0 && fieldState[i - 1, j - 2] == 0 && fieldState[i - 2, j - 2] == 0)
                    {
                        value.Add(new Point((short)(y - 1), (short)(x - 1)));
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
                    value.Add(new Point((short)y, (short)(x + 2)));
                }
                else
                {
                    // 1
                    // 2 []
                    // --
                    if (j + 2 < 17 && fieldState[i + 2, j + 1] == 0 && fieldState[i + 2, j + 2] == 0)
                    {
                        value.Add(new Point((short)(y + 1), (short)(x + 1)));
                    }
                    //    1
                    // [] 2 
                    //    --
                    if (j - 2 >= 0 && fieldState[i + 2, j - 1] == 0 && fieldState[i + 2, j - 2] == 0)
                    {
                        value.Add(new Point((short)(y - 1), (short)(x + 1)));
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
                    value.Add(new Point((short)y, (short)(x - 2)));
                }
                else
                {
                    // --
                    // 2 []
                    // 1
                    if (j + 2 < 17 && fieldState[i - 2, j + 1] == 0 && fieldState[i - 2, j + 2] == 0)
                    {
                        value.Add(new Point((short)(y + 1), (short)(x - 1)));
                    }
                    //    --
                    // [] 2
                    //    1
                    if (j - 2 >= 0 && fieldState[i - 2, j - 1] == 0 && fieldState[i - 2, j - 2] == 0)
                    {
                        value.Add(new Point((short)(y - 1), (short)(x - 1)));
                    }
                }
            }
            Point[] result = value.ToArray();
            return result;
        }

        private bool HasAvailablePaths(State state, Wall wall)
        {
            fieldState = AddWallsAndPlayersToMatrix(state, wall);
            adjacencyGraph = TransformFieldStateToAdjacencyMatrix(fieldState);
            Player[] players = state.Players;
            int[] firstWinCases = new int[] { 72, 73, 74, 75, 76, 77, 78, 79, 80 };
            int[] secondWinCases = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
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

        private Dictionary<int, int[]> TransformFieldStateToAdjacencyMatrix(int[,] matrix)
        {
            Dictionary<int, int[]> result = new Dictionary<int, int[]>();
            for (int i = 0; i < matrix.GetLength(0); i = i + 2)
            {
                for (int j = 0; j < matrix.GetLength(1); j = j + 2)
                {
                    int x = i / 2;
                    int y = j / 2;
                    int key = x * 9 + y;
                    List<int> value = new List<int>();
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
            short x1 = wall.Start[0].X;
            short y1 = wall.Start[0].Y;
            short x2 = wall.Start[1].X;
            short y2 = wall.Start[1].Y;
            short x3 = wall.End[0].X;
            short y3 = wall.End[0].Y;
            short x4 = wall.End[1].X;
            short y4 = wall.End[1].Y;
            return new Wall(new Point[] { new Point(y1, x1), new Point(y2, x2) }, new Point[] { new Point(y3, x3), new Point(y4, x4) });
        }
    }
}
