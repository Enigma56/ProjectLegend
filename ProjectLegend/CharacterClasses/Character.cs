using System;
using System.Collections.Generic;
using ProjectLegend.GameUtilities.BuffUtilities;
using ProjectLegend.ItemClasses.GearClasses;

namespace ProjectLegend.CharacterClasses
{
    /// <summary>
    /// Contains field variables that can belong to either an enemy or a player
    /// </summary>
    public class Character
    {
        public List<Buff> Buffs { get; init; }
        
        //Character Stats
        public HealthPoints Health { get; set; }
        
        public AttackPoints Attack { get; set; }
        
        public double Accuracy { get; set; }
        
        //Character states
        public bool Invulnerable { get; set; }
        public bool Stunned { get; set; }
        public bool Dead { get; set; }
        
        protected Character() //protected prevents Character from being instantiated anywhere except within Player and Enemy
        {
            Health = new HealthPoints();
            Attack = new AttackPoints();

            Buffs = new List<Buff>();
        }

    }
}