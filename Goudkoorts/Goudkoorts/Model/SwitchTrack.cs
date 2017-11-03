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

        public override void Switch()
        {
            if (Next != Down)
            {
                Next = Down;
                Up.Color = ConsoleColor.Red;
            }
            else
            {
                Next = Up;
                Down.Color = ConsoleColor.Red;
            }
            Next.Color = ConsoleColor.Green;
        }
    }
}