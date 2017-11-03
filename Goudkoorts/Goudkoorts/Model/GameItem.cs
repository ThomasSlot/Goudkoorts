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

        public virtual int AddPoints(int Points)
        {
            return Points;
        }

        public virtual bool IsType(string Type) //check is this type is the same as given type in parameter
        {
            string x = this.GetType().ToString();
            string[] split = x.Split('.');

            if (split[1].Equals(Type))
            {
                return true;
            }
            return false; 
        }

        public virtual bool isTrack()
        {
            if(this.GetType().BaseType == typeof(RideTrack)){
                return true;
            }
            return false;
        }

        public virtual void Switch()
        { 
        }

        public virtual bool CreateCart(Random R, int Dif)
        {
            return false;
        }
        
        public virtual void SetCart(bool b)
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