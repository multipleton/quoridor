using NUnit.Framework;

namespace Quoridor.Core.Test
{
    public class GameEngineTests
    {
        [Test]
        public void Stub()
        {
            var game = new GameEngine(2);
            Assert.True(game.Dejkstra());
        }
    }
}
