using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goudkoorts
{
    public class SwitchTrack : RideTrack
    {
        public SwitchTrack(char name)
        {
            this.name = name;
            this.temp = name;
        }
    }
}