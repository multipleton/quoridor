using static System.Console;
using System.Text;
using Quoridor.Core.Models;

namespace Quoridor.Console.Output
{
    public class OutputHandler
    {
        private readonly Connection connection;

        public OutputHandler(Connection connection)
        {
            this.connection = connection;
            OutputEncoding = Encoding.UTF8;
        }

        public void PrintConnected()
        {
            PrintSeparator();
            WriteLine("You are successfully connected to the game");
        }

        public void PrintNewConnection(Connection connection)
        {
            PrintSeparator();
            WriteLine("New player connected to the game: " + connection.Identifier);
        }

        public void PrintStart(Connection connection)
        {
            PrintSeparator();
            WriteLine("Waiting for move from: " + connection.Identifier);
        }

        public void PrintUpdate(State state)
        {
            PrintSeparator();
            string verticalNaming = "ABCDEFGHI";
            string offset = "   ";
            Write(offset);
            for (int i = 1; i < 18; i++)
            {
                if (i % 2 == 0)
                {
                    Write(" ");
                }
                else
                {
                    Write((i - 1) / 2);
                }
            }
            WriteLine();
            Write(offset);
            for (int i = 0; i < 17; i++)
            {
                Write("-");
            }
            WriteLine();
            for (int i = 1; i < 18; i++)
            {
                if (i % 2 == 0)
                {
                    Write(" ");
                }
                else
                {
                    Write(verticalNaming[(i - 1) / 2]);
                }
                Write(" |");
                for (int j = 1; j < 18; j++)
                {
                    if (j % 2 == 0 || i % 2 == 0)
                    {
                        Wall wallOnCell = GetWallOnCell(state, j, i);
                        if (wallOnCell != null)
                        {
                            Write("#");
                        } else
                        {
                            Write(" ");
                        }
                    }
                    else
                    {
                        Player playerOnCell = GetPlayerOnCell(state, j, i);
                        if (playerOnCell != null)
                        {
                            Write(playerOnCell.Id);
                        }
                        else
                        {
                            Write("□");
                        }
                    }
                }
                Write("|");
                WriteLine();
            }
            Write(offset);
            for (int i = 0; i < 17; i++)
            {
                Write("-");
            }
            WriteLine();
        }

        public void PrintMove(Connection previous, Connection current)
        {
            PrintSeparator();
            WriteLine("Move accepted from: " + previous.Identifier);
            WriteLine("Waiting for move from: " + current.Identifier);
        }

        public void PrintInvalidMove()
        {
            PrintSeparator();
            WriteLine("Invalid move! Try again.");
        }

        public void PrintFinish(Connection winner)
        {
            PrintSeparator();
            WriteLine("The game finished!");
            WriteLine("Winner: " + winner.Identifier);
            WriteLine("Do you want to restart? (y/n)");
        }

        private void PrintSeparator()
        {
            WriteLine();
            WriteLine();
            WriteLine("[" + connection.Identifier + "]:");
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
            return value == 0 ? 0 : value * 2 + 1;
        }
    }
}
