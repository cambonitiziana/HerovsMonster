using GameService;
using System;
using Microsoft.Extensions.DependencyInjection;
using HeroVSMonster.Core.Entities;
using System.Collections.Generic;

namespace HeroVSMonster
{
    class Program
    {
        static void Main(string[] args)
        {
            //identificaton player ID (if any) otherwise create new player
            int id =Funzionalità.Funzionalità.MatchPredisposition();

            newHero:
            //recover (if any) the characters associated with the player's id otherwise character creation
            Hero h = Funzionalità.Funzionalità.GetHeroByPlayerID(id);

            newMatch:
            //Hero Weapons: choise between the waepons associated to the specific Hero class 
            h.weapon = Funzionalità.Funzionalità.GetWeapons(h);
           
            //fetch of a monster (same or less level of the Hero) and match
            // match end when:  1) Hero success in the escape
            //                  2) Monster win
            //                  3) Player win
            h = Funzionalità.Funzionalità.Match(h);


            var serviceProvider2 = DIConfiguration.ConfigurazioneHero();
            HeroService heroService = serviceProvider2.GetService<HeroService>();

            Console.WriteLine("Vuoi sfidare un altro mostro??");
            bool answer = Funz.CheckAnswer();
            if (answer)
            {
                if (h.lifePoint == 0)
                {
                    heroService.DeleteHero(h);
                    Console.WriteLine("il personaggio precedente è stato eliminato!");
                    goto newHero;
                }
                else
                {

                    h = Funzionalità.Funzionalità.updateLevel(h);
                    heroService.Update(h);
                    goto newMatch;
                }
            }
            else
            {
                h = Funzionalità.Funzionalità.updateLevel(h);
                heroService.Update(h);
                Console.WriteLine("Salvataggio Dati! \nAPresto");
            }


            

            
        }
       
        
    }
}
