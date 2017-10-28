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

        public List<Cart> carts { get; set; }

        public Ship ship { get; set; }

        public int Points { get; set; }

        public int Difficulty { get; set; }

        public Shipyard()
        {
            carts = new List<Cart>();
            Difficulty = 1;
        }

        public void setNumber(int i)
        {
            ShipyardNumber = i;
        }

        public void create()
        {
            //read file
            string fileName = "Shipyard" + ShipyardNumber + ".txt";
            string path = Path.Combine(Environment.CurrentDirectory, @"Shipyard\", fileName);
            string[] lines = File.ReadAllLines(path);

            //playing field
            Level = new List<List<GameItem>>();

            //add items
            int x = 0;
            int Switch = 1;
            for (int y = 0; y < lines.Length; y++) 
            {
                Level.Insert(y, new List<GameItem>());

                foreach (char c in lines[y])
                {
                    switch (c)
                    {
                        case '-':
                            Level[y].Insert(x, new Empty(" ", x, y));
                            break;
                        case 'B':
                            ship = new Ship("B", x, y);
                            Level[y].Insert(x, ship);
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
                    if (!Level[y][x].HasCart)
                    {
                        j = Level[y][x].SwitchNumber.ToString();

                        if (a.Equals(j))
                        {
                            if (Level[y][x].GetType() == typeof(MergeTrack)) //switch mergetrack
                            {
                                if (Level[y][x].Previous != Level[y][x].Down)
                                {
                                    Level[y][x].Previous = Level[y][x].Down;
                                    Level[y][x].Up.Color = ConsoleColor.Red;
                                }
                                else
                                {
                                    Level[y][x].Previous = Level[y][x].Up;
                                    Level[y][x].Down.Color = ConsoleColor.Red;
                                }
                                Level[y][x].Previous.Color = ConsoleColor.Green;
                            }
                            else if (Level[y][x].GetType() == typeof(SwitchTrack))//switch switchtrack
                            {
                                if (Level[y][x].Next != Level[y][x].Down)
                                {
                                    Level[y][x].Next = Level[y][x].Down;
                                    Level[y][x].Up.Color = ConsoleColor.Red;
                                }
                                else
                                {
                                    Level[y][x].Next = Level[y][x].Up;
                                    Level[y][x].Down.Color = ConsoleColor.Red;
                                }
                                Level[y][x].Next.Color = ConsoleColor.Green;
                            }
                        }
                    }
                }
            }
        }
        
        public int PlayRound()
        {
            //random cart spawn
            Warehouse w;
            Cart cart;
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
                            cart = new Cart(i, j);
                            cart.Current = Level[i][j];
                            carts.Add(cart);
                        }
                    }
                }
            }
           
            //move the carts if there are any
            if (carts != null)
            {
                moveCarts();
            }


            //check for points
            if(checkPoints())
            {
                return 2; //stop game
            }

            //check for crash (not classificationyard) and delete if true
            if (checkCrash())
            {
                return 3; //stop game
            }

            //delete cars if endtrack
            deleteCart();

            return 1; //entire round played
        }

        public bool checkPoints()
        {
            if(ship.Fill >= 4) //if ship is full;
            {
                Difficulty += 2; //set difficulty higher
                Points += 10;
                ship.Fill = 0;
            }

            if(Points >= 36)
            {
                return true;
            }
            return false;
        }

        public void deleteCart()
        {
            if (carts.Count() >= 1)
            {
                for (int i = 0; i < carts.Count() - 1; i++)
                {
                    if (carts[i].Current.GetType() == typeof(EndTrack))
                    {
                        carts.RemoveAt(i);
                    }
                }
            }
        }

        public bool checkCrash()
        {
            if(carts.Count() > 1)
            {
                for(int i = 0; i < carts.Count() - 1; i++)
                {
                    if(carts[i].Current == carts[i + 1].Current)
                    {
                        return true; //game over
                    }
                }
            }
            return false;
        }

        public void moveCarts()
        {
            foreach (Cart c in carts)
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
                    if (c.Current.GetType() == typeof(Pier)) //fill ship
                    {
                        ship.Fill += 1;
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