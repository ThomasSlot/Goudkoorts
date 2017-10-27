using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goudkoorts
{
    public class Pier : RideTrack
    {
        public Pier(char name, int x, int y)
        {
            this.name = name;
            this.temp = name;
            this.x = x;
            this.y = y;
        }
    }
}