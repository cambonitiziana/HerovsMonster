using HeroVSMonster.Core.Entities;
using HeroVSMonster.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameService
{
    public class StatisticsService

    {
        private IStatisticsRepository _repo;

        public StatisticsService (IStatisticsRepository repo)
        {
            _repo = repo;
        }

        public List<MatchStatistics> GetAll(Player p)
        {
            return _repo.getAll(p);
        }

        public void Update(Hero h)
        {
            _repo.update(h);
        }

    }
}
