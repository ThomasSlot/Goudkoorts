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
        public char name { get; set; }
        
        public GameItem()
        {
        }        
    }
}