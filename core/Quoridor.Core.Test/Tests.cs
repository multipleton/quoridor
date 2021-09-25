using NUnit.Framework;

namespace Quoridor.Core.Test
{
    public class Tests
    {
        private GameEngine gameEngine;

        [SetUp]
        public void Setup()
        {
            gameEngine = new GameEngine();
        }

        [Test]
        public void StubTest() // TODO: should be removed
        {
            string expected = "GameEngine";
            string actual = gameEngine.Stub();
            Assert.AreEqual(expected, actual);
        }
    }
}
