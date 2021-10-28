using System;
using Quoridor.Core;
using Quoridor.ConsoleOutput;
using Quoridor.ConsoleInput;
using Quoridor.AI;

namespace Quoridor.ConsoleApp
{
    class App
    {
        static void Main(string[] args)
        {
            var gameEngine = GameEngine.Instance;
            var input = new Input();
            var output = new Output();
        }
    }
}
