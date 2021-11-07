using System;
using Quoridor.Input;
using Quoridor.Output;
using Quoridor.Core;
using Quoridor.Core.Models;

namespace Quoridor.ExternalInterface
{
    public class ExternalConnection : Connection
    {
        private readonly IInputHandler inputHandler;
        private readonly IOutputHandler outputHandler;

        private readonly Action<Point> onInputMove;
        private readonly Action<Wall> onInputWall;

        public ExternalConnection(GameEngine gameEngine) : base("External")
        {
            inputHandler = new ExternalInputHandler();
            outputHandler = new ExternalOutputHandler(this);
            onInputMove = point => gameEngine.MakeMove(point);
            onInputWall = wall => gameEngine.MakeMove(wall);
        }

        public override void OnConnected() { }

        public override void OnFinish(Connection winner) { }

        public override void OnInvalidMove() { }

        public override void OnMove(Connection previous, Connection current, Point point, Wall wall)
        {
            outputHandler.PrintMove(previous, current, point, wall);
        }

        public override void OnNewConnection(Connection connection) { }

        public override void OnStart(Connection current) { }

        public override void OnUpdate(State state) { }

        public override void OnWaitingForMove()
        {
            inputHandler.ReadInput(onInputMove, onInputWall);
        }
    }
}
