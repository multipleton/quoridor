namespace Quoridor.Core
{
    class State
    {
        private readonly Player[] players;
        private readonly Wall[] walls;

        public Player[] Players => players;
        public Wall[] Walls => walls;
    }
}
