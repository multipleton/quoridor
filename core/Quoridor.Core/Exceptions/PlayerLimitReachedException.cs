using System;

namespace Quoridor.Core.Exceptions
{
    public class PlayerLimitReachedException : ArgumentException
    {
        private const string MESSAGE = "Cannot add more players! Player limit reached: ";

        public PlayerLimitReachedException(int limit) : base(MESSAGE + limit) { }
    }
}
