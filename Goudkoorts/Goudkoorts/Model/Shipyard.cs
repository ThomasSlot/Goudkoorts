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
            for (int y = 0; y < lines.Length; y++) 
            {
                Level.Insert(y, new List<GameItem>());

                foreach (char c in lines[y])
                {
                    switch (c)
                    {
                        case '-':
                            Level[y].Insert(x, new Empty('-', x, y));
                            break;
                        case 'B':
                            Level[y].Insert(x, new Ship('B', x, y));
                            break;
                        case 'X':
                            Level[y].Insert(x, new EndTrack('X', x, y));
                            break;
                        case 'R':
                            Level[y].Insert(x, new RegularTrack('R', x, y));
                            break;
                        case 'P':
                            Level[y].Insert(x, new Pier('P', x, y));
                            break;
                        case 'S':
                            Level[y].Insert(x, new SwitchTrack('S', x, y));
                            break;
                        case 'W':
                            Level[y].Insert(x, new Warehouse('W', x, y));
                            break;
                        case 'C':
                            Level[y].Insert(x, new ClassificationYard('C', x, y));
                            break;
                        case 'M':
                            Level[y].Insert(x, new MergeTrack('M', x, y));
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
                    SetCoordinates(i, j);
                }
            }
        }

        public void SetCoordinates(int a, int b) //set coordinates of each line plus sides
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
        }   
        
        public bool PlayRound()
        {
            //random random cart spawn
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
           
            if (carts != null)
            {
                moveCarts();
            }


            //check for points
            //check for crash (not classificationyard) and delete if true
            if (checkCrash())
            {
                return false; //stop game
            }
            //delete cars if endtrack

            return true;
        }

        public bool checkCrash()
        {
            if(carts.Count() > 1)
            {
                for(int i = 0; i < carts.Count() - 1; i++)
                {
                    if(carts[i].current == carts[i + 1].current)
                    {
                        carts.RemoveAt(i);
                        return true;
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
                } else if (c.current.GetType().BaseType == typeof(RideTrack)) //if track
                {
                    if (c.current.left.GetType().BaseType == typeof(RideTrack) && c.current.left != c.previous) //if left track
                    {
                        c.previous = c.current;
                        c.previous.setCart(false);
                        c.current = c.current.left;
                        c.current.setCart(true);
                    }
                    else if (c.current.right.GetType().BaseType == typeof(RideTrack) && c.current.right != c.previous)//if right track
                    {
                        c.previous = c.current;
                        c.previous.setCart(false);
                        c.current = c.current.right;
                        c.current.setCart(true);
                    }
                    else if (c.current.up.GetType().BaseType == typeof(RideTrack) && c.current.up != c.previous)//if up track
                    {
                        c.previous = c.current;
                        c.previous.setCart(false);
                        c.current = c.current.up;
                        c.current.setCart(true);
                    }
                    else if (c.current.down.GetType().BaseType == typeof(RideTrack) && c.current.down != c.previous)//if down track
                    {
                        c.previous = c.current;
                        c.previous.setCart(false);
                        c.current = c.current.down;
                        c.current.setCart(true);
                    }
                }             
            }
        }
    }
}