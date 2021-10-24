using static System.Console;
using Quoridor.Core;
using Quoridor.Player;
using Quoridor.AI;

namespace Quoridor.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            GameEngine gameEngine = GameEngine.Instance;
            gameEngine.Initialize(2);
            RandomBot randomBot = new RandomBot(gameEngine);
            PlayerConnection playerConnection = new PlayerConnection(gameEngine);
            gameEngine.Connect(playerConnection);
            // gameEngine.Connect(randomBot);
            gameEngine.Connect(new PlayerConnection(gameEngine));
            gameEngine.Start();
        }
    }
}
