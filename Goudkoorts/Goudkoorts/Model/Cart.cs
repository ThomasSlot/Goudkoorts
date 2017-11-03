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

        public int Move()
        {
            int ReturnInt = 0;

            if (Current.IsType("Warehouse")) //if warehouse
            {
                Previous = Current; //set previous
                if (Current.Left.isTrack()) //if left track
                {
                    Current = Current.Left;
                    Current.SetCart(true);
                }
                else if (Current.Right.isTrack())//if right track
                {
                    Current = Current.Right;
                    Current.SetCart(true);
                }
                else if (Current.Up.isTrack())//if up track
                {
                    Current = Current.Up;
                    Current.SetCart(true);
                }
                else if (Current.Down.isTrack())//if down track
                {
                    Current = Current.Down;
                    Current.SetCart(true);
                }
                return ReturnInt;
            }
            if (Current.isTrack()) //current ridetrack
            {
                if (Current.IsType("Pier")) //fill Ship
                {
                    ReturnInt = 1;
                }

                if (Current.Right.isTrack() && Current.Right != Previous) //check right
                {
                    Direction("right");//go right
                    return ReturnInt;
                }

                if (Current.Left.isTrack() && Current.Left != Previous)//check left
                {
                    if (Current.IsType("ClassificationYard") && !Current.Left.HasCart) //classification yard
                    {
                        Direction("left");//go left
                        return ReturnInt;
                    }
                    if (Current.IsType("RegularTrack") || Current.IsType("Pier"))
                    {
                        Direction("left");//go left
                        return ReturnInt;
                    }
                }

                if (Current.Up.isTrack() && Current.Up != Previous)//check up
                {
                    if (Current.IsType("SwitchTrack") && Current.Up == Current.Next)//check switchtrack
                    {
                        Direction("up"); //go up
                        return ReturnInt;
                    }
                    if (Current.Up.IsType("MergeTrack") && Current.Up.Previous == Current)
                    {
                        Direction("up"); //go up
                        return ReturnInt; 
                    }
                    if (Current.IsType("RegularTrack") && !Current.Up.IsType("MergeTrack")) //check regulartrack
                    {
                        Direction("up"); //go up
                        return ReturnInt;
                    }
                }

                if (Current.Down.isTrack() && Current.Down != Previous) //check down
                {
                    if (Current.IsType("SwitchTrack") && Current.Down == Current.Next)//check switchtrack
                    {
                        Direction("down"); //go down
                        return ReturnInt;
                    }
                    if (Current.Down.IsType("MergeTrack") && Current.Down.Previous == Current)
                    {
                        Direction("down"); //go down
                        return ReturnInt;
                    }

                    if (Current.IsType("RegularTrack") && !Current.Down.IsType("MergeTrack"))//check regulartrack
                    {
                        Direction("down"); //go down
                        return ReturnInt;
                    }
                }
            }
            return ReturnInt;
        }

        public void Direction(string direction)
        {
            Previous = Current;
            Previous.SetCart(false);

            switch (direction)
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