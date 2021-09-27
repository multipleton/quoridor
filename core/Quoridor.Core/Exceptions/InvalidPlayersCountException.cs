using System;

namespace Quoridor.Core.Exceptions
{
    class InvalidPlayersCountException : ArgumentException
    {
        private const string MESSAGE = "Invalid players count! Must be 2 or 4, but provided: ";

        public InvalidPlayersCountException(int count) : base(MESSAGE + count) { }
    }
}
