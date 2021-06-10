using System;

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
            
            Active();

            this.DisplayBuffs();

            CurrentHealth = MaxHealth;
            CurrentAttack = MaxAttack;

            Console.WriteLine(ToString());
        }
        
        // Passive ability - always active
        public override void Passive() //Increased evasion and attack
        {
            TotalEvasion += _passiveEvasionBonus;
            MaxAttack += (int) (MaxAttack * _passiveAttackMultiplier);
        }
        
        //Active ability - activated by player
        public override void Active() //Raise evasion by 40% for 1 turn
        {
            var raiseEvasion = new Buff("Evasive", 1);
            
            void ActiveBuff()
            {
                TotalEvasion += _activeEvasionBonus;
            }
            int energyConsumption = 300;
            if (CurrentEnergy >= energyConsumption)
            {
                CurrentEnergy -= energyConsumption;
                ApplyBuff(raiseEvasion, ActiveBuff);
            }
            else
            {
                Console.WriteLine("You do not have enough energy!");
            }
        }
        
        //Ultimate - activated by player
        public override void Ultimate() //Go invulnerable for one attack stage and raise attack by 25%
        {
            double difference = 1 - TotalEvasion;
            TotalEvasion += difference; //caps evasion at 1.0
        }



    }
}