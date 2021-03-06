using System;

namespace Quoridor.Core.Exceptions
{
    public class PlayerNotFoundException : NullReferenceException
    {
        private const string MESSAGE = "Cannot find player with such id: ";

        public PlayerNotFoundException(int id) : base(MESSAGE + id) { }
    }
}
