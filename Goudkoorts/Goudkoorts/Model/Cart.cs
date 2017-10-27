using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goudkoorts
{
    public class Cart : GameItem
    {
        public Cart(int y, int x)
        {
            this.y = y;
            this.x = x;
            name = 'Z';
        }
    }
}