using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goudkoorts
{
    public class GameItem
    {
        public GameItem right { get; set; }
        public GameItem left { get; set; }
        public GameItem up { get; set; }
        public GameItem down { get; set; }
        public GameItem current { get; set; }
        public GameItem previous { get; set; }
        public GameItem next { get; set; }

        public int x { get; set; }
        public int y { get; set; }

        public ConsoleColor color { get; set; }

        public int SwitchNumber { get; set; }

        public string name { get; set; }

        public string temp { get; set; }

        public bool hasCart { get; set; }
        
        public GameItem()
        {
            hasCart = false;
        }
            
        public void setCart(bool b)
        {
            if (b)
            {
                name = "Z";
                hasCart = true;
            }else
            {
                name = temp;
                hasCart = false;
            }
        }
          
    }
}