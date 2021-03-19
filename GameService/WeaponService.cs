using HeroVSMonster.Core.Entities;
using HeroVSMonster.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameService
{
    public class WeaponService
    {
        private IWeaponRepository _repo;
        public WeaponService(IWeaponRepository repo)
        {
            _repo = repo;
        }

        public List<Weapon> GetAllWeapons(Hero h)
        {
            return _repo.GetAll(h);
        }
    }
}
