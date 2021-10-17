using System;

namespace Quoridor.Core.Exceptions
{
    /// <summary>
    ///     The exception that is thrown when trying to set
    ///     'once set parameter' that already setted.
    /// </summary>
    class InvalidOSPOperationException : InvalidOperationException
    {
        private const string MESSAGE = "Trying to set 'once set parameter' that already setted: ";

        public InvalidOSPOperationException(string name) : base(MESSAGE + name) { }
    }
}
