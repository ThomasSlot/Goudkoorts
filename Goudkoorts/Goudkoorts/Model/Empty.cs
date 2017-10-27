using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goudkoorts
{
    public class Empty : GameItem
    {
        public Empty(char name, int x, int y)
        {
            this.name = name;
            this.x = x;
            this.y = y;
        }

    }
}