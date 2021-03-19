using HeroVSMonster.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeroVSMonster.Core.Interface
{
    public interface IStatisticsRepository
    {
       public List<MatchStatistics> getAll(Player p);
       public void update(Hero h );
       public void delete(Hero h);


    }
}
