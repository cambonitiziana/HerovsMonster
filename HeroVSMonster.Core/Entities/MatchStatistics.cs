using System;
using System.Collections.Generic;
using System.Text;

namespace HeroVSMonster.Core.Entities
{
   public class MatchStatistics
    {
        public string nameOfHero { get; set; }
        public int winnings { get; set; }
        public int totalMatch { get; set; }
        public decimal time { get; set; }
    }
}
