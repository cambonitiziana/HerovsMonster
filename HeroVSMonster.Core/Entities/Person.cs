using System;
using System.Collections.Generic;
using System.Text;

namespace HeroVSMonster.Core.Entities
{
    public abstract class Person: Entity
    {
        public int level { get; set; }
        public int lifePoint { get; set; }
        public string classPerson { get; set; }
        public Weapon weapon { get; set; }
        public int score { get; set; }
    }
}
