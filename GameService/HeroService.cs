using HeroVSMonster.Core.Entities;
using HeroVSMonster.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameService
{
    public class HeroService
    {
        private IHeroRepository  _repo;
        public HeroService(IHeroRepository repo)
        {
            _repo = repo;
        }
        public IEnumerable<Hero> GetAllHeros()
        {
            return _repo.GetAll();
        }
        public List<Hero> GetHeroByID(int n)
        {
            //logica di business

            if (n > 0)
            {
                return _repo.GetHerossByID(n);
                
            }
            return null;
        }
        public static bool areHeroPresent(List<Hero> herosList)
        {
            if (herosList == null)
            {
                Console.WriteLine("Non sono presenti eroi associati al tuo nome!");
                return false;
            }
            else if  (herosList.Count >= 1)
            {
                Console.WriteLine("I tuoi eroi sono: ");
                Console.WriteLine($" Nome\t - Classe\t - Livello\t - Punti Vita");
                foreach (var h in herosList)
                {
                    Console.WriteLine($"{h.name}\t - {h.classPerson}\t - {h.level}\t - {h.lifePoint}");
                }
                Console.WriteLine("Vuoi utilizzare un eroe già create?");
                var answer = Funz.CheckAnswer();
                return answer;
            }
            else
            {
                Console.WriteLine("Non sono presenti eroi associati al tuo nome!");
                return false;
            }
        }
        public Hero CreateHero(Hero h,int id)
        {
            if (h != null)
            {
                _repo.Create(h,id);
                return h;
            }
            else
                return null;
        }
        public Hero DataNewHero()
        {
            Hero NewHero = new Hero();
            Console.WriteLine("Il nuovo ero sarà: \n \t\t\t A - Guerriero  \t\t\t B - Mago ");
            inserimento:
                Console.WriteLine("                      \t\t\t      Premi A   \t\t\t Premi B ");
                string ans = Console.ReadLine();
                if (ans == "a")
                {
                    NewHero.classPerson = "Guerriero";
                }
                else if (ans == "b")
                {
                    NewHero.classPerson = "Mago";
                }
                else
                {
                    Console.WriteLine("inserimento non valido!");
                    goto inserimento;
                }
            Console.WriteLine("Inserisci il nome del nuovo eroe:");
            NewHero.name = Console.ReadLine();
            NewHero.level = 1;
            NewHero.lifePoint = 20;
            NewHero.score = 0;

            return NewHero;
        }
        public bool DeleteHero(Hero h)
        {
            return _repo.Delete(h);
        }
        public void Update(Hero h)
        {
            _repo.Update(h);
        }

    }
}
