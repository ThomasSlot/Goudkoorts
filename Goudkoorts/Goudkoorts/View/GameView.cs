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

        public void ShowGame(Shipyard s)
        {
            shipyard = s;

            Console.Clear();

            for (int x = 0; x < shipyard.Level.Count(); x++)
            {
                foreach (GameItem g in shipyard.Level[x])
                {
                    Console.Write(g.name);
                }
                Console.WriteLine(" ");
            }
        }

    }
}