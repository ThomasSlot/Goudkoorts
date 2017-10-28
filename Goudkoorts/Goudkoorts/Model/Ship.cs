using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goudkoorts
{
    public class Ship : GameItem
    {
        public int fill { get; set; }

        public Ship(string name, int x, int y)
        {
            this.name = name;
            this.x = x;
            this.y = y;
            fill = 0;
        }
    }
}