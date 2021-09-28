using NUnit.Framework;
using Quoridor.Core.Models;
using System;

namespace Quoridor.Core.Test
{
    public class StateTests
    {
        [Test]
        public void Constructor_Test()
        {
            State state2 = new State(2);
            Assert.NotNull(state2);
            State state4 = new State(4);
            Assert.NotNull(state4);
            Assert.Throws<ArgumentException>(() => new State(5));
        }
    }
}
