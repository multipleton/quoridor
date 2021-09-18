using static System.Console;

using Quoridor.Console.Input;
using Quoridor.Console.Output;

namespace Quoridor.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            InputHandler inputHandler = new InputHandler();
            OutputHandler outputHandler = new OutputHandler();
            WriteLine(inputHandler.Stub());
            WriteLine(outputHandler.Stub());
        }
    }
}
