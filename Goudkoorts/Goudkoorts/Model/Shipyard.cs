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

        public Shipyard(int number)
        {
            ShipyardNumber = number; //choose level

            create(); //create level
            
        }

        private void create()
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
                            Level[y].Insert(x, new Empty('-'));
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
                    }
                    x++; //count charachter
                }
                x = 0; //new count on new line
            }
        }
    }
}