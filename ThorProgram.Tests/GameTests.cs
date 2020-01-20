using NFluent;
using NUnit.Framework;

namespace Tests
{
    public class GameTests
    {
        private Game _game;
        const int _heightMap = 18;
        const int _weightMap = 40;

        [SetUp]
        public void Setup()
        {
            _game = new Game();
            _game.InitTheMap(_heightMap, _weightMap);
        }
        [Test]
        public void ShouldInitiateTheMapWithPosition()
        {
            // Act
            _game.InitTheMap(_heightMap,_weightMap);

            // Assert
            Check.That(_game.Map).IsNotNull();
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
        public void shouldMoveThor()
        {
            // Arrange 
            const int thorPositionByX = 3;
            const int thorPositionByY = 3;
            _game.SetContentPosition(thorPositionByX, thorPositionByY, ContentPosition.Thor);

            // Act
            _game.MoveThorTo(Direction.North);
            var thorPosition = _game.Map.Find(location => location.Content.Equals(ContentPosition.Thor));

            // Assert
            Check.That(thorPosition).IsNotNull();
            Check.That(thorPosition.Y).IsEqualTo(thorPositionByY-1);

        }
    }
}