using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goudkoorts
{
    public class UserInput
    {
        public UserInput()
        {

        }

        public String getInput()
        {
            while (true)
            {
                ConsoleKeyInfo info = Console.ReadKey();
                string key = info.Key.ToString();

                switch (key)
                {
                    case "S":
                        return key;
                    case "R":
                        return key;
                    case "D1":
                        return "1";
                    case "D2":
                        return "2";
                    case "D3":
                        return "3";
                    case "D4":
                        return "4";
                    case "D5":
                        return "5";
                    case "D6":
                        return "6";
                }
            }
        }
    }
}