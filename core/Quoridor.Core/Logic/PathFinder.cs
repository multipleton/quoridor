using Quoridor.Core.Models;
using System.Collections.Generic;
using System;

namespace Quoridor.Core.Logic
{
    public class PathFinder
    {
        private Node[,] nodes;
        private int _x;
        private int _y;
        private int _finalY;
        private int[,] marked;
        public PathFinder(int[,] fieldState, Point playerPosition, int finalY)
        {
            nodes = transformFieldStateToGraph(fieldState);
            _x = playerPosition.X;
            _y = playerPosition.Y;
            _finalY = finalY;
            marked = new int[9, 9];
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

        public bool DFS(int x = 8, int y = 4)
        {
            List<Point> availableList = new List<Point>();

            if (nodes[x, y].Top == 0)
            {
                availableList.Add(new Point((short)(x - 1), (short)y));
            }
            if (nodes[x, y].Bottom == 0)
            {
                availableList.Add(new Point((short)(x + 1), (short)y));
            }
            if (nodes[x, y].Left == 0)
            {
                availableList.Add(new Point((short)x, (short)(y - 1)));
            }
            if (nodes[x, y].Right == 0)
            {
                availableList.Add(new Point((short)x, (short)(y + 1)));
            }

            var available = availableList.ToArray();

            for (int i = 0; i < available.Length; i++)
            {
                if (x == 0) Console.WriteLine("Win");
                x = available[i].X;
                y = available[i].Y;
                Console.WriteLine(x + " " + y);

                if (marked[x, y] == 1) continue;
                marked[x, y] = 1;

                if (x - 1 > 0 && nodes[x, y].Top == 0)
                {
                    DFS(x - 1, y);
                }
                if (x + 1 < 9 && nodes[x, y].Bottom == 0)
                {
                    DFS(x + 1, y);
                }
                if (y - 1 > 0 && nodes[x, y].Left == 0)
                {
                    DFS(x, y - 1);
                }
                if (y + 1 < 9 && nodes[x, y].Right == 0)
                {
                    DFS(x, y + 1);
                }
            }
            return false;
        }

        private class Node
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
