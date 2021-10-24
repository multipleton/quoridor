using Quoridor.Core.Exceptions;

namespace Quoridor.Core.Models
{
    public abstract class Connection
    {
        private readonly string identifier;
        private short playerId;

        public string Identifier => identifier;
        public short PlayerId
        {
            get => playerId;
            set
            {
                if (playerId != -1) throw new InvalidOSPOperationException("playerId");
                playerId = value;
            }
        }

        public Connection(string identifier)
        {
            this.identifier = identifier;
            playerId = -1;
        }

        public abstract void OnConnected();

        public abstract void OnNewConnection(Connection connection);

        public abstract void OnStart(Connection current);

        public abstract void OnUpdate(State state);

        public abstract void OnWaitingForMove();

        public abstract void OnInvalidMove();

        public abstract void OnMove(Connection previous, Connection current);

        public abstract void OnFinish(Connection winner);
    }
}
