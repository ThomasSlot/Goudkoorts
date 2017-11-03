using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goudkoorts
{
    public class Ship : GameItem
    {
        public int Amount { get; set; }

        public Ship(string Name, int X, int Y)
        {
            this.Name = Name;
            this.X = X;
            this.Y = Y;
            Amount = 0;
        }

        public void Fill(int points)
        {
            Amount += points;
        }

        public bool IsFull()
        {
            if(Amount >= 4)
            {
                Amount = 0;
                return true;
            }
            return false;
        }
    }
}