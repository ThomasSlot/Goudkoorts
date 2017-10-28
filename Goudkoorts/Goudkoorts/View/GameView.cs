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
            Console.WriteLine("Two Carts crashed into eachother, you died.");
            Console.WriteLine("Press R to return to Menu");
        }

        public void GameWin()
        {
            Console.Clear();
            Console.WriteLine("You have won the game!");
            Console.WriteLine("Press R to return to Menu");
        }

        public void ShowGame(Shipyard S, int Time, int Cart, List<Cart> Carts, int Points, int Fill, int Dif)
        {
            shipyard = S;

            Console.Clear();

            Console.WriteLine("TIMER:" + Time);
            Console.WriteLine("Carts:" + Cart + "  Difficulty:" + Dif);
            Console.WriteLine("Points:" + Points);
            Console.WriteLine("ShipFull (" + Fill + "/4): ");
            Console.WriteLine("");
            Console.WriteLine("-----------------");

            for (int x = 0; x < shipyard.Level.Count(); x++) //actual game
            {
                foreach (GameItem G in shipyard.Level[x])
                {
                    if (G.Color == ConsoleColor.Green || G.Color == ConsoleColor.Red)
                    {
                        Console.ForegroundColor = G.Color;
                        Console.Write(G.Name);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(G.Name);
                    }
                }
                Console.WriteLine(" |");
            }

            Console.WriteLine("________________");
            Console.WriteLine("Controls:");
            Console.WriteLine("1 - Switch MergeTrack  1         4 - Switch SwitchTrack 2");
            Console.WriteLine("2 - Switch SwitchTrack 1         5 - Switch MergeTrack  3");
            Console.WriteLine("3 - Switch MergeTrack  2         S - Stop The Game");
            Console.WriteLine("R - Return To Menu");
            Console.WriteLine("Signs:");
            Console.WriteLine("W - Warehouse                    P - Pier");
            Console.WriteLine("= - RegularTrack                 B - Boat");
            Console.WriteLine("1/5 - SwitchTrack/MergeTrack     C - ClassificationYard");
            Console.WriteLine("X - EndTrack");
            Console.WriteLine("Choose a number to Switch:");
        }

    }
}