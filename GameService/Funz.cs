using System;
using System.Collections.Generic;
using System.Text;

namespace GameService
{
   public class Funz
    {
        public static bool CheckAnswer()
        {
        Richiesta:
            Console.WriteLine(" \t\t\t Si - Premi S \t\t\t  No  - Premi N");
            string answer = Console.ReadLine();
            if (answer == "s")
            {
                return true;
            }
            else if (answer == "n")
            {
                return false;
            }
            else
            {
                Console.WriteLine("Opzione non valida");
                goto Richiesta;
            }
        }

    }
   

}
