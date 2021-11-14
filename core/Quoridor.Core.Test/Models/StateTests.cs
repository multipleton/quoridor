using NUnit.Framework;
using Quoridor.Core.Models;
using Quoridor.Core.Exceptions;

namespace Quoridor.Core.Test
{
    public class StateTests
    {
        [Test]
        public void Constructor_Test()
        {
            Assert.DoesNotThrow(() => new State(2));
            Assert.DoesNotThrow(() => new State(4));
            Assert.Throws<InvalidPlayersCountException>(() => new State(5));
        }

        [Test]
        public void Getter_AddWall_Test()
        {
            State state2 = new State(2);
            state2.AddPlayer();
            Point[] start = { new Point(1, 1), new Point(1, 2) };
            Point[] end = { new Point(2, 1), new Point(2, 2) };
            Wall wall = new Wall(start, end);
            state2.AddWall(wall);
            Assert.AreEqual(state2.Players.Length, 1);
            Assert.AreEqual(state2.Walls.Length, 1);
            Assert.AreEqual(state2.Walls[0].Start[0].X, 1);
            Assert.AreEqual(state2.Walls[0].Start[0].Y, 1);
            Assert.AreEqual(state2.Walls[0].Start[1].X, 1);
            Assert.AreEqual(state2.Walls[0].Start[1].Y, 2);
            Assert.AreEqual(state2.Walls[0].End[0].X, 2);
            Assert.AreEqual(state2.Walls[0].End[0].Y, 1);
            Assert.AreEqual(state2.Walls[0].End[1].X, 2);
            Assert.AreEqual(state2.Walls[0].End[1].Y, 2);
        }

        [Test]
        public void AddPlayer_Test()
        {
            State state2 = new State(2);
            Assert.AreEqual(state2.Players.Length, 0);
            state2.AddPlayer();
            Assert.AreEqual(state2.Players.Length, 1);
            state2.AddPlayer();
            Assert.Throws<PlayerLimitReachedException>(() => state2.AddPlayer());
            State state4 = new State(4);
            Assert.AreEqual(state4.Players.Length, 0);
            state4.AddPlayer();
            Assert.AreEqual(state4.Players.Length, 1);
            state4.AddPlayer();
            Assert.AreEqual(state4.Players.Length, 2);
            state4.AddPlayer();
            Assert.AreEqual(state4.Players.Length, 3);
            state4.AddPlayer();
            Assert.AreEqual(state4.Players.Length, 4);
            Assert.Throws<PlayerLimitReachedException>(() => state4.AddPlayer());
        }

        [Test]
        public void AddWall()
        {
            State state2 = new State(2);
            Point[] start = { new Point(1, 1), new Point(1, 2) };
            Point[] end = { new Point(2, 1), new Point(2, 2) };
            Wall wall = new Wall(start, end);
            for (int i = 0; i < 20; i++)
            {
                state2.AddWall(wall);
            }
            Assert.Throws<WallLimitReachedException>(() => state2.AddWall(wall));
        }
    }
}
