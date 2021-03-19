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
            (int id, Player p) = Funzionalità.Funzionalità.MatchPredisposition();
            
            menu:
            Console.WriteLine("A - CreaEroe e Nuova Partita\nB - Elimina Eroe\nC - Esci");

            var menuCommand = Funzionalità.Funzionalità.isAdmin(p);
            //SAlvi il giocatore dcide di salvare -> poi si torna al menu
            //se non salvi 
            if (menuCommand == "d")
            {
                goto menu;
            }
            else if (menuCommand == "a")
            {

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
                if (answer) // se answer = true
                {
                    if (h.lifePoint == 0)
                    {
                        heroService.DeleteHero(h);
                        Console.WriteLine("il personaggio precedente è stato eliminato!");
                        goto newHero;
                    }

                    Console.WriteLine("Vuoi cambiare personaggio?");
                    bool a = Funz.CheckAnswer();
                    if (a)
                    {
                        Funzionalità.Funzionalità.updateLevel(h);
                        goto newHero;
                    }
                    else
                    {

                        h = Funzionalità.Funzionalità.updateLevel(h);
                        //heroService.Update(h);
                        goto newMatch;
                    }
                }
                else //se non vuole continuare a gocare -> uscita 
                {
                    Console.WriteLine("Salvataggio dati");
                    //salvataggio dati
                    h = Funzionalità.Funzionalità.updateLevel(h);
                    heroService.Update(h);
                    goto menu;
                }
            }
            else if (menuCommand == "b")
            {
                var serviceProvider2 = DIConfiguration.ConfigurazioneHero();
                HeroService heroService = serviceProvider2.GetService<HeroService>();

                Console.WriteLine("Hai scelto di eliminare un eroe!");
                //Funzionalità.Funzionalità.GetHeroByPlayerID(id); //mi da gli eroi associati ad un id di un giocatore

                var heros = heroService.GetHeroByID(id);
                //var presentHero = HeroService.areHeroPresent(heros);    //mi dice se ci sono eroi associati all'ID; 
                //Console.WriteLine("I tuoi eroi sono: ");
                foreach (var h in heros)
                {
                    Console.WriteLine($"Nome: {h.name}\t - Classe: {h.classPerson}\t - Livello: {h.level}\t - Punti Vita:{h.lifePoint}");
                }
                Console.WriteLine("Inserisci il nome del personaggio che vuoi eliminare");
               
            
            inserimento:

                try
                {
                    var answer = Console.ReadLine();
                    foreach (var h in heros)
                    {
                        if (h.name == answer)
                        {
                            heroService.DeleteHero(h);
                        }
                       
                    }
                    Console.WriteLine("E stato eliminato!");
                    goto menu;


                }
                catch
                {
                    Console.WriteLine("inserimento non valido");
                    goto inserimento;
                }
             
            }


        }
    }
}
