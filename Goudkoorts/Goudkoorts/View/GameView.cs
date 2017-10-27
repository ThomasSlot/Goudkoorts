using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goudkoorts
{
    public class GameView
    {
        Shipyard shipyard;

        public GameView()
        {
            
        }

        public void GameCrash()
        {
            Console.Clear();
            Console.WriteLine("Two carts crashed into eachother, you died.");
            Console.WriteLine("Press R to return to Menu");
        }

        public void GameWin()
        {
            Console.Clear();
            Console.WriteLine("You have won the game!");
            Console.WriteLine("Press R to return to Menu");
        }

        public void ShowGame(Shipyard s, int time, int cart, List<Cart> carts, int Points, int fill)
        {
            shipyard = s;

            Console.Clear();

            Console.WriteLine("TIMER:" + time);
            Console.WriteLine("Carts:" + cart);
            Console.WriteLine("Points:" + Points);
            Console.WriteLine("ShipFull (0/4): " + fill);
            Console.WriteLine("");

            for (int x = 0; x < shipyard.Level.Count(); x++) //actual game
            {
                foreach (GameItem g in shipyard.Level[x])
                {
                    Console.Write(g.name);
                }
                Console.WriteLine(" ");
            }

            Console.WriteLine("Controls:");
            Console.WriteLine("1 - Switch MergeTrack  1         4 - Switch SwitchTrack 2");
            Console.WriteLine("2 - Switch SwitchTrack 1         5 - Switch MergeTrack  3");
            Console.WriteLine("3 - Switch MergeTrack  2         S - Stop The Game");
            Console.WriteLine("R - Return To Menu");
            Console.WriteLine("Signs:");
            Console.WriteLine("W - Warehouse                    P - Pier");
            Console.WriteLine("R - RegularTrack                 B - Boat");
            Console.WriteLine("S - SwitchTrack                  C - ClassificationYard");
            Console.WriteLine("M - MergeTrack                   X - EndTrack");
        }

    }
}