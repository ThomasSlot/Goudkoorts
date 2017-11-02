using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goudkoorts
{
    public abstract class RideTrack : GameItem
    {

        public RideTrack()
        {
        }

        public override bool IsTrack()
        {
            return true;
        }

        public override void Switch()
        {

        }
    }
}