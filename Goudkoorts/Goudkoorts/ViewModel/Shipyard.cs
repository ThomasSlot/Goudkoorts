using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Goudkoorts
{
    public class Shipyard
    {

        private int ShipyardNumber {get ;set; }

        public List<List<GameItem>> Level { get; set; }

        public List<Cart> Carts { get; set; }

        public Ship Ship { get; set; }

        public int Points { get; set; }

        public int Difficulty { get; set; }

        public Shipyard()
        {
            Carts = new List<Cart>();
            Difficulty = 1;
        }

        public void SetNumber(int i)
        {
            ShipyardNumber = i;
        }

        public void Create()
        {
            //read file
            string FileName = "Shipyard" + ShipyardNumber + ".txt";
            string path = Path.Combine(Environment.CurrentDirectory, @"Shipyard\", FileName);
            string[] Lines = File.ReadAllLines(path);

            //playing field
            Level = new List<List<GameItem>>();

            //add items
            int x = 0;
            int Switch = 1;
            for (int y = 0; y < Lines.Length; y++) 
            {
                Level.Insert(y, new List<GameItem>());

                foreach (char c in Lines[y])
                {
                    switch (c)
                    {
                        case '-':
                            Level[y].Insert(x, new Empty(" ", x, y));
                            break;
                        case 'B':
                            Ship = new Ship("B", x, y);
                            Level[y].Insert(x, Ship);
                            break;
                        case 'X':
                            Level[y].Insert(x, new EndTrack("X", x, y));
                            break;
                        case '=':
                            Level[y].Insert(x, new RegularTrack("=", x, y));
                            break;
                        case 'P':
                            Level[y].Insert(x, new Pier("P", x, y));
                            break;
                        case 'S':
                            Level[y].Insert(x, new SwitchTrack(Switch.ToString(), x, y));
                            Level[y][x].SwitchNumber = Switch;
                            Switch++;
                            break;
                        case 'W':
                            Level[y].Insert(x, new Warehouse("W", x, y));
                            break;
                        case 'C':
                            Level[y].Insert(x, new ClassificationYard("C", x, y));
                            break;
                        case 'M':
                            Level[y].Insert(x, new MergeTrack(Switch.ToString(), x, y));
                            Level[y][x].SwitchNumber = Switch;
                            Switch++;
                            break;
                    }
                    x++; //count charachter
                }
                x = 0; //new count on new line
            }

            //add Links
            for (int i = 0; i < Level.Count(); i++) //y-size
            {
                for(int j = 0; j < Level[i].Count() - 1; j++) //x-size
                {
                    SetSides(i, j);
                }
            }
        }

        public void SetSides(int a, int b) //set joining sides
        {
            if (Level[a][b].X > 0) //left
            {
                Level[a][b].Left = Level[a][b-1];
            }
            if(Level[a][b].X < Level[0].Count()) //right
            {
                Level[a][b].Right = Level[a][b+1];
            }
            if(Level[a][b].Y < Level.Count() - 1) //down
            {
                Level[a][b].Down = Level[a+1][b];
            }
            if(Level[a][b].Y > 0) //up
            {
                Level[a][b].Up = Level[a-1][b];
            }

            //set openside of merge/switchtrack
            if (Level[a][b].GetType() == typeof(MergeTrack))
            {
                Level[a][b].Previous = Level[a][b].Up;
                Level[a][b].Down.Color = ConsoleColor.Red;
                Level[a][b].Previous.Color = ConsoleColor.Green;
            } else if(Level[a][b].GetType() == typeof(SwitchTrack))
            {
                Level[a][b].Next = Level[a][b].Up;
                Level[a][b].Down.Color = ConsoleColor.Red;
                Level[a][b].Next.Color = ConsoleColor.Green;
            }
        }

        public void Switch(string a)
        {
            string j;

            for (int y = 0; y < Level.Count(); y++) //y-size
            {
                for (int x = 0; x < Level[y].Count() - 1; x++) //x-size
                {
                    j = Level[y][x].SwitchNumber.ToString();

                    if (a.Equals(j))
                    {
                        Level[y][x].Switch(); //GameItem.Switch
                    }
                }
            }
        }
        
        public int PlayRound()
        {
            //random Cart spawn
            Warehouse w;
            Cart Cart;
            Random r = new Random();
            for (int i = 0; i < Level.Count(); i++) //y-size
            {
                for (int j = 0; j < Level[i].Count() - 1; j++) //x-size
                {
                    if(Level[i][j].GetType() == typeof(Warehouse))
                    {
                        w = (Warehouse)Level[i][j];
                        if(w.CreateCart(r, Difficulty))
                        {
                            Cart = new Cart(i, j);
                            Cart.Current = Level[i][j];
                            Carts.Add(Cart);
                        }
                    }
                }
            }
           
            //move the Carts if there are any
            if (Carts != null)
            {
                MoveCarts();
            }


            //check for points
            if(CheckPoints())
            {
                return 2; //stop game
            }

            //check for crash (not classificationyard) and delete if true
            if (CheckCrash())
            {
                return 3; //stop game
            }

            //delete cars if endtrack
            DeleteCart();

            return 1; //entire round played
        }

        public bool CheckPoints()
        {
            if(Ship.Fill >= 4) //if Ship is full;
            {
                Difficulty += 2; //set difficulty higher
                Points += 10;
                Ship.Fill = 0;
            }

            if(Points >= 36)
            {
                return true;
            }
            return false;
        }

        public void DeleteCart()
        {
            if (Carts.Count() >= 1)
            {
                for (int i = 0; i < Carts.Count() - 1; i++)
                {
                    if (Carts[i].Current.GetType() == typeof(EndTrack))
                    {
                        Carts.RemoveAt(i);
                    }
                }
            }
        }

        public bool CheckCrash()
        {
            if(Carts.Count() > 1)
            {
                for(int i = 0; i < Carts.Count() - 1; i++)
                {
                    if(Carts[i].Current == Carts[i + 1].Current)
                    {
                        return true; //game over
                    }
                }
            }
            return false;
        }

        public void MoveCarts()
        {
            foreach (Cart c in Carts)
            {
                if (c.Current.GetType() == typeof(Warehouse)) //if warehouse
                {
                    c.Previous = c.Current; //set previous
                    if (c.Current.Left.GetType().BaseType == typeof(RideTrack)) //if left track
                    {
                        c.Current = c.Current.Left;
                        c.Current.SetCart(true);
                    }
                    else if (c.Current.Right.GetType().BaseType == typeof(RideTrack))//if right track
                    {
                        c.Current = c.Current.Right;
                        c.Current.SetCart(true);
                    }
                    else if (c.Current.Up.GetType().BaseType == typeof(RideTrack))//if up track
                    {
                        c.Current = c.Current.Up;
                        c.Current.SetCart(true);
                    }
                    else if (c.Current.Down.GetType().BaseType == typeof(RideTrack))//if down track
                    {
                        c.Current = c.Current.Down;
                        c.Current.SetCart(true);
                    }
                    continue;
                }

                if (c.Current.GetType().BaseType == typeof(RideTrack)) //current ridetrack
                {
                    if (c.Current.GetType() == typeof(Pier)) //fill Ship
                    {
                        Ship.Fill += 1;
                        Points += 1; //add 1 point
                    }

                    if (c.Current.Right.GetType().BaseType == typeof(RideTrack) && c.Current.Right != c.Previous) //check right
                    {
                        Direction(c, "right");//go right
                            continue;
                    }

                    if (c.Current.Left.GetType().BaseType == typeof(RideTrack) && c.Current.Left != c.Previous)//check left
                    {
                        if (c.Current.GetType() == typeof(ClassificationYard) && !c.Current.Left.HasCart) //classification yard
                        {
                            Direction(c, "left");//go left
                            continue;
                        } else if (c.Current.GetType() == typeof(RegularTrack) || c.Current.GetType() == typeof(Pier))
                        {
                            Direction(c, "left");//go left
                            continue;
                        } 
                    }

                    if (c.Current.Up.GetType().BaseType == typeof(RideTrack) && c.Current.Up != c.Previous)//check up
                    {
                            if (c.Current.GetType() == typeof(SwitchTrack) && c.Current.Up == c.Current.Next)//check switchtrack
                            {
                                Direction(c, "up"); //go up
                                continue;
                            } else if (c.Current.GetType() == typeof(RegularTrack)) //check regulartrack
                            {
                                Direction(c, "up"); //go up
                                continue;
                            }
                    }

                    if (c.Current.Down.GetType().BaseType == typeof(RideTrack) && c.Current.Down != c.Previous) //check down
                    {
                            if (c.Current.GetType() == typeof(SwitchTrack) && c.Current.Down == c.Current.Next)//check switchtrack
                            {
                                Direction(c, "down"); //go down
                                continue;
                            } else if (c.Current.GetType() == typeof(RegularTrack))//check regulartrack
                            {
                                Direction(c, "down"); //go down
                                continue;
                            }
                    }
                }
            }
        }

        public void Direction(Cart c, string direction)
        {
            c.Previous = c.Current;
            c.Previous.SetCart(false);

            switch (direction)
            {
                case "left":
                    c.Current = c.Current.Left;
                    break;
                case "right":
                    c.Current = c.Current.Right;
                    break;
                case "up":
                    c.Current = c.Current.Up;
                    break;
                case "down":
                    c.Current = c.Current.Down;
                    break;
            }

            c.Current.SetCart(true);
        }
    }
}