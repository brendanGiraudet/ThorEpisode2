using NFluent;
using NUnit.Framework;

namespace ThorProgram.Tests
{
    public class GameTests
    {
        private Game _game;
        private const int HeightMap = 18;
        private const int WeightMap = 40;

        [SetUp]
        public void Setup()
        {
            _game = new Game(HeightMap , WeightMap);
        }

        [Test]
        [TestCase(3,2)]
        [TestCase(4,2)]
        [TestCase(4,3)]
        [TestCase(4,4)]
        [TestCase(3,4)]
        [TestCase(2,4)]
        [TestCase(2,3)]
        [TestCase(2,2)]
        public void ShouldTrueWhenUseIsNearByThorMethod(int giantPositionByX,int giantPositionByY)
        {
            // Arrange
            const int thorPositionByX = 3;
            const int thorPositionByY = 3;
            _game.SetContentPosition(thorPositionByX, thorPositionByY, ContentPosition.Thor);
            _game.SetContentPosition(giantPositionByX, giantPositionByY, ContentPosition.Giant);
            var expectedGiantPosition = new Position
            {
                X = giantPositionByX,
                Y = giantPositionByY
            };
            
            // Act
            var isNearByThor = _game.IsNearByThor(expectedGiantPosition);

            // Assert
            Check.That(isNearByThor).IsTrue();
        }

        [Test]
        public void ShouldFindTheNearestGiant()
        {
            // Arrange
            const int thorPositionByX = 3;
            const int thorPositionByY = 3;
            const int giantPositionByX = 4;
            const int giantPositionByY = 4;
            const int otherGiantPositionByX = 8;
            const int otherGiantPositionByY = 8;
            _game.SetContentPosition(thorPositionByX, thorPositionByY, ContentPosition.Thor);
            _game.SetContentPosition(giantPositionByX, giantPositionByY, ContentPosition.Giant);
            _game.SetContentPosition(otherGiantPositionByX, otherGiantPositionByY, ContentPosition.Giant);
            var expectedGiantPosition = new Position
            {
                X = giantPositionByX,
                Y = giantPositionByY
            };
            
            // Act
            var giantPosition = _game.FindTheNearestGiant();

            // Assert
            Check.That(giantPosition).IsEqualTo(expectedGiantPosition);
        }
        
        [Test]
        public void ShouldMoveThorToGiant()
        {
            // Arrange 
            const int thorPositionByX = 3;
            const int thorPositionByY = 3;
            const int giantPositionByX = 4;
            const int giantPositionByY = 4;
            _game.SetContentPosition(thorPositionByX, thorPositionByY, ContentPosition.Thor);
            _game.SetContentPosition(giantPositionByX, giantPositionByY, ContentPosition.Giant);
            var giantPosition = new Position
            {
                X = giantPositionByX,
                Y = giantPositionByY, 
                Content = ContentPosition.Giant
            };
 
            // Act
            var _ = _game.GetDirectionWhereThorMoveTo(giantPosition);
            var thorPosition = _game.Map.Find(location => location.Content.Equals(ContentPosition.Thor));

            // Assert
            Check.That(thorPosition).IsNotNull();
            Check.That(thorPosition).IsEqualTo(giantPosition);
        }
    }
}