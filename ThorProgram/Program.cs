using System;
using System.Collections.Generic;
using System.Linq;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Player
{
    private static void Main(string[] args)
    {
        const int height = 18;
        const int weight = 40;
        var game = new Game(height, weight);
        game.InitTheMap();

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
            Console.WriteLine("WAIT");
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

    public void MoveThorTo(Direction direction)
    {
        var currentThorPosition = Map.Find(position => position.Content.Equals(ContentPosition.Thor));
        var expectedThorPosition = currentThorPosition;
        
        switch (direction)
        {
            case Direction.North:
                if(currentThorPosition.Y > 1)
                    expectedThorPosition.Y--;
            break;
            case Direction.NorthEst:
                if(currentThorPosition.Y > 1 && currentThorPosition.X < WeightMap - 1)
                    expectedThorPosition.Y--;
                    expectedThorPosition.X++;
            break;
            case Direction.East:
                if( currentThorPosition.X < WeightMap - 1)
                    expectedThorPosition.X++;
            break;
            default:
            System.Console.WriteLine("Error : no destination found");
                return;
        }

        currentThorPosition.Content = ContentPosition.Empty;
        expectedThorPosition.Content = ContentPosition.Thor;
    }    

    public void DisplayMap()
    {
        for (var i = 0; i < _heightMap; i++)
        {
            var row = Map.Where(position => position.Equals(i)).OrderBy(p => p.X).ToList();
            row.ForEach(Console.WriteLine);
            Console.WriteLine("\n");
        }
    }

public enum Direction
{
    North = 0,
    NorthEst = 1,
    East = 2,
    SouthEast = 3,
    South = 4,
    SouthWest = 5,
    West = 6, 
    NorthWest = 7
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
}
public enum ContentPosition
{
    Empty = 0,
    Thor = 1,
    Giant = 2,
}