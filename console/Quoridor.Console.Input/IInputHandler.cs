using System;
using Quoridor.Core.Models;

namespace Quoridor.Console.Input
{
    public interface IInputHandler
    {
        public void ReadInput(Action<Point> onMove, Action<Point[], Point[]> onWall);
    }
}
