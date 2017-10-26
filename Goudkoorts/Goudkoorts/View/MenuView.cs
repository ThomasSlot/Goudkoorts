using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goudkoorts
{
    public class MenuView
    {
        public MenuView()
        {
        }

        public void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("            GOLDRUSH: The Game          ");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("Welcome to GOLDRUSH: The Game!");
            Console.WriteLine("The game is simple: Earn 36 points by bringing gold to the ship.");
            Console.WriteLine("You earn 1 points by filling the ship with a cart and 10 with a full ship.");
            Console.WriteLine("Watch the timer, because you only get a limited amount of time to react!");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Controls:");
            Console.WriteLine("1 - Switch MergeTrack  1         4 - Switch SwitchTrack 2");
            Console.WriteLine("2 - Switch SwitchTrack 1         5 - Switch MergeTrack  3");
            Console.WriteLine("3 - Switch MergeTrack  2         S - Stop The Game");
            Console.WriteLine("R - Return To Menu");
            Console.WriteLine("");
            Console.WriteLine("Press S to Start the game!");
        }
    }
}