using GameService;
using HeroVSMonster.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using HeroVSMonster.Core;

namespace HeroVSMonster.Funzionalità
{
    public class Funzionalità
    {


        public static (int, Player) MatchPredisposition()
        {
            Console.WriteLine(" BENVENUTO \nHero VS Monster");

            var serviceProvider = DIConfiguration.ConfigurazionePlayer();
            PlayerService playerService = serviceProvider.GetService<PlayerService>();
            Console.WriteLine("Inserisci nome utente: ");
            string nomePlayer = Console.ReadLine();

            List<Player> players = playerService.GetAllPlayer();

            (bool answer, Player p) = PlayerService.IsRegistred(nomePlayer, players); //é registrato? true: si, false: no
            //se è registrato ricavo il suo ID 

            if (answer == false)
            {
                //Se non é registrato, lo aggiungo e ricavo il suo ID
                var NewPlayer = PlayerService.Registration(nomePlayer);
                var pl = playerService.CreatePlayer(NewPlayer);
                p.ID = PlayerService.GetPlayerID(players, pl);
                //id = PlayerService.GetExistingPlayerID(players);

            }
            return (p.ID, p);
        }

        public static string isAdmin(Player p)
        {
            string answer;
            if (p.Admin)
            {
                Console.WriteLine("D - Crea mostro\nE - Vedi Statistiche");
            }
        inserimento:

            answer = Console.ReadLine();
            if (answer == "d")
            {
                Console.WriteLine("Hai deciso di creare un mostro");
                CreateMonster();
                return answer;
            }
            else if (answer == "e")
            {
                var serviceProvider5 = DIConfiguration.ConfigurazioneStatistics();
                StatisticsService statisticsService = serviceProvider5.GetService<StatisticsService>();
                var statistics=statisticsService.GetAll(p);
                foreach (var s in statistics)
                {
                    Console.WriteLine($"Nome Eroe: {s.nameOfHero} - Partite vinte: {s.winnings} - Partite Totali:  {s.totalMatch} Tempo di gioco totale[m]:{s.time} ");
                }
            }
            else if (answer == "a" || answer == "b" || answer == "c")
            {
                return answer;
            }
            else
            {
                Console.WriteLine("inserimento non valido");
                goto inserimento;
            }

                return answer;
        }

        public static Hero GetHeroByPlayerID(int id)
        {
            var serviceProvider2 = DIConfiguration.ConfigurazioneHero();
            HeroService heroService = serviceProvider2.GetService<HeroService>();

            var heros = heroService.GetHeroByID(id);
            var presentHero = HeroService.areHeroPresent(heros);

            Hero fightingHero = new Hero(); //decido quale è l'eroe combattente

            if (presentHero)
            {
                Console.WriteLine("Inserisci il nome dell'eroe scelto:");
                string heroName = Console.ReadLine();
                foreach (var h in heros)
                {
                    if (h.name == heroName)
                    {
                        fightingHero = h;
                    }

                }
            }
            else
            {
                try
                {
                    var newHero = heroService.DataNewHero();
                    var Hero = heroService.CreateHero(newHero, id);
                    fightingHero = Hero;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Giocatore non creato" + e.Message);
                }
            }
            return fightingHero;
        }
        public static Weapon GetWeapons(Hero fightingHero)
        {
            var serviceProvider3 = DIConfiguration.ConfigurazioneWeapon();
            WeaponService weaponService = serviceProvider3.GetService<WeaponService>();
            Console.WriteLine("Le armi a tua disposizione sono:");

        weapon:
            //List<Weapon> weapons = new List<Weapon>;
            var weapons = weaponService.GetAllWeapons(fightingHero);
            for (int i = 0; i < weapons.Count(); i++)
            {
                Console.WriteLine("{0} - {1}", i, weapons[i].name);
            }

            Console.WriteLine("Inserisci il numero dell'arma che hai scelto:");
            try
            {
                int index = Convert.ToInt32(Console.ReadLine());
                Weapon w = weapons[index];
                Console.WriteLine("Il tuo personaggio è un {0} di nome {1} la cui arma è {2}", fightingHero.classPerson, fightingHero.name, w.name);
                return w;
            }
            catch (Exception e)
            {
                Console.WriteLine("Inserimento non valido");
                goto weapon;
            }

        }
        public static Monster GetMonster(Hero fightingHero)
        {

            var serviceProvider4 = DIConfiguration.ConfigurazioneMonster();
            MonsterService MonsterService = serviceProvider4.GetService<MonsterService>();

            var monster = MonsterService.GetAllMonster(fightingHero); //mi rende i mostri con livello uguale o mini
            var random = new Random();


            var serviceProvider3 = DIConfiguration.ConfigurazioneLevel();
            LevelService levelService = serviceProvider3.GetService<LevelService>();

            var Levels = levelService.GetLivelliInfo();
            int index = random.Next(monster.Count);
            Monster fightingMonster = monster[index];

            for (int i = 0; i < Levels.Count; i++)
            {
                if (Levels[i].livello == fightingMonster.level)
                {
                    fightingMonster.lifePoint = Levels[i].lifePoint;
                }
            }

            Console.WriteLine("Stai sfidando {0} la cui arma è  {1}", fightingMonster.classPerson, fightingMonster.weapon.name);

            return (fightingMonster);
        }
        public static void CreateMonster()
        {
            var serviceProvider4 = DIConfiguration.ConfigurazioneMonster();
            MonsterService MonsterService = serviceProvider4.GetService<MonsterService>();

            Console.WriteLine("Categoria del mostro: ");
            string c = Console.ReadLine();

            Console.WriteLine("Livello: ");
            int l = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Arma: ");
            string weaponName = Console.ReadLine();
            Console.WriteLine("Punti danno dell'arma MUOOOSTROSA: ");
            int damagePoint = Convert.ToInt32(Console.ReadLine());

            Monster m = new Monster()
            {
                classPerson = c,
                level = l,
                weapon = new Weapon
                {
                    name = weaponName,
                    damagePoint = damagePoint
                }

            };
            MonsterService.CreateMonster(m);
                }
        #region Match
        public static (Monster,Hero,bool) HeroChoice(Monster m, Hero h)
        {
            Console.WriteLine("Vuoi attaccare o tentare la fuga? ");
            bool answer = AttackOrEscape();
            bool escaped = false; 
            if (answer==true) // attacca
            {
                Console.WriteLine("{0}, {1} con arma {2} attacca {3} con arma {4}", h.name, h.classPerson, h.weapon.name, m.classPerson, m.weapon.name);
                bool outcome = isSucesfull();
                if (outcome) // attacco ha avuto successo incremento punteggio Score
                {
                    m.lifePoint -= h.weapon.damagePoint;
                    Console.WriteLine("Il tuo attacco ha avuto successo");
                    
                }
                else
                {
                    Console.WriteLine("OUCH! Il tuo attacco è fallito!!!", m.classPerson);
                   // h.lifePoint -= m.weapon.damagePoint;
                   
                }
            }
            else  //tenta la fuga
            {
                Console.WriteLine("SCAPPPPAAAAAAAAAAAAAAAAAA!!!!!!!!!!!!");
                bool outcome = isSucesfull();
                if (outcome) //fuga riuscita
                {
                    Console.WriteLine("Sei stato velocissimo e sei riuscito a scappare!");
                    h.score -= (m.level * 5);
                    escaped = true;
                }
                else
                {
                    Console.WriteLine("{0} è stato più veloce di te", m.classPerson);
                    h.lifePoint -= m.weapon.damagePoint; 
                    
                }
            }
            return (m, h, escaped);

        }
        public static (Monster, Hero) MonsterAttack(Monster m, Hero h)
        {
            bool outcome = isSucesfull();
            if (outcome)
            {
                Console.WriteLine("AIII! L'attacco del mostro è andato a buon fine");
                h.lifePoint -= m.weapon.damagePoint;
               
            }
            else
            {
                Console.WriteLine("WOW! che riflessi! Te la sei scampata!");
                
            }
            return (m, h);
        }
        public static Hero Match(Hero h)
        {
            //if (h.Statistcs.totalMatch == null)
            //{
            //    h.Statistcs.totalMatch = 0;
            //}
            h.Statistcs.winnings ++;
            var startTime = DateTime.Now;
            Monster m = Funzionalità.GetMonster(h);
            do
            {
                (Monster monster, Hero hero, bool escaped) = HeroChoice(m, h);

                if (escaped)
                { 
                    h = hero;
                    return h;
                }
                else
                {
                    Console.WriteLine("Ora sta attaccando il mostroooooo!");
                    ( m, h) = MonsterAttack(monster, hero);
                    
                    
                    if (m.lifePoint <= 0)
                    {
                        m.lifePoint = 0;
                    }
                    else if (h.lifePoint <= 0)
                    {
                        h.lifePoint = 0;
                    }
                    displayInfo(m, h);
                    h = checkTheWinner(m, h);
                }
            } while (h.lifePoint > 0 && m.lifePoint > 0);
            var stopTime = DateTime.Now;
            var interval = startTime - stopTime;
            //if (h.Statistcs.time == null)
            //{
            //    h.Statistcs.time = 0;
            //}
            h.Statistcs.time+=Convert.ToDecimal(interval.TotalMilliseconds);
            Console.WriteLine("Partita finita!");
            return h;
        }

        public static Hero checkTheWinner(Monster m, Hero h)
        {
            if (m.lifePoint == 0)
            {
                Console.WriteLine("Ha vinto il {0} {1}", h.classPerson, h.name);
                h.score = h.score + (10 * m.level);
                if (h.Statistcs.winnings == null)
                { 
                    h.Statistcs.winnings = 0;
                }
                h.Statistcs.winnings ++;
            }
            else if (h.lifePoint == 0)
            {
                Console.WriteLine("Ha vinto il {0} \nIl tuo personaggio verrà eliminato definitivamente!", m.classPerson);
            }
            return h;
        }
        #endregion
        public static Hero updateLevel(Hero h)
        {
            var serviceProvider3 = DIConfiguration.ConfigurazioneLevel();
            LevelService levelService = serviceProvider3.GetService<LevelService>();

            var Levels = levelService.GetLivelliInfo();
            Livello l = new Livello();
            for (int i = 1; i < Levels.Count; i++)
            {
                if (h.score >= Levels[i].score)
                {
                    h.level = Levels[i].livello;
                    h.lifePoint = Levels[i].lifePoint;
                }
            }
            return h;
        }
        public static bool AttackOrEscape()
        {
         Richiesta:
            Console.WriteLine("Attacca - Premi A \t\t\t Scappa  - Premi S");
            string answer = Console.ReadLine();
            if (answer == "a")
            {
                return true;
            }
            else if (answer == "s")
            {
                return false;
            }
            else
            {
                Console.WriteLine("Opzione non valida");
                goto Richiesta;
            }
        }
        public static bool isSucesfull()
        {
            var random = new Random();
            int i = random.Next();
            if (i % 2 == 0)// se è pari attacco/fuga è andata a buon fine
            {
                return true;
            }
            else  // se è dispari l'attacco/fuga non è andata a buon fine
            { 
                return false;
            }
        }
        public static void displayInfo(Monster m, Hero h)
        {
            Console.WriteLine("{0} - punti vita: {1} - Punteggio: {2}", h.name, h.lifePoint, h.score);
            Console.WriteLine("{0} - punti vita: {1} - Punteggio: {2}", m.classPerson, m.lifePoint, m.score);
        }
        
    }
}
