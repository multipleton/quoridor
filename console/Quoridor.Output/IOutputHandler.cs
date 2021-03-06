using Quoridor.Core.Models;

namespace Quoridor.Output
{
    public interface IOutputHandler
    {
        public void PrintConnected();

        public void PrintNewConnection(Connection connection);

        public void PrintStart(Connection connection);

        public void PrintUpdate(State state);

        public void PrintMove(Connection previous, Connection current, Point oldPoint, Point point, Wall wall);
        public void PrintInvalidMove();

        public void PrintFinish(Connection winner);
    }
}
