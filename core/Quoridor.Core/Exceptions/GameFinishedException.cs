using System;

namespace Quoridor.Core.Exceptions
{
    public class GameFinishedException : InvalidOperationException
    {
        private const string MESSAGE = "Cannot perform operation, the game is finished!";

        public GameFinishedException() : base(MESSAGE) { }
    }
}
