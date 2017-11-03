using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goudkoorts
{
    public class Warehouse : GameItem
    {

        public Warehouse(string Name, int X, int Y)
        {
            this.Name = Name;
            this.Temp = Name;
            this.X = X;
            this.Y = Y;
        }

        public override bool CreateCart(Random R, int Dif)
        {
            int X = R.Next(100);
            if(X < Dif*20)
            {
                return true;
            }
            return false;
        }
    }
}