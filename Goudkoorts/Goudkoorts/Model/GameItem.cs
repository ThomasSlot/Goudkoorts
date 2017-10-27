using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goudkoorts
{
    public abstract class GameItem
    {
        public GameItem next { get; set; }
        public GameItem previous { get; set; }
        public GameItem current { get; set; }
        public char name { get; set; }

        public char temp { get; set; }

        public bool hasCart { get; set; }
        
        public GameItem()
        {
        }
        
        public void setPrevious(GameItem g)
        {
            previous = g;
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