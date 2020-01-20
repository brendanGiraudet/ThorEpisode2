using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Player
{
    static void Main(string[] args)
    {
        var game = new Game();
        const int height = 18;
        const int weight = 40;
        game.InitTheMap(height, weight);

        var inputs = Console.ReadLine().Split(' ');
        int thorPositionByX = int.Parse(inputs[0]);
        int thorPositionByY = int.Parse(inputs[1]);
        game.SetContentPosition(thorPositionByX, thorPositionByY, ContentPosition.Thor);

        // game loop
        while (true)
        {
            inputs = Console.ReadLine().Split(' ');
            int H = int.Parse(inputs[0]); // the remaining number of hammer strikes.
            int N = int.Parse(inputs[1]); // the number of giants which are still present on the map.
            for (int i = 0; i < N; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int giantPositionByX = int.Parse(inputs[0]);
                int giantPositionByY = int.Parse(inputs[1]);
                game.SetContentPosition(giantPositionByX, giantPositionByY, ContentPosition.Giant);
            }

            // The movement or action to be carried out: WAIT STRIKE N NE E SE S SW W or N
            Console.WriteLine("WAIT");
        }
    }
}