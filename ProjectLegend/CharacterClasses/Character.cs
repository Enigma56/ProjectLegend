using System;
using System.Collections.Generic;
using ProjectLegend.GameUtilities.BuffUtilities;

namespace ProjectLegend.CharacterClasses
{
    public class Character
    {
        public List<Buff> Buffs { get; init; }
        
        //Character Stats
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        public int MaxAttack { get; set; }
        public double AttackMultiplier { get; set; }
        public double Accuracy { get; set; }
        
        //Character states
        public bool Invulnerable { get; set; }
        public bool Stunned { get; set; }
        public bool Dead { get; set; }
        
        protected Character() //protected prevents Character from being instantiated anywhere except within Player and Enemy
        {
            Buffs = new List<Buff>();
            AttackMultiplier = 1.0;
        }
        
        //All getter/setters that are not auto-properties
        private int _currentAttack;
        public int CurrentAttack
        {
            get => (int) (_currentAttack * AttackMultiplier);
            set => _currentAttack = value;
        }
    }
}