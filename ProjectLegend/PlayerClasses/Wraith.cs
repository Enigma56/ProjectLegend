using System;
using System.Linq.Expressions;

namespace ProjectLegend.PlayerClasses
{
    public sealed class Wraith : Player
    {
        private double _passiveEvasionBonus = .05;
        private double _passiveAttackMultiplier = .05; //in percent

        private double _activeEvasionBonus = .3;
        public Wraith()
        {
            MaxHealth = 50;
            MaxAttack = 20;

            Passive();
            
            CurrentHealth = MaxHealth;
            CurrentAttack = MaxAttack;

            Console.WriteLine(ToString());
        }
        
        // Passive ability - always active
        public override void Passive() //Increased evasion and attack
        {
            Evasion += _passiveEvasionBonus;
            MaxAttack += (int) (MaxAttack * _passiveAttackMultiplier);
        }
        
        //Active ability - activated by player
        public override void Active() //Raise evasion by 40% for 1 turn
        {
            int energyConsumption = 300;
            
            Evasion += _activeEvasionBonus;
            
        }
        
        //Ultimate - activated by player
        public override void Ultimate() //Go invulnerable for one attack stage and raise attack by 25%
        {
            double difference = 1 - Evasion;
            Evasion += difference;
        }



    }
}