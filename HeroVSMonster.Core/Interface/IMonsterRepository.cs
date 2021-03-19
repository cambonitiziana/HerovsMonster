using HeroVSMonster.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeroVSMonster.Core.Interface
{
   public interface IMonsterRepository: IRepository<Monster>
    {
        public List<Monster> GetAll(Hero h);
    }
}
