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

        public GameVM()
        {
            //initialize views
            GameView = new GameView();
            MenuView = new MenuView();
            
            UserInput = new UserInput();
            Shipyard = new Shipyard(); //create actual game

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
                if (UserInput.getInput().Equals("S"))
                {
                    playing = true;
                    Shipyard.setNumber(1);
                    Shipyard.create();

                    while (playing) //playing the game
                    {
                        for (int a = 3; a >=0; a--)
                        {
                            Console.CursorLeft = 22;
                            GameView.ShowGame(Shipyard, a);
                            System.Threading.Thread.Sleep(1000);
                        }
                        Shipyard.PlayRound();
                    }
                }
            }
            
        }
    }
}