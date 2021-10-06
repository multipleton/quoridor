using Quoridor.Core.Models;

namespace Quoridor.Core
{
    public class GameEngine
    {
        private string currentPlayer;
        private State state;
        private int playersCount;

        public GameEngine(int playersCount)
        {
            state = new State((short)playersCount);
        }

        public void ConnectPlayer()
        {
            if (playersCount != 4)
            {
                playersCount++;
            }

        }

        public void StartGame()
        {
            if (playersCount == 2 || playersCount == 4)
            {
                state = new State((short)playersCount);
                for (int i = 0; i < playersCount; i++)
                {
                    state.AddPlayer();
                }
            }
        }

        private void MovePlayer(Point point)
        {

        }

        private void PlaceWall(Point[] start, Point[] end)
        {

        }

        public void MakeMove(Point point)
        {
            MovePlayer(point);
        }

        public void MakeMove(Point[] start, Point[] end)
        {
            PlaceWall(start, end);
        }

        public Point[] GetAvailiblePlayerFields()
        {
            return new Point[] { };
        }

        public Point[,] GetAvailibleWallPositions()
        {
            return new Point[,] { };
        }

        public void Reset()
        {

        }

        public void EndGame()
        {

        }
    }
}
