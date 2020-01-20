using System;
using System.Collections.Generic;
public class Game
{
    public List<Position> Map { get; set; }
    public void InitTheMap(int height, int weight)
    {
        Map = new List<Position>();
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < weight; j++)
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
        position.Content = ContentPosition.Thor;
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
            default:
            System.Console.WriteLine("Error : no destination found");
                return;
        }

        currentThorPosition.Content = ContentPosition.Empty;
        expectedThorPosition.Content = ContentPosition.Thor;
    }
}