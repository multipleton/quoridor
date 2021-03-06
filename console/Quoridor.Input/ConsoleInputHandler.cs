using System;
using Quoridor.Core.Models;

namespace Quoridor.Input
{
    public class ConsoleInputHandler : IInputHandler
    {
        private readonly Connection connection;

        public ConsoleInputHandler(Connection connection)
        {
            this.connection = connection;
        }

        public void ReadInput(Action<Point> onMove, Action<Wall> onWall)
        {
            bool error = false;
            Console.WriteLine();
            Console.Write("[" + connection.Identifier + " (" + connection.Player.Id + ")] > ");
            string command = Console.ReadLine();
            var splitCommand = command.Split(new char[0]);
            switch (splitCommand[0].ToLower())
            {
                case "move":
                    error = !HandleMove(splitCommand, onMove);
                    break;
                case "wall":
                    error = !HandleWall(splitCommand, onWall);
                    break;
                default:
                    error = true;
                    break;
            }
            if (error)
            {
                Console.WriteLine("Invalid command!");
                ReadInput(onMove, onWall);
            }
        }

        private bool HandleMove(string[] command, Action<Point> onMove)
        {
            if (command.Length != 2) return false;
            Point point = ParsePoint(command[1]);
            if (point == null) return false;
            onMove(point);
            return true;
        }

        private bool HandleWall(string[] command, Action<Wall> onWall)
        {
            if (command.Length != 5) return false;
            Point[] start =
            {
                ParsePoint(command[1]),
                ParsePoint(command[2]),
            };
            Point[] end =
            {
                ParsePoint(command[3]),
                ParsePoint(command[4]),
            };
            for (int i = 0; i < start.Length; i++)
            {
                if (start[i] == null || end[i] == null) return false;
            }
            Wall wall = new Wall(start, end);
            onWall(wall);
            return true;
        }

        private Point ParsePoint(string input)
        {
            string verticalNaming = "ABCDEFGHI";
            int x;
            try
            {
                x = int.Parse(input[1].ToString());
            }
            catch (Exception)
            {
                return null;
            }
            int y = verticalNaming.IndexOf(char.ToUpper(input[0]));
            if (y == -1) return null;
            return new Point(x, y);
        }
    }
}
