using System;

namespace Quoridor.Core.Exceptions
{
    public class WallLimitReachedException : ArgumentException
    {
        private const string MESSAGE = "Cannot add more walls! Wall limit reached: ";

        public WallLimitReachedException(int limit) : base(MESSAGE + limit) { }
    }
}
