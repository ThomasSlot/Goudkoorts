using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goudkoorts
{
    public class Warehouse : GameItem
    {

        public Warehouse(string name, int x, int y)
        {
            this.name = name;
            this.temp = name;
            this.x = x;
            this.y = y;
        }

        public bool createCart(Random r, int dif)
        {
            int x = r.Next(100);
            if(x < dif*20)
            {
                return true;
            }
            return false;
        }
    }
}