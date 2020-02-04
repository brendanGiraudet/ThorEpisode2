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

            var game = new Game();
            var thorPosition = game.GetThorPosition();

            // game loop
            while (true)
            {
                var giants = game.GetGiantPositions();
                giants.ForEach(Console.Error.WriteLine);

                var giant = game.FindTheNearestPosition(thorPosition, giants);
                string action;
                Console.Error.WriteLine("Thor: " + thorPosition);
                Console.Error.WriteLine("Selected Giant : " + giant);
                if (giant == null)
                {
                    action = wait;
                }
                else
                {
                    var predictedGiantPosition = giant;
                    var direction = game.GetDirectionWhereMoveTo(predictedGiantPosition, thorPosition);
                    game.MoveTo(direction, predictedGiantPosition);
                    if (game.IsBeside(thorPosition, giant))
                    {
                        action = strike;
                    }
                    else
                    {
                        action = game.GetDirectionWhereMoveTo(thorPosition,giant);
                        game.MoveTo(action, thorPosition);
                    }    
                }
                
                Console.WriteLine(action);
            }
        }
    }

    public class Game
    {
        public string GetDirectionWhereMoveTo(Position currentPosition, Position targetPosition)
        {
            var direction = "";
            var currentPositionByX = currentPosition.X;
            var currentPositionByY = currentPosition.Y;
            var targetPositionByX = targetPosition.X;
            var targetPositionByY = targetPosition.Y;

            if (targetPositionByY < currentPositionByY)
            {
                direction = "N";
            }
            if (targetPositionByY > currentPositionByY)
            {
                direction = "S";
            }
            if (targetPositionByX < currentPositionByX)
            {
                direction += "W";
            }

            if (targetPositionByX > currentPositionByX)
            {
                direction += "E";
            }

            return direction;
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
            const int numberOfMovementForDiagonals = 2;
            return GetNumberOfMovementToReachPosition(currentPosition, expectedPosition).Equals(numberOfMovement)
                   || IsInDiagonalPosition(currentPosition, expectedPosition, numberOfMovementForDiagonals);
        }

        private bool IsInDiagonalPosition(Position currentPosition, Position expectedPosition, int numberOfMovementForDiagonals)
        {
            var numberOfMovementByX = Math.Abs(currentPosition.X - expectedPosition.X);
            var numberOfMovementByY = Math.Abs(currentPosition.Y - expectedPosition.Y);
            var numberOfMovement = numberOfMovementByX + numberOfMovementByY;
            return numberOfMovement.Equals(numberOfMovementForDiagonals);
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

        public void MoveTo(string direction, Position currentPosition)
        {
            if (direction.Contains("N"))
            {
                currentPosition.Y--;
            }
            if (direction.Contains("S"))
            {
                currentPosition.Y++;
            }
            if (direction.Contains("E"))
            {
                currentPosition.X++;
            }if (direction.Contains("W"))
            {
                currentPosition.X--;
            }
        }

        public Position FindTheRightPosition(Position thorPosition, List<Position> giants, int numberOfStrike)
        {
            throw new NotImplementedException();
        }
    }

    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        
        public override string ToString()
        {
            return $"X : {X}, Y : {Y}";
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
}