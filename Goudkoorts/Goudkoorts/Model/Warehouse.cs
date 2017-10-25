using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goudkoorts
{
    public class Warehouse : GameItem
    {
        public Cart Cart { get; set; }

        public Warehouse(char name)
        {
            this.name = name;
        }
    }
}