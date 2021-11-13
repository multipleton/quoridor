using Quoridor.Core.Exceptions;

namespace Quoridor.Core.Models
{
    public abstract class Connection
    {
        private readonly string identifier;
        private Player player;

        public string Identifier => identifier;
        public Player Player
        {
            get => player;
            set
            {
                if (player != null) throw new InvalidOSPOperationException("player");
                player = value;
            }
        }

        public Connection(string identifier)
        {
            this.identifier = identifier;
            player = null;
        }

        public abstract void OnConnected();

        public abstract void OnNewConnection(Connection connection);

        public abstract void OnStart(Connection current);

        public abstract void OnUpdate(State state);

        public abstract void OnWaitingForMove();

        public abstract void OnInvalidMove();

        public abstract void OnMove(Connection previous, Connection current, Point point, Wall wall);

        public abstract void OnFinish(Connection winner);
    }
}
