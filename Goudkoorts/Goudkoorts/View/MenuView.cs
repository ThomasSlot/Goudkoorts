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
            Console.WriteLine("1 - Move TrackSwitch 1         4 - Move TrackSwitch 4");
            Console.WriteLine("2 - Move TrackSwitch 2         5 - Move TrackSwitch 5");
            Console.WriteLine("3 - Move TrackSwitch 3         S - Stop The Game");
            Console.WriteLine("R - Return To Menu");
            Console.WriteLine("");
            Console.WriteLine("Press S to Start the game!");
        }
    }
}