using System;
using System.Text;
using Quoridor.Core.Models;

namespace Quoridor.Output
{
    public class ConsoleOutputHandler : IOutputHandler
    {
        private readonly Connection connection;

        public ConsoleOutputHandler(Connection connection)
        {
            this.connection = connection;
            Console.OutputEncoding = Encoding.UTF8;
        }

        public void PrintConnected()
        {
            PrintSeparator();
            Console.WriteLine("You are successfully connected to the game");
        }

        public void PrintNewConnection(Connection connection)
        {
            PrintSeparator();
            Console.WriteLine("New player connected to the game: " + connection.Identifier);
        }

        public void PrintStart(Connection connection)
        {
            PrintSeparator();
            Console.WriteLine("Waiting for move from: " + connection.Identifier);
        }

        public void PrintUpdate(State state)
        {
            PrintSeparator();
            string verticalNaming = "ABCDEFGHI";
            string offset = "   ";
            Console.Write(offset);
            for (int i = 1; i < 18; i++)
            {
                if (i % 2 == 0)
                {
                    Console.Write(" ");
                }
                else
                {
                    Console.Write((i - 1) / 2);
                }
            }
            Console.WriteLine();
            Console.Write(offset);
            for (int i = 0; i < 17; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
            for (int i = 1; i < 18; i++)
            {
                if (i % 2 == 0)
                {
                    Console.Write(" ");
                }
                else
                {
                    Console.Write(verticalNaming[(i - 1) / 2]);
                }
                Console.Write(" |");
                for (int j = 1; j < 18; j++)
                {
                    if (j % 2 == 0 || i % 2 == 0)
                    {
                        Wall wallOnCell = GetWallOnCell(state, j, i);
                        if (wallOnCell != null)
                        {
                            Console.Write("#");
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                    else
                    {
                        Player playerOnCell = GetPlayerOnCell(state, j, i);
                        if (playerOnCell != null)
                        {
                            Console.Write(playerOnCell.Id);
                        }
                        else
                        {
                            Console.Write("□");
                        }
                    }
                }
                Console.Write("|");
                Console.WriteLine();
            }
            Console.Write(offset);
            for (int i = 0; i < 17; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }

        public void PrintMove(Connection previous, Connection current, Point point, Wall wall)
        {
            PrintSeparator();
            Console.WriteLine("Move accepted from: " + previous.Identifier);
            Console.WriteLine("Waiting for move from: " + current.Identifier);
        }

        public void PrintInvalidMove()
        {
            PrintSeparator();
            Console.WriteLine("Invalid move! Try again.");
        }

        public void PrintFinish(Connection winner)
        {
            PrintSeparator();
            Console.WriteLine("The game finished!");
            Console.WriteLine("Winner: " + winner.Identifier);
        }

        private void PrintSeparator()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[" + connection.Identifier + "]:");
        }

        private Player GetPlayerOnCell(State state, int x, int y)
        {
            Player result = null;
            foreach (var player in state.Players)
            {
                bool isX = player.Position.X == (x - 1) / 2;
                bool isY = player.Position.Y == (y - 1) / 2;
                if (isX && isY)
                {
                    result = player;
                    break;
                }
            }
            return result;
        }

        private Wall GetWallOnCell(State state, int x, int y)
        {
            Wall result = null;
            foreach (var wall in state.Walls)
            {
                for (int i = 0; i < wall.Start.Length; i++)
                {
                    bool vertical = ((ToExtended(wall.Start[i].X) + ToExtended(wall.End[i].X)) / 2) == x;
                    bool horizontal = ((ToExtended(wall.Start[i].Y) + ToExtended(wall.End[i].Y)) / 2) == y;
                    bool xEqual = (x == ToExtended(wall.Start[i].X)) && (x == ToExtended(wall.End[i].X));
                    bool yEqual = (y == ToExtended(wall.Start[i].Y)) && (y == ToExtended(wall.End[i].Y));
                    if ((vertical && yEqual) || (horizontal && xEqual))
                    {
                        result = wall;
                        break;
                    }
                }
                if (result != null)
                {
                    break;
                }
            }
            return result;
        }

        private int ToExtended(int value)
        {
            return value == 0 ? 0 + 1: value * 2 + 1;
        }
    }
}
