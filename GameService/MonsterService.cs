using HeroVSMonster.Core.Entities;
using HeroVSMonster.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameService
{
    public class MonsterService
    {
        private IMonsterRepository _repo;
        public MonsterService(IMonsterRepository repo)
        {
            _repo = repo;
        }

        public List<Monster> GetAllMonster(Hero h)
        {
            return _repo.GetAll(h);
        }


    }
}
