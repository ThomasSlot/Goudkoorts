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

        public Shipyard()
        {
            carts = new List<Cart>();
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
                            Level[y].Insert(x, new Empty("-", x, y));
                            break;
                        case 'B':
                            ship = new Ship("B", x, y);
                            Level[y].Insert(x, ship);
                            break;
                        case 'X':
                            Level[y].Insert(x, new EndTrack("X", x, y));
                            break;
                        case 'R':
                            Level[y].Insert(x, new RegularTrack("R", x, y));
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
            if (Level[a][b].x > 0) //left
            {
                Level[a][b].left = Level[a][b-1];
            }
            if(Level[a][b].x < Level[0].Count()) //right
            {
                Level[a][b].right = Level[a][b+1];
            }
            if(Level[a][b].y < Level.Count() - 1) //down
            {
                Level[a][b].down = Level[a+1][b];
            }
            if(Level[a][b].y > 0) //up
            {
                Level[a][b].up = Level[a-1][b];
            }

            //set openside of merge/switchtrack
            if (Level[a][b].GetType() == typeof(MergeTrack))
            {
                Level[a][b].previous = Level[a][b].up;
                Level[a][b].down.color = ConsoleColor.Red;
                Level[a][b].previous.color = ConsoleColor.Green;
            } else if(Level[a][b].GetType() == typeof(SwitchTrack))
            {
                Level[a][b].next = Level[a][b].up;
                Level[a][b].down.color = ConsoleColor.Red;
                Level[a][b].next.color = ConsoleColor.Green;
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
                        if (Level[y][x].GetType() == typeof(MergeTrack)) //switch mergetrack
                        {
                            if(Level[y][x].previous != Level[y][x].down)
                            {
                                Level[y][x].previous = Level[y][x].down;
                                Level[y][x].up.color = ConsoleColor.Red;
                            } else
                            {
                                Level[y][x].previous = Level[y][x].up;
                                Level[y][x].down.color = ConsoleColor.Red;
                            }
                            Level[y][x].previous.color = ConsoleColor.Green;
                        } else if(Level[y][x].GetType() == typeof(SwitchTrack))//switch switchtrack
                        {
                            if (Level[y][x].next != Level[y][x].down)
                            {
                                Level[y][x].next = Level[y][x].down;
                                Level[y][x].up.color = ConsoleColor.Red;
                            }
                            else
                            {
                                Level[y][x].next = Level[y][x].up;
                                Level[y][x].down.color = ConsoleColor.Red;
                            }
                            Level[y][x].next.color = ConsoleColor.Green;
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
                        if(w.createCart(r))
                        {
                            cart = new Cart(i, j);
                            cart.current = Level[i][j];
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
            if(ship.fill >= 4) //if ship is full;
            {
                Points += 10;
                ship.fill = 0;
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
                    if (carts[i].current.GetType() == typeof(EndTrack))
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
                    if(carts[i].current == carts[i + 1].current)
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
                if (c.current.GetType() == typeof(Warehouse)) //if warehouse
                {
                    c.previous = c.current; //set previous
                    if (c.current.left.GetType().BaseType == typeof(RideTrack)) //if left track
                    {
                        c.current = c.current.left;
                        c.current.setCart(true);
                    }
                    else if (c.current.right.GetType().BaseType == typeof(RideTrack))//if right track
                    {
                        c.current = c.current.right;
                        c.current.setCart(true);
                    }
                    else if (c.current.up.GetType().BaseType == typeof(RideTrack))//if up track
                    {
                        c.current = c.current.up;
                        c.current.setCart(true);
                    }
                    else if (c.current.down.GetType().BaseType == typeof(RideTrack))//if down track
                    {
                        c.current = c.current.down;
                        c.current.setCart(true);
                    }
                }

                else if (c.current.GetType().BaseType == typeof(RideTrack)) //if track
                {
                    if (c.current.left.GetType().BaseType == typeof(RideTrack) && c.current.left != c.previous) //if left = track
                    {
                        if (c.current.left.GetType() == typeof(ClassificationYard) && c.current.left.hasCart == false) //if left = classificationyard and not has cart
                        {
                            Direction(c, "left");
                        } else if (c.current.left.GetType() != typeof(ClassificationYard))
                        {
                            Direction(c, "left");
                        }
                    }
                    else if (c.current.right.GetType().BaseType == typeof(RideTrack) && c.current.right != c.previous)//if right = track
                    {
                        Direction(c, "right");
                    }
                    else if (c.current.up.GetType().BaseType == typeof(RideTrack) && c.current.up != c.previous)//if up = track
                    {
                        Direction(c, "up");
                    }
                    else if (c.current.down.GetType().BaseType == typeof(RideTrack) && c.current.down != c.previous)//if down = track
                    {
                        if (c.current.color != ConsoleColor.Red)//if down = open mergetrack
                        {
                            Console.WriteLine("ik mag naar beneden");
                            Direction(c, "down");
                        } 
                    }
                } 
                if(c.current.GetType() == typeof(Pier)) //fill ship
                {
                    Ship s = (Ship) c.current.up;
                    s.fill += 1;
                    Points += 1; //add 1 point
                    Console.WriteLine("Ship filled: " + c.current.GetType() + ", " + s.fill);
                    Console.ReadLine();
                }            
            }
        }

        public void Direction(Cart c, string direction)
        {
            c.previous = c.current;
            c.previous.setCart(false);

            switch (direction)
            {
                case "left":
                    c.current = c.current.left;
                    break;
                case "right":
                    c.current = c.current.right;
                    break;
                case "up":
                    c.current = c.current.up;
                    break;
                case "down":
                    c.current = c.current.down;
                    break;
            }

            c.current.setCart(true);
        }
    }
}