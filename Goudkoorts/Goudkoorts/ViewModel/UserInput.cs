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

        public String GetInput()
        {
            while (true)
            {
                ConsoleKeyInfo Info = Console.ReadKey();
                string Key = Info.Key.ToString();

                switch (Key)
                {
                    case "S":
                        return Key;
                    case "R":
                        return Key;
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