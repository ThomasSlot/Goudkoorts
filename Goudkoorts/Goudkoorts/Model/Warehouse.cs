using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goudkoorts
{
    public class Warehouse : GameItem
    {

        public Warehouse(char name, int x, int y)
        {
            this.name = name;
            this.temp = name;
            this.x = x;
            this.y = y;
        }

        public bool createCart(Random r)
        {
            int x = r.Next(11);
            if(x > 7)
            {
                return true;
            }
            return false;
        }
    }
}