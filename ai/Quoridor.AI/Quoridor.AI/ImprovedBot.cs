using Quoridor.Core;
using Quoridor.Core.Models;

namespace Quoridor.AI
{
    public class ImprovedBot : Connection
    {
        private readonly GameEngine gameEngine;

        public ImprovedBot(GameEngine gameEngine) : base("Improved Bot")
        {
            this.gameEngine = gameEngine;
        }

        public override void OnConnected()
        {
            throw new System.NotImplementedException();
        }

        public override void OnFinish(Connection winner)
        {
            throw new System.NotImplementedException();
        }

        public override void OnInvalidMove()
        {
            throw new System.NotImplementedException();
        }

        public override void OnMove(Connection previous, Connection current, Point oldPoint, Point point, Wall wall)
        {
            throw new System.NotImplementedException();
        }

        public override void OnNewConnection(Connection connection)
        {
            throw new System.NotImplementedException();
        }

        public override void OnStart(Connection current)
        {
            throw new System.NotImplementedException();
        }

        public override void OnUpdate(State state)
        {
            throw new System.NotImplementedException();
        }

        public override void OnWaitingForMove()
        {
            throw new System.NotImplementedException();
        }
    }
}
