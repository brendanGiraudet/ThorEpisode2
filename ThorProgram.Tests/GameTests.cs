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
        public void shouldThorIntheMap()
        {
            // Arrange
            const int thorPositionByX = 3;
            const int thorPositionByY = 3;


            // Act
            _game.SetContentPosition(thorPositionByX, thorPositionByY, ContentPosition.Thor);
            var thorPosition = _game.Map.Find(location => location.Content == ContentPosition.Thor);

            // Assert
            Check.That(thorPosition).IsNotNull();
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
            _game.MoveThorTo(giantPosition);
            var thorPosition = _game.Map.Find(location => location.Content.Equals(ContentPosition.Thor));

            // Assert
            Check.That(thorPosition).IsNotNull();
            Check.That(thorPosition).IsEqualTo(giantPosition);
        }
    }
}