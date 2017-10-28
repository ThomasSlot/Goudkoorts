using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goudkoorts
{
    public class Empty : GameItem
    {
        public Empty(string Name, int X, int Y)
        {
            this.Name = Name;
            this.X = X;
            this.Y = Y;
        }

    }
}