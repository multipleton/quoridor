using static System.Console;
using System;
using Quoridor.Core.Models;

namespace Quoridor.Console.Input
{
    public class ExternalInputHandler : IInputHandler
    {
        private enum ParsingType
        {
            POINT,
            CROSSING,
        }

        public void ReadInput(Action<Point> onMove, Action<Point[], Point[]> onWall)
        {
            bool error = false;
            Write("-> ");
            string command = ReadLine();
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
                ReadInput(onMove, onWall);
            }
        }

        private bool HandleMove(string[] command, Action<Point> onMove)
        {
            if (command.Length != 2) return false;
            Point point = Parse(command[1], ParsingType.POINT);
            if (point == null) return false;
            onMove(point);
            return true;
        }

        private bool HandleWall(string[] command, Action<Point[], Point[]> onWall)
        {
            if (command.Length != 2) return false;
            if (command[1].Length != 3) return false;
            string position = command[1].Substring(0, 2);
            string type = command[1].Substring(2, 1);
            Point crossing = Parse(position, ParsingType.CROSSING);
            if (crossing == null) return false;
            int offsetX = type == "h" ? 1 : 0;
            int offsetY = type == "v" ? 1 : 0;
            Point[] start =
            {
                new Point(crossing.X, crossing.Y),
                new Point((short)(crossing.X + offsetX), (short)(crossing.Y + offsetY)),
            };
            Point[] end =
            {
                new Point((short)(crossing.X + offsetY), (short)(crossing.Y + offsetX)),
                new Point((short)(crossing.X + 1), (short)(crossing.Y + 1)),
            };
            onWall(start, end);
            return true;
        }

        private Point Parse(string input, ParsingType parsingType)
        {
            string horizontalNaming;
            switch (parsingType)
            {
                default:
                case ParsingType.POINT:
                    horizontalNaming = "ABCDEFGHI";
                    break;
                case ParsingType.CROSSING:
                    horizontalNaming = "STUVWXYZ";
                    break;
            }
            int x = horizontalNaming.IndexOf(char.ToUpper(input[0]));
            int y;
            try
            {
                y = int.Parse(input[1].ToString()) - 1;
            }
            catch (Exception)
            {
                return null;
            }
            if (y == -1) return null;
            return new Point((short)x, (short)y);
        }
    }
}
