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

        public Shipyard()
        {
            create();
        }

        private void create()
        {
            RideTrack Rail1 = new RideTrack("Rail1");

            RideTrack EndTrack = new RideTrack("EndTrack");
            EndTrack.previous = Rail1;


        }
    }
}