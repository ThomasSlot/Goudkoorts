using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goudkoorts
{
    public class MergeTrack : RideTrack
    {
        public MergeTrack(char name)
        {
            this.name = name;
            this.temp = name;
        }
    }
}