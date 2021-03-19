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

        public Monster CreateMonster(Monster m)
        {
            if (m != null)
            {
                _repo.Create(m);
                return m;
            }
            else
                return null;
        }


    }
}
