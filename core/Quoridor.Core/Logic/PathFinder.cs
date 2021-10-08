namespace Quoridor.Core.Logic
{
    public class PathFinder
    {
        private static Node[,] transformFieldStateToGraph(int[,] fieldState)
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
