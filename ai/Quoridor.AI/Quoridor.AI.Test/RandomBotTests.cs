using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using Quoridor.Core;
using Quoridor.Core.Models;

namespace Quoridor.AI.Test
{
    public class RandomBotTests
    {
        private Mock<GameEngine> gameEngineMock;
        private Mock<State> stateMock;
        private Point position;
        private Player player;

        private RandomBot randomBot;

        [SetUp]
        public void Setup()
        {
            gameEngineMock = new Mock<GameEngine>();
            stateMock = new Mock<State>((short)2);
            position = new Point(4, 4);
            player = new Player(0, position, 10);
            randomBot = new RandomBot(gameEngineMock.Object);
            randomBot.PlayerId = 0;
            randomBot.OnUpdate(stateMock.Object);
        }

        [Test]
        public void TestOnWaitingForMove()
        {
            List<Point> possiblePlayerMoves = new List<Point>();
            possiblePlayerMoves.Add(new Point((short)(position.X + 1), position.Y));
            possiblePlayerMoves.Add(new Point((short)(position.X - 1), position.Y));
            possiblePlayerMoves.Add(new Point(position.X, (short)(position.Y + 1)));
            possiblePlayerMoves.Add(new Point(position.X, (short)(position.Y - 1)));
            possiblePlayerMoves.Add(new Point((short)(position.X + 1), (short)(position.Y + 1)));
            possiblePlayerMoves.Add(new Point((short)(position.X + 1), (short)(position.Y - 1)));
            possiblePlayerMoves.Add(new Point((short)(position.X - 1), (short)(position.Y + 1)));
            possiblePlayerMoves.Add(new Point((short)(position.X - 1), (short)(position.Y - 1)));
            gameEngineMock
                .Setup(i => i.MakeMove(It.IsAny<Point>()))
                .Callback<Point>(
                    point =>
                    {
                        bool isContains = false;
                        foreach (var entry in possiblePlayerMoves)
                        {
                            if (entry.X == point.X && entry.Y == point.Y)
                            {
                                isContains = true;
                            }
                        }
                        Assert.IsTrue(isContains);
                    }
                );
            gameEngineMock
                .Setup(i => i.MakeMove(It.IsAny<Point[]>(), It.IsAny<Point[]>()))
                .Callback<Point[], Point[]>(
                    (start, end) =>
                    {
                        bool direction = start[0].X == end[0].X;
                        int offsetX = direction ? 0 : 1;
                        int offsetY = direction ? 1 : 0;
                        Assert.IsTrue(start[0].X + offsetX == start[1].X);
                        Assert.IsTrue(start[0].Y + offsetY == start[1].Y);
                        Assert.IsTrue(end[0].X + offsetX == end[1].X);
                        Assert.IsTrue(end[0].Y + offsetY == end[1].Y);
                    }
                );
            stateMock.Setup(i => i.GetPlayer(0)).Returns(player);
            randomBot.OnWaitingForMove();
        }

        [Test]
        public void TestOnInvalidMove()
        {
            gameEngineMock.Verify(i => i.MakeMove(It.IsAny<Point>()), Times.Once);
            stateMock.Setup(i => i.GetPlayer(0)).Returns(player);
            randomBot.OnInvalidMove();
        }
    }
}
