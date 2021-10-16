using Quoridor.Core.Models;
using System.Collections.Generic;
using System;

namespace Quoridor.Core.Logic
{
    public class PathFinder
    {
        private Dictionary<int, int[]> adjacencyGraph;
        Stack<int> s = new Stack<int>();
        int[] marked = new int[81];
        public PathFinder(int[,] fieldState)
        {
            adjacencyGraph = transformFieldStateToAdjacencyMatrix(fieldState);
            marked = new int[81];
        }
        private Dictionary<int, int[]> transformFieldStateToAdjacencyMatrix(int[,] fieldState)
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

        public bool DFS(int current)
        {
            if (marked[current] != 0)
            {
                return false;
            }
            marked[current] = 1;
            if (current < 9)
            {
                return true;
            }
            for (int i = 0; i < adjacencyGraph[current].Length; ++i)
            {
                if (DFS(adjacencyGraph[current][i])) return true;
            }
            return false;
        }
    }
}
