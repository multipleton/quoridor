using static System.Console;
using System;
using Quoridor.Core.Models;

namespace Quoridor.Console.Input
{
    public class InputHandler
    {
        public void ReadInput(Action<Point> onMove, Action<Point[], Point[]> onWall)
        {
            Write("> ");
            string command = ReadLine();
            var splitCommand = command.Split(new char[0]);
            switch (splitCommand[0].ToLower())
            {
                case "move":
                    Point point = ParsePoint(splitCommand[1]);
                    onMove(point);
                    break;
                case "wall":
                    Point[] start =
                    {
                        ParsePoint(splitCommand[1]),
                        ParsePoint(splitCommand[2]),
                    };
                    Point[] end =
                    {
                        ParsePoint(splitCommand[3]),
                        ParsePoint(splitCommand[4]),
                    };
                    onWall(start, end);
                    break;
            }
        }

        private Point ParsePoint(string input)
        {
            string verticalNaming = "ABCDEFGHI";
            int x = int.Parse(input[1].ToString());
            int y = verticalNaming.IndexOf(char.ToUpper(input[0]));
            return new Point((short)x, (short)y);
        }
    }
}
