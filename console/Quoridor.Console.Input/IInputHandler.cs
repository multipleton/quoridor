using System;
using Quoridor.Core.Models;

namespace Quoridor.Input
{
    public interface IInputHandler
    {
        public void ReadInput(Action<Point> onMove, Action<Wall> onWall);
    }
}
