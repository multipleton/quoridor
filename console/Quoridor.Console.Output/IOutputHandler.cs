using Quoridor.Core.Models;

namespace Quoridor.Console.Output
{
    public interface IOutputHandler
    {
        public void PrintConnected();

        public void PrintNewConnection(Connection connection);

        public void PrintStart(Connection connection);

        public void PrintUpdate(State state);

        public void PrintMove(Connection previous, Connection current, Point point, Wall wall);
        public void PrintInvalidMove();

        public void PrintFinish(Connection winner);
    }
}
