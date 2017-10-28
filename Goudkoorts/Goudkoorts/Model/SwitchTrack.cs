using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goudkoorts
{
    public class SwitchTrack : RideTrack
    {
        public SwitchTrack(string Name, int X, int Y)
        {
            this.Name = Name;
            this.Temp = Name;
            this.X = X;
            this.Y = Y;
        }

    }
}