﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goudkoorts
{
    public class Ship : GameItem
    {
        public int Fill { get; set; }

        public Ship(string Name, int X, int Y)
        {
            this.Name = Name;
            this.X = X;
            this.Y = Y;
            Fill = 0;
        }

        public bool IsFull()
        {
            if(Fill >= 4)
            {
                Fill = 0;
                return true;
            }
            return false;
        }
    }
}