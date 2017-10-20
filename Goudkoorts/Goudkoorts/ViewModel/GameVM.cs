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

            //start game
            PlayGame();
        }

        public void PlayGame()
        {
            bool stopped = false;

            while (!stopped) //check if player decided to stop
            {
                MenuView.ShowMenu();
                if (UserInput.getInput().Equals("S"))
                {
                    Shipyard = new Shipyard(); //create actual game
                    GameView.ShowGame();
                    Console.ReadLine();
                }
            }
            
        }
    }
}