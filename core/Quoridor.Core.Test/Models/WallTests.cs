using NUnit.Framework;
using Quoridor.Core.Models;
using System;

namespace Quoridor.Core.Test
{
    public class WallTests
    {
        [Test]
        public void Constructor_Not_Null_Test()
        {
            Point[] start = { new Point(1, 1), new Point(1, 2) };
            Point[] end = { new Point(2, 1), new Point(2, 2) };
            Assert.DoesNotThrow(() => new Wall(start, end));
        }

        [Test]
        public void Getter_Test()
        {
            Point[] start = { new Point(1, 1), new Point(1, 2) };
            Point[] end = { new Point(2, 1), new Point(2, 2) };
            Wall wall = new Wall(start, end);
            Assert.AreEqual(wall.Start[0].X, start[0].X);
            Assert.AreEqual(wall.Start[0].Y, start[0].Y);
            Assert.AreEqual(wall.Start[1].X, start[1].X);
            Assert.AreEqual(wall.Start[1].Y, start[1].Y);
            Assert.AreEqual(wall.End[0].X, end[0].X);
            Assert.AreEqual(wall.End[0].Y, end[0].Y);
            Assert.AreEqual(wall.End[1].X, end[1].X);
            Assert.AreEqual(wall.End[1].Y, end[1].Y);
        }
    }
}
