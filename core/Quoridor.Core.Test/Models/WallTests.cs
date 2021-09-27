using NUnit.Framework;
using Quoridor.Core.Models;

namespace Quoridor.Core.Test
{
    public class WallTests
    {
        [Test]
        public void Constructor_Not_Null_Test()
        {
            Point[] start = { new Point(1, 1), new Point(1, 2) };
            Point[] end = { new Point(2, 1), new Point(2, 2) };
            Wall wall = new Wall(start, end);
            Assert.NotNull(wall);
        }
        [Test]
        public void Constructor_Null_Arguments_Test()
        {
            Wall wall = new Wall(null, null);
            Assert.NotNull(wall);
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
        [Test]
        public void Change_Point_Field_By_Get_Object_Test()
        {
            Point[] start = { new Point(1, 1), new Point(1, 2) };
            Point[] end = { new Point(2, 1), new Point(2, 2) };
            Wall wall = new Wall(start, end);
            wall.Start[0].X = 5;
            Assert.AreNotEqual(wall.Start[0].X, 5);
        }
    }
}
