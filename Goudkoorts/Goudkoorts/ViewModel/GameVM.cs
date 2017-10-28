using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goudkoorts
{
    public class GameVM
    {
        public GameView GameView { get; set; }

        public MenuView MenuView { get; set; }

        public UserInput UserInput { get; set; }

        public Shipyard Shipyard { get; set; }

        public int DifficultyLevel { get; set; }

        public GameVM()
        {
            //initialize views
            GameView = new GameView();
            MenuView = new MenuView();
            
            UserInput = new UserInput();

            //start game
            PlayGame();
        }

        public void PlayGame()
        {
            bool stopped = false;
            bool playing = false;

            while (!stopped) //check if player decided to stop
            {
                MenuView.ShowMenu();
                if (UserInput.getInput().Equals("S")) //player starts game
                {
                    Shipyard = new Shipyard(); //create actual game
                    playing = true;
                    Shipyard.setNumber(1);
                    Shipyard.create();

                    bool start = true;
                    while (playing) //playing the game
                    {
                        if (!start) //only use when its not the start of the game
                        {
                            string input = UserInput.getInput();
                            if (input.Equals("R")) //return to menu
                            {
                                break;
                            }
                            else if (input.Equals("S")) //stop game
                            {
                                Environment.Exit(0);
                            }
                            else
                            {
                                Shipyard.Switch(input); //switches
                            }
                        }
                        start = false;

                        DifficultyLevel = Shipyard.Difficulty; //set difficulty

                        for (int a = 11 - DifficultyLevel; a >=0; a--) //timer
                        {
                            Console.CursorLeft = 22;
                            GameView.ShowGame(Shipyard, a, Shipyard.carts.Count(), Shipyard.carts, Shipyard.Points, Shipyard.ship.Fill, DifficultyLevel);
                            System.Threading.Thread.Sleep(1000);
                        }

                        if (Shipyard.PlayRound() > 1) //check outcome of playing round
                        {
                            if (Shipyard.PlayRound() == 3) //crash
                            {
                                GameView.GameCrash();
                            } else if(Shipyard.PlayRound() == 2) //win
                            {
                                GameView.GameWin();
                            }

                            if (UserInput.getInput().Equals("R"))
                            {
                                break;
                            }
                        } 
                    }
                }
            }
            
        }
    }
}