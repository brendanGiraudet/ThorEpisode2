using System.Collections.Generic;
using NFluent;
using NUnit.Framework;

namespace ThorProgram.Tests
{
    public class GameTests
    {
        private Game _game;

        [SetUp]
        public void Setup()
        {
            _game = new Game();
        }

        [Test]
        [TestCase(3, 2)]
        [TestCase(4, 2)]
        [TestCase(4, 3)]
        [TestCase(4, 4)]
        [TestCase(3, 4)]
        [TestCase(2, 4)]
        [TestCase(2, 3)]
        [TestCase(2, 2)]
        public void ShouldCurrentPositionWasBesideOfExpectedPosition(int giantPositionByX, int giantPositionByY)
        {
            // Arrange
            const int thorPositionByX = 3;
            const int thorPositionByY = 3;
            var thorPosition = new Position
            {
                X = thorPositionByX,
                Y = thorPositionByY
            };
            var expectedGiantPosition = new Position
            {
                X = giantPositionByX,
                Y = giantPositionByY
            };

            // Act
            var isBeside = _game.IsBeside(thorPosition, expectedGiantPosition);

            // Assert
            Check.That(isBeside).IsTrue();
        }

        [Test]
        public void ShouldFindTheNearestPosition()
        {
            // Arrange
            const int thorPositionByX = 3;
            const int thorPositionByY = 3;
            const int giantPositionByX = 4;
            const int giantPositionByY = 4;
            const int otherGiantPositionByX = 8;
            const int otherGiantPositionByY = 8;
            var thorPosition = new Position
            {
                X = thorPositionByX,
                Y = thorPositionByY
            };
            var expectedGiantPosition = new Position
            {
                X = giantPositionByX,
                Y = giantPositionByY
            };
            var otherGiantPosition = new Position
            {
                X = otherGiantPositionByX,
                Y = otherGiantPositionByY
            };
            var giants = new List<Position>
            {
                expectedGiantPosition,
                otherGiantPosition
            };

            // Act
            var giantPosition = _game.FindTheNearestPosition(thorPosition, giants);

            // Assert
            Check.That(giantPosition).IsEqualTo(expectedGiantPosition);
        }

        [Test]
        public void ShouldMoveInTheRightDirection()
        {
            // Arrange 
            const int thorPositionByX = 3;
            const int thorPositionByY = 3;
            const int giantPositionByX = 4;
            const int giantPositionByY = 4;
            var thorPosition = new Position
            {
                X = thorPositionByX,
                Y = thorPositionByY
            };
            var giantPosition = new Position
            {
                X = giantPositionByX,
                Y = giantPositionByY
            };
            const string rightDirection = "SE";

            // Act
            var direction = _game.GetDirectionWhereMoveTo(thorPosition, giantPosition);

            // Assert
            Check.That(direction).IsEqualTo(rightDirection);
        }

        [Test]
        public void ShouldFindTheRightPositionToKillGiantInMinimumStrike()
        {
            // Arrange
            var thorPosition = new Position
            {
                X = 3,
                Y = 3
            };
            var giants = new List<Position>
            {
                new Position
                {
                    X = 2,
                    Y = 4,
                },
                new Position
                {
                    X = 5,
                    Y = 2
                }
            };
            var numberOfStrike = 1;
            var expectedPosition = new Position
            {
                X = 3,
                Y = 2
            };

            // Act
            var rightPosition = _game.FindTheRightPosition(thorPosition, giants, numberOfStrike);

            // Assert
            Check.That(rightPosition).IsEqualTo(expectedPosition);
        }
    }
}