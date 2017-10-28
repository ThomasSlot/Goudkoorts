using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goudkoorts
{
    public class MergeTrack : RideTrack
    {
        public MergeTrack(string name, int x, int y)
        {
            this.name = name;
            this.temp = name;
            this.x = x;
            this.y = y;
        }
    }
}