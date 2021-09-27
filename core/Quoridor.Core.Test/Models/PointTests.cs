using NUnit.Framework;
using Quoridor.Core.Models;

namespace Quoridor.Core.Test
{
    public class PointTests
    {
        private GameEngine gameEngine;

        [Test]
        public void Constructor_Not_Null_Test() // TODO: should be removed
        {
            Point point = new Point(5, 4);
            Assert.NotNull(point);
        }
        [Test]
        public void Getter_Test()
        {
            Point point = new Point(5, 4);
            Assert.AreEqual(point.X, 5);
            Assert.AreEqual(point.Y, 4);
        }
        [Test]
        public void Settter_Test()
        {
            Point point = new Point(5, 4);
            point.X = 1;
            Assert.AreEqual(point.X, 1);
            point.Y = 2;
            Assert.AreEqual(point.Y, 2);
        } 
    }
}
