using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goudkoorts
{
    public class Shipyard
    {
        public Warehouse Warehouse { get; set; }

        public Ship Ship { get; set; }

        public Pier Pier { get; set; }

        public LinkedList<RideTrack> Railway { get; set; }
    }
}