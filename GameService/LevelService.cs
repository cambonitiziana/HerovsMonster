using HeroVSMonster.Core;
using HeroVSMonster.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameService
{
    public class LevelService
    {
        private ILevelRepository _repo;

        public LevelService(ILevelRepository repo)
        {
            _repo = repo;
        }

        public List<Livello> GetLivelliInfo() 
        {
            return _repo.GetAll();
                
        }
    }
}
