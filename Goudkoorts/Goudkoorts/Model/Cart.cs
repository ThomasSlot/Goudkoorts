using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goudkoorts
{
    public class Cart : GameItem
    {
        public Cart(int Y, int X)
        {
            this.Y = Y;
            this.X = X;
            Name = "Z";
        }

        public void Move()
        {
            if (Current.isType("Warehouse")) //if on warehouse
            {
                Previous = Current;
                if (Current.Left.IsTrack())
                {
                    Current = Current.Left;
                    Current.SetCart(true);
                } else if (Current.Right.IsTrack())
                {
                    Current = Current.Right;
                    Current.SetCart(true);
                } else if ( Current.Up.IsTrack())
                {
                    Current = Current.Up;
                    Current.SetCart(true);
                } else if (Current.Down.IsTrack())
                {
                    Current = Current.Down;
                    Current.SetCart(true);
                }
                return;
            } else if (Current.IsTrack()) //if on ridetrack
            {
                if(Current.Right.IsTrack() && Current.Right != Previous) //right
                {
                    Direction("right");
                    return;
                }
                if(Current.Left.IsTrack() && Current.Left != Previous) //left
                {
                    if(Current.isType("ClassificationYard") && !Current.Left.HasCart)
                    {
                        Direction("left");
                        return;
                    }
                    if(Current.isType("RegularTrack") || Current.isType("Pier"))
                    {
                        Direction("left");
                        return;
                    }
                }
                if (Current.Down.IsTrack() && Current.Down != Previous) //down
                {
                    if(Current.isType("SwitchTrack") && Current.Down == Current.Next)
                    {
                        Direction("down");
                        return;
                    } 
                    if(Current.Down.isType("MergeTrack") && Current.Down.Previous == Current)
                    {
                        Direction("down");
                        return;
                    }
                    if (Current.isType("RegularTrack"))
                    {
                        Direction("down");
                        return;
                    }
                }
                if (Current.Up.IsTrack() && Current.Up != Previous) //up
                {
                    if(Current.isType("SwitchTrack") && Current.Up == Current.Next)
                    {
                        Direction("up");
                        return;
                    } 
                    if(Current.Up.isType("MergeTrack") && Current.Down.Previous == Current)
                    {
                        Direction("up");
                        return;
                    }
                    if (Current.isType("RegularTrack"))
                    {
                        Direction("up");
                        return;
                    }
                }
            }
        }

        public void Direction(string Direction)
        {
            Previous = Current;
            Previous.SetCart(false);

            switch (Direction)
            {
                case "left":
                    Current = Current.Left;
                    break;
                case "right":
                    Current = Current.Right;
                    break;
                case "up":
                    Current = Current.Up;
                    break;
                case "down":
                    Current = Current.Down;
                    break;
            }

            Current.SetCart(true);
        }

        public bool OnEndTrack()
        {
            if(Current.GetType() == typeof(EndTrack))
            {
                return true;
            }
            return false;
        }
    }
}