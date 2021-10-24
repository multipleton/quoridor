using Quoridor.Core;
using Quoridor.Core.Models;
using Quoridor.Console.Output;

namespace Quoridor.Player
{
    public class PlayerConnection : Connection
    {
        private readonly GameEngine gameEngine;
        private readonly OutputHandler outputHandler;

        public PlayerConnection(GameEngine gameEngine) : base("player") {
            this.gameEngine = gameEngine;
            outputHandler = new OutputHandler();
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
            // TODO: call input handler
        }

        public override void OnInvalidMove()
        {
            outputHandler.PrintInvalidMove();
            // TODO: call input handler
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
