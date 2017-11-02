using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goudkoorts
{
    public class Cart : GameItem
    {
        public Cart(int Y, int X)
        {
            this.Y = Y;
            this.X = X;
            Name = "Z";
        }

        public bool OnEndTrack()
        {
            if(Current.GetType() == typeof(EndTrack))
            {
                return true;
            }
            return false;
        }
    }
}