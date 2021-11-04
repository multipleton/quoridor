using System;
using Quoridor.Console.Input;
using Quoridor.Core;
using Quoridor.Core.Models;

namespace Quoridor.ExternalInterface
{
    public class ExternalConnection : Connection
    {
        private readonly ExternalInputHandler externalInputHandler;

        private readonly Action<Point> onInputMove;
        private readonly Action<Point[], Point[]> onInputWall;

        public ExternalConnection(GameEngine gameEngine) : base("External")
        {
            this.externalInputHandler = new ExternalInputHandler();
            onInputMove = point => gameEngine.MakeMove(point);
            onInputWall = (start, end) => gameEngine.MakeMove(start, end);
        }

        public override void OnConnected() { }

        public override void OnFinish(Connection winner) { }

        public override void OnInvalidMove() { }

        public override void OnMove(Connection previous, Connection current) { }

        public override void OnNewConnection(Connection connection) { }

        public override void OnStart(Connection current) { }

        public override void OnUpdate(State state) { }

        public override void OnWaitingForMove()
        {
            externalInputHandler.ReadInput(onInputMove, onInputWall);
        }
    }
}
