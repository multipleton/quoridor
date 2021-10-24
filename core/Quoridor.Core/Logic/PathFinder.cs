using Quoridor.Core.Models;
using System.Collections.Generic;
using System;

namespace Quoridor.Core.Logic
{
    public class PathFinder
    {
        private static Dictionary<int, int[]> adjacencyGraph;
        private static int[] marked;
        private int[,] fieldState;
        
        private static Dictionary<int, int[]> TransformFieldStateToAdjacencyMatrix(int[,] fieldState)
        {
            Dictionary<int, int[]> result = new Dictionary<int, int[]>();
            for (int i = 0; i < fieldState.GetLength(0); i = i + 2)
            {
                for (int j = 0; j < fieldState.GetLength(1); j = j + 2)
                {
                    int x = i / 2;
                    int y = j / 2;
                    int key = x * 9 + y;
                    List<int> value = new List<int>();
                    if (j + 1 < 17 && fieldState[i, j + 1] == 0)
                    {
                        value.Add(x * 9 + y + 1); 
                    }
                    if (j - 1 > 0 && fieldState[i, j - 1] == 0)
                    {
                        value.Add(x * 9 + y - 1);
                    }
                    if (i + 1 < 17 && fieldState[i + 1, j] == 0)
                    {
                        value.Add((x + 1) * 9 + y);
                    }
                    if (i - 1 > 0 && fieldState[i - 1, j] == 0)
                    {
                        value.Add((x - 1) * 9 + y);
                    }
                    result.Add(key, value.ToArray());
                }
            }
            return result;
        }
        private static bool DFS(int current, int[] winCases)
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

        private static int[,] AddWallsAndPlayersToMatrix(State state)
        {
            int[,] matrix = new int[17, 17];
            Player[] players = state.Players;
            Wall[] walls = state.Walls;
            for (int i = 0; i < players.Length; i++)
            {
                int x = players[i].Position.X;
                int y = players[i].Position.Y;
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

        public bool HasAvailablePaths(State state)
        {
            fieldState = AddWallsAndPlayersToMatrix(state);
            adjacencyGraph = TransformFieldStateToAdjacencyMatrix(fieldState);

            Player[] players = state.Players;
            int[] firstWinCases = new int[] { 72, 73, 74, 75, 76, 77, 78, 79, 80 };
            int[] secondWinCases = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

            marked = new int[81];
            int x = players[0].Position.X;
            int y = players[0].Position.Y;
            if (!DFS(x * 9 + y, firstWinCases)) return false;

            marked = new int[81];
            int x2 = players[1].Position.X;
            int y2 = players[1].Position.Y;
            if (!DFS(x2 * 9 + y2, secondWinCases)) return false;

            return true;
        }
    }
}
