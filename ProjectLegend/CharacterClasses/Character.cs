using System;
using System.Collections.Generic;

namespace ProjectLegend.CharacterClasses
{
    public class Character
    {
        public List<Buff> Buffs { get; init; }
        
        //Character Stats
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }

        public int MaxAttack { get; set; }
        public int CurrentAttack { get; set; }
        
        public double Accuracy { get; set; }
        
        //Character states
        public bool Invulnerable { get; set; }
        public bool Stunned { get; set; }
        public bool Dead { get; set; }
        
        protected Character() //protected prevents Character from being instantiated anywhere except within Player and Enemy
        {
            Buffs = new List<Buff>();
        }
    }
}