using HeroVSMonster.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeroVSMonster.Core.Interface
{
    public interface IHeroRepository: IRepository<Hero>
    {
        public List<Hero> GetHerossByID(int n);
        //public void Create2(Hero newHero, int id);

        public void Create(Hero newHero, int id);
    }
}
