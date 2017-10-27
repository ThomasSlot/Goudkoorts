using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goudkoorts
{
    public class ClassificationYard : RideTrack
    {
        public ClassificationYard(char name)
        {
            this.name = name;
            this.temp = name;
        }
    }
}