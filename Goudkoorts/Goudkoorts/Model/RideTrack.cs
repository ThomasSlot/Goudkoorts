using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goudkoorts
{
    public class RideTrack
    {
        public RideTrack next { get; set; }
        public RideTrack previous { get; set; }
        public int xCoo { get; set; }
        public int yCoo { get; set; }
        public string name { get; set; }
        public char sign { get; set; }

        public RideTrack(string name)
        {
            sign = '-';
            this.name = name;
        }
    }
}