using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goudkoorts
{
    public abstract class GameItem
    {
        public GameItem Right { get; set; }
        public GameItem Left { get; set; }
        public GameItem Up { get; set; }
        public GameItem Down { get; set; }
        public GameItem Current { get; set; }
        public GameItem Previous { get; set; }
        public GameItem Next { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public ConsoleColor Color { get; set; }

        public int SwitchNumber { get; set; }

        public string Name { get; set; }

        public string Temp { get; set; }

        public bool HasCart { get; set; }
        
        public GameItem()
        {
            HasCart = false;
        }

        public virtual void Switch()
        { 
        }

        public virtual bool CreateCart(Random R, int Dif)
        {
            return false;
        }
        
            
        public void SetCart(bool b)
        {
            if (b)
            {
                Name = "Z";
                HasCart = true;
            }else
            {
                Name = Temp;
                HasCart = false;
            }
        }
          
    }
}