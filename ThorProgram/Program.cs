using System;
using System.Collections.Generic;
using System.Linq;

namespace ThorProgram
{
    /**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
    static class Player
    {
        private static void Main(string[] args)
        {
            const string strike = "STRIKE";
            const string wait = "WAIT";

            const int height = 18;
            const int weight = 40;
            var game = new Game(height, weight);
            var thorPosition = game.GetThorPosition();

            // game loop
            while (true)
            {
                var giants = game.GetGiantPositions();
                giants.ForEach(Console.Error.WriteLine);

                var giant = game.FindTheNearestPosition(thorPosition, giants);
                if (giant == null)
                {
                    Console.WriteLine(wait);
                    return;
                }

                Console.Error.WriteLine(thorPosition);
                if (game.IsBeside(thorPosition, giant))
                {
                    Console.WriteLine(strike);
                    giant.Content = ContentPosition.Empty;
                    return;
                }

                var direction = game.GetDirectionWhereMoveTo(thorPosition,giant);
                Console.WriteLine(direction);
            }
        }
    }

    public class Game
    {
        public List<Position> Map { get; private set; }
        private readonly int _heightMap;
        private readonly int _weightMap;

        public Game(int height, int weight)
        {
            _heightMap = height;
            _weightMap = weight;
            InitTheMap();
        }

        private void InitTheMap()
        {
            Map = new List<Position>();
            for (var i = 0; i < _heightMap; i++)
            {
                for (var j = 0; j < _weightMap; j++)
                {
                    Map.Add(new Position
                    {
                        X = j,
                        Y = i
                    });
                }
            }
        }

        public void SetContentPosition(int x, int y, ContentPosition content)
        {
            var position = Map.Find(location => location.X.Equals(x) && location.Y.Equals(y));
            position.Content = content;
        }

        public string GetDirectionWhereMoveTo(Position currentPosition, Position targetPosition)
        {
            var direction = "";
            var currentPositionByX = currentPosition.X;
            var currentPositionByY = currentPosition.Y;
            var targetPositionByX = targetPosition.X;
            var targetPositionByY = targetPosition.Y;

            if (targetPositionByY < currentPositionByY)
            {
                currentPosition.Y--;
                direction = "N";
            }
            if (targetPositionByY > currentPositionByY)
            {
                currentPosition.Y++;
                direction = "S";
            }
            if (targetPositionByX < currentPositionByX)
            {
                currentPosition.X--;
                direction += "W";
            }

            if (targetPositionByX > currentPositionByX)
            {
                currentPosition.X++;
                direction += "E";
            }

            return direction;
        }

        private Position GetPosition(int positionByX, int positionByY)
        {
            return Map.Find(position => position.X.Equals(positionByX)
                                        && position.Y.Equals(positionByY));
        }

        public Position CurrentThorPosition()
        {
            return Map.Find(position => position.Content.Equals(ContentPosition.Thor));
        }

        public void DisplayMap()
        {
            Map.ForEach(position => { Console.Error.WriteLine(position); });
        }

        public Position FindTheNearestPosition(Position currentPosition, List<Position> positions)
        {
            var positionDifferenceList = positions.Select
            (expectedPosition => new
                {
                    Position = expectedPosition,
                    Diff = GetNumberOfMovementToReachPosition(currentPosition, expectedPosition)
                }
            );
            positionDifferenceList = positionDifferenceList.OrderBy(
                p => p.Diff);
            return positionDifferenceList.FirstOrDefault()?.Position;
        }

        private static int GetNumberOfMovementToReachPosition(Position currentPosition, Position expectedPosition)
        {
            return Math.Abs(expectedPosition.X - currentPosition.X) +
                   Math.Abs(expectedPosition.Y - currentPosition.Y);
        }

        public bool IsBeside(Position currentPosition, Position expectedPosition)
        {
            const int numberOfMovement = 1;
            return GetNumberOfMovementToReachPosition(currentPosition, expectedPosition).Equals(numberOfMovement)
                   || IsInDiagonalPosition(currentPosition, expectedPosition, numberOfMovement);
        }

        private bool IsInDiagonalPosition(Position currentPosition, Position expectedPosition, int distance)
        {
            var numberOfMovementByX = Math.Abs(currentPosition.X - expectedPosition.X);
            var numberOfMovementByY = Math.Abs(currentPosition.Y - expectedPosition.Y);
            return numberOfMovementByX.Equals(distance) && numberOfMovementByY.Equals(distance);
        }

        public List<Position> GetGiantPositions()
        {
            var inputs = Console.ReadLine()?.Split(' ');
            if (inputs == null) throw new ApplicationException("No input for giants");
            var giants = new List<Position>();
            var h = int.Parse(inputs[0]); // the remaining number of hammer strikes.
            var n = int.Parse(inputs[1]); // the number of giants which are still present on the map.
            for (var i = 0; i < n; i++)
            {
                inputs = Console.ReadLine()?.Split(' ');
                if (inputs == null) continue;
                giants.Add(new Position
                {
                    X = int.Parse(inputs[0]),
                    Y = int.Parse(inputs[1])
                });
            }

            return giants;
        }

        public Position GetThorPosition()
        {
            var inputs = Console.ReadLine()?.Split(' ');
            if (inputs == null) throw new ApplicationException("No input for thor position");
            return new Position
            {
                X = int.Parse(inputs[0]),
                Y = int.Parse(inputs[1])
            };
        }
    }

    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public ContentPosition Content { get; set; }

        public override string ToString()
        {
            return $"X : {X}, Y : {Y}, Content : {Content}";
        }

        public override bool Equals(Object obj)
        {
            if ((obj == null) || this.GetType() != obj.GetType())
            {
                return false;
            }

            var otherPosition = obj as Position;
            return Equals(otherPosition);
        }

        private bool Equals(Position other)
        {
            return X == other.X && Y == other.Y;
        }
    }

    public enum ContentPosition
    {
        Empty = 0,
        Thor = 1,
        Giant = 2,
    }
}