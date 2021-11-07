﻿using System;
using Quoridor.Core.Models;
using static System.Console;

namespace Quoridor.Console.Output
{
    public class ExternalOutputHandler : IOutputHandler
    {
        private enum StringifyType
        {
            CELL,
            CROSSING,
        }

        private readonly Connection connection;

        public ExternalOutputHandler(Connection connection)
        {
            this.connection = connection;
        }

        public void PrintConnected() { }

        public void PrintFinish(Connection winner) { }

        public void PrintInvalidMove() { }

        public void PrintMove(Connection previous, Connection current, Point point, Wall wall)
        {
            if (previous == connection) return;
            string move;
            if (point != null)
            {
                move = "move " + StringifyPoint(point, StringifyType.CELL);
            }
            else if (wall != null)
            {
                move = "wall " + StringifyWall(wall);
            }
            else
            {
                throw new ArgumentException("Received invalid move!");
            }
            WriteLine(move);
        }

        public void PrintNewConnection(Connection connection) { }

        public void PrintStart(Connection connection) { }

        public void PrintUpdate(State state) { }

        private string StringifyPoint(Point point, StringifyType stringifyType)
        {
            string result = "";
            string horizontalNaming;
            switch (stringifyType)
            {
                default:
                case StringifyType.CELL:
                    horizontalNaming = "ABCDEFGHI";
                    break;
                case StringifyType.CROSSING:
                    horizontalNaming = "STUVWXYZ";
                    break;
            }
            result += horizontalNaming[point.X];
            result += point.Y + 1;
            return result;
        }

        private string StringifyWall(Wall wall)
        {
            string result = "";
            result += StringifyPoint(wall.Start[0], StringifyType.CROSSING);
            result += wall.Start[0].Y == wall.Start[1].Y ? "h" : "v";
            return result;
        }
    }
}
