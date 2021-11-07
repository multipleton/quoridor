using System;
using Quoridor.Core;
using Quoridor.Core.Models;
using Quoridor.Console.Input;
using Quoridor.Console.Output;

namespace Quoridor.Player
{
    public class PlayerConnection : Connection
    {
        private readonly IInputHandler inputHandler;
        private readonly IOutputHandler outputHandler;

        private readonly Action<Point> onInputMove;
        private readonly Action<Wall> onInputWall;

        public PlayerConnection(GameEngine gameEngine, string identifier = "Player") : base(identifier)
        {
            inputHandler = new ConsoleInputHandler(this);
            outputHandler = new ConsoleOutputHandler(this);
            onInputMove = point => gameEngine.MakeMove(point);
            onInputWall = wall => gameEngine.MakeMove(wall);
        }

        public override void OnConnected()
        {
            outputHandler.PrintConnected();
        }

        public override void OnNewConnection(Connection connection)
        {
            outputHandler.PrintNewConnection(connection);
        }

        public override void OnStart(Connection current)
        {
            outputHandler.PrintStart(current);
        }

        public override void OnUpdate(State state)
        {
            outputHandler.PrintUpdate(state);
        }

        public override void OnWaitingForMove()
        {
            inputHandler.ReadInput(onInputMove, onInputWall);
        }

        public override void OnInvalidMove()
        {
            outputHandler.PrintInvalidMove();
            OnWaitingForMove(); // TODO: move it to gameEngine OnInvalidMove call
        }

        public override void OnMove(Connection previous, Connection current, Point point, Wall wall)
        {
            outputHandler.PrintMove(previous, current, point, wall);
        }

        public override void OnFinish(Connection winner)
        {
            outputHandler.PrintFinish(winner);
        }
    }
}
