using HeroVSMonster.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeroVSMonster.Core.Interface
{
    public interface IWeaponRepository:IRepository<Weapon>
    {
        public List<Weapon> GetAll(Hero h);
    }
}
