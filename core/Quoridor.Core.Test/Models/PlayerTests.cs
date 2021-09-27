using NUnit.Framework;
using Quoridor.Core.Models;

namespace Quoridor.Core.Test
{
    public class PlayerTests
    {
        [Test]
        public void Constructor_Not_Null_Test()
        {
            Player player = new Player(0, new Point(5, 0), 10);
            Assert.NotNull(player);
        }
        [Test]
        public void Getter_Test()
        {
            Player player = new Player(0, new Point(5, 0), 10);
            Assert.AreEqual(player.Id, 0);
            Assert.AreEqual(player.Position.X, 5);
            Assert.AreEqual(player.Position.Y, 0);
            Assert.AreEqual(player.WallsCount, 10);
        }
        [Test]
        public void Move_Test()
        {
            Player player = new Player(0, new Point(5, 0), 10);
            player.Move(5, 1);
            Assert.AreEqual(player.Position.Y, 1);
            player.Move(6, 1);
            Assert.AreEqual(player.Position.X, 6);
        }
        [Test]
        public void ReduceWallCount_Test()
        {
            Player player1 = new Player(0, new Point(5, 0), 10);
            Assert.AreEqual(player1.ReduceWallsCount(), true);
            Assert.AreEqual(player1.WallsCount, 9);
            Player player2 = new Player(0, new Point(5, 0), 0);
            Assert.AreEqual(player2.ReduceWallsCount(), false);
            Assert.AreEqual(player2.WallsCount, 0);
        }
    }
}
