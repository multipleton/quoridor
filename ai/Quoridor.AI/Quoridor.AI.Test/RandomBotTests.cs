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
            stateMock = new Mock<State>(2);
            position = new Point(4, 4);
            player = new Player(0, position, 10);
            randomBot = new RandomBot(gameEngineMock.Object);
            randomBot.PlayerId = player.Id;
            randomBot.OnUpdate(stateMock.Object);
        }

        [Test]
        public void TestOnWaitingForMove()
        {
            List<Point> possiblePlayerMoves = new List<Point>();
            possiblePlayerMoves.Add(new Point(position.X + 1, position.Y));
            possiblePlayerMoves.Add(new Point(position.X - 1, position.Y));
            possiblePlayerMoves.Add(new Point(position.X, position.Y + 1));
            possiblePlayerMoves.Add(new Point(position.X, position.Y - 1));
            possiblePlayerMoves.Add(new Point(position.X + 1, position.Y + 1));
            possiblePlayerMoves.Add(new Point(position.X + 1, position.Y - 1));
            possiblePlayerMoves.Add(new Point(position.X - 1, position.Y + 1));
            possiblePlayerMoves.Add(new Point(position.X - 1, position.Y - 1));
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
                                break;
                            }
                        }
                        Assert.IsTrue(isContains);
                    }
                );
            gameEngineMock
                .Setup(i => i.MakeMove(It.IsAny<Wall>()))
                .Callback<Wall>(
                    wall =>
                    {
                        bool direction = wall.Start[0].X == wall.End[0].X;
                        int offsetX = direction ? 0 : 1;
                        int offsetY = direction ? 1 : 0;
                        Assert.IsTrue(wall.Start[0].X + offsetX == wall.Start[1].X);
                        Assert.IsTrue(wall.Start[0].Y + offsetY == wall.Start[1].Y);
                        Assert.IsTrue(wall.End[0].X + offsetX == wall.End[1].X);
                        Assert.IsTrue(wall.End[0].Y + offsetY == wall.End[1].Y);
                    }
                );
            stateMock.Setup(i => i.GetPlayer(player.Id)).Returns(player);
            randomBot.OnWaitingForMove();
        }

        [Test]
        public void TestOnInvalidMove()
        {
            bool isMethodCalled = false;
            gameEngineMock
                .Setup(i => i.MakeMove(It.IsAny<Point>()))
                .Callback<Point>(_ => isMethodCalled = !isMethodCalled);
            gameEngineMock
                .Setup(i => i.MakeMove(It.IsAny<Wall>()))
                .Callback<Wall>(_ => isMethodCalled = !isMethodCalled);
            stateMock.Setup(i => i.GetPlayer(player.Id)).Returns(player);
            randomBot.OnInvalidMove();
            Assert.IsTrue(isMethodCalled);
        }
    }
}
