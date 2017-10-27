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
                            Level[y].Insert(x, new Ship('B'));
                            break;
                        case 'X':
                            Level[y].Insert(x, new EndTrack('X'));
                            break;
                        case 'R':
                            Level[y].Insert(x, new RegularTrack('R'));
                            break;
                        case 'P':
                            Level[y].Insert(x, new Pier('P'));
                            break;
                        case 'S':
                            Level[y].Insert(x, new SwitchTrack('S'));
                            break;
                        case 'W':
                            Level[y].Insert(x, new Warehouse('W'));
                            break;
                        case 'C':
                            Level[y].Insert(x, new ClassificationYard('C'));
                            break;
                        case 'M':
                            Level[y].Insert(x, new MergeTrack('M'));
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
                        
            if(Level[a][b].x != 0) //left
            {
                Level[a][b].left = Level[a--][b];
                a++;
            }
            if(Level[a][b].y < Level[0].Count()) //right
            {
                Level[a][b].right = Level[a++][b];
                a--;
            }
            if(Level[a][b].x < Level.Count()) //down
            {
                Level[a][b].down = Level[a][b++];
                b--;
            }
            if(Level[a][b].y != 0) //up
            {
                Level[a][b].up = Level[a][b--];
                b++;
            }
        }   
        
        public void PlayRound()
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
                Console.ReadLine();
            } 
           

            //check for points
            //check for crash (not classificationyard) and delete if true
            //delete cars if endtrack
        }

        public void moveCarts()
        {
            foreach (Cart c in carts)
            {
                Console.WriteLine(c.x + ", " + c.y + ", " + c.current);
                if (c.current.GetType() == typeof(Warehouse)) //if warehouse
                {
                    if (c.current.left.GetType().BaseType == typeof(RideTrack)) //if left track
                    {
                        c.current = c.current.left;
                    }
                    else if (c.current.right.GetType().BaseType == typeof(RideTrack))//if right track
                    {
                        c.current = c.current.right;
                    }
                    else if (c.current.up.GetType().BaseType == typeof(RideTrack))//if up track
                    {
                        c.current = c.current.up;
                    }
                    else if (c.current.down.GetType().BaseType == typeof(RideTrack))//if down track
                    {
                        c.current = c.current.down;
                    }
                } else if (c.current.GetType().BaseType == typeof(RideTrack)) //if track
                {
                    if (c.current.left.GetType().BaseType == typeof(RideTrack)) //if left track
                    {
                        c.current = c.current.left;
                    }
                    else if (c.current.right.GetType().BaseType == typeof(RideTrack))//if right track
                    {
                        c.current = c.current.right;
                    }
                    else if (c.current.up.GetType().BaseType == typeof(RideTrack))//if up track
                    {
                        c.current = c.current.up;
                    }
                    else if (c.current.down.GetType().BaseType == typeof(RideTrack))//if down track
                    {
                        c.current = c.current.down;
                    }
                }
                Console.WriteLine(c.x + ", " + c.y);
                
            }
        }
    }
}