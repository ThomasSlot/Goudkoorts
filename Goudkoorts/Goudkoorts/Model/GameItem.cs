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
   
        public int x { get; set; }
        public int y { get; set; }

        public char name { get; set; }

        public char temp { get; set; }

        public bool hasCart { get; set; }
        
        public GameItem()
        {
        }
            
        public void setCart(bool b)
        {
            if (b)
            {
                name = 'Z';
            }else
            {
                name = temp;
            }
        }
          
    }
}