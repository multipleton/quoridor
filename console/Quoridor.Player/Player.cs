using System;
using Quoridor.Core;
using Quoridor.Core.Models;
using Quoridor.Console.Input;
using Quoridor.Console.Output;

namespace Quoridor.Player
{
    public class PlayerConnection : Connection
    {
        private readonly GameEngine gameEngine;
        private readonly InputHandler inputHandler;
        private readonly OutputHandler outputHandler;

        private readonly Action<Point> onInputMove;
        private readonly Action<Point[], Point[]> onInputWall;

        public PlayerConnection(GameEngine gameEngine) : base("player") {
            this.gameEngine = gameEngine;
            inputHandler = new InputHandler();
            outputHandler = new OutputHandler();
            onInputMove = point => gameEngine.MakeMove(point);
            onInputWall = (start, end) => gameEngine.MakeMove(start, end);
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

        public override void OnMove(Connection previous, Connection current)
        {
            outputHandler.PrintMove(previous, current);
        }

        public override void OnFinish(Connection winner)
        {
            outputHandler.PrintFinish(winner);
            // TODO: call input handler
        }
    }
}
