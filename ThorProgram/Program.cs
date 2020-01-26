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
            const int height = 18;
            const int weight = 40;
            var game = new Game(height, weight);

            var inputs = Console.ReadLine()?.Split(' ');
            if (inputs != null)
            {
                var thorPositionByX = int.Parse(inputs[0]);
                var thorPositionByY = int.Parse(inputs[1]);
                game.SetContentPosition(thorPositionByX, thorPositionByY, ContentPosition.Thor);
            }

            // game loop
            while (true)
            {
                inputs = Console.ReadLine()?.Split(' ');
                if (inputs != null)
                {
                    var h = int.Parse(inputs[0]); // the remaining number of hammer strikes.
                    var n = int.Parse(inputs[1]); // the number of giants which are still present on the map.
                    for (var i = 0; i < n; i++)
                    {
                        inputs = Console.ReadLine()?.Split(' ');
                        if (inputs == null) continue;
                        var giantPositionByX = int.Parse(inputs[0]);
                        var giantPositionByY = int.Parse(inputs[1]);
                        game.SetContentPosition(giantPositionByX, giantPositionByY, ContentPosition.Giant);
                    }
                }

                game.DisplayMap();
                var giant = game.FindTheNearestGiant();
                if(giant == null)
                    Console.WriteLine("WAIT");
                else
                {
                    var direction = game.GetDirectionWhereThorMoveTo(giant);
                    Console.WriteLine(direction);
                }
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

        public string GetDirectionWhereThorMoveTo(Position giantPosition)
        {
            var direction = "";
            var currentThorPosition = CurrentThorPosition();
            var positionByX = currentThorPosition.X;
            var positionByY = currentThorPosition.Y;

            if (giantPosition.Y < currentThorPosition.Y)
            {
                positionByY--;
                direction = "N";
            }

            if (giantPosition.Y > currentThorPosition.Y)
            {
                positionByY++;
                direction = "S";
            }

            if (giantPosition.X < currentThorPosition.X)
            {
                positionByX--;
                direction += "W";
            }

            if (giantPosition.X > currentThorPosition.X)
            {
                positionByX++;
                direction += "E";
            }

            var expectedPosition = GetPosition(positionByX, positionByY);
            expectedPosition.Content = ContentPosition.Thor;
            currentThorPosition.Content = ContentPosition.Empty;

            return direction;
        }

        private Position GetPosition(int positionByX, int positionByY)
        {
            return Map.Find(position => position.X.Equals(positionByX)
                                        && position.Y.Equals(positionByY));
        }

        private Position CurrentThorPosition()
        {
            return Map.Find(position => position.Content.Equals(ContentPosition.Thor));
        }

        public void DisplayMap()
        {
            for (var i = 0; i < _heightMap; i++)
            {
                var row = Map.Where(position => position.Y.Equals(i)).OrderBy(p => p.X).ToList();
                row.ForEach(Console.WriteLine);
                Console.WriteLine("\n");
            }
        }

        public Position FindTheNearestGiant()
        {
            var giants = Map.Where(
                position => position.Content.Equals(ContentPosition.Giant)
            ).ToList();
            if (!giants.Any())
            {
                return null;
            }
            var currentThorPosition = CurrentThorPosition();
            var positionDifferenceList = giants.Select
            ( position => new 
                {
                    Position = position,
                    Diff = Math.Abs(position.X - currentThorPosition.X) + 
                           Math.Abs(position.Y - currentThorPosition.Y)
                }
            );
            positionDifferenceList = positionDifferenceList.OrderBy(
                p => p.Diff);
            return positionDifferenceList.FirstOrDefault()? .Position;
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

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }

    public enum ContentPosition
    {
        Empty = 0,
        Thor = 1,
        Giant = 2,
    }
}