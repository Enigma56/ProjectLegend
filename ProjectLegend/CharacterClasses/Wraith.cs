using System;
using System.Runtime.CompilerServices;

namespace ProjectLegend.CharacterClasses
{
    public sealed class Wraith : Player
    {
        private int _abilityEnergyConsumption;
        private double _passiveEvasionBonus = .05;
        private double _passiveAttackMultiplier = .05; //in percent
        private double _ultimateAttackMultiplier = .25;
        
        private Buff _evasive = new Buff("Evasive", 1);
        private Buff _invulnerability = new Buff("Spectral Movement", 1);
        private Buff _raiseAttack = new Buff("Sharpened Mind", 1);
        
        private double EvasionDifference { get; set; }
        private int AttackDifference { get; set; }

        private double _activeEvasionBonus = .3;
        public Wraith()
        {
            MaxHealth = 50;
            MaxAttack = 20;
            CurrentEnergy = 1000;

            Passive();

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
        public override void Active(Enemy enemy) //Raise evasion by 40% for 1 turn
        {
            void AddEvasive(Character player)
            {
            TotalEvasion += _activeEvasionBonus;
            }
            void RemoveEvasive(Character player)
            {
                TotalEvasion -= _activeEvasionBonus;
            }
            _abilityEnergyConsumption = 300;
            
            _evasive.ApplyEffect = AddEvasive;
            _evasive.RemoveEffect = RemoveEvasive;

            _evasive.Apply(this, _abilityEnergyConsumption);
        }

        //Ultimate - activated by player
        public override void Ultimate(Enemy enemy) //Go invulnerable for one attack stage and raise attack by 25%
        {
            _abilityEnergyConsumption = 500;
            
            void Invulnerability(Character player)
            {
                EvasionDifference = 1 - TotalEvasion;
                TotalEvasion += EvasionDifference; //caps evasion at 1.0
            }
            void RemoveInvulnerability(Character player)
            {
                TotalEvasion -= EvasionDifference;
                EvasionDifference = 0;
            }
            void RaiseAttack(Character player)
            {
                AttackDifference = (int) Math.Ceiling(CurrentAttack * _ultimateAttackMultiplier);
                CurrentAttack += AttackDifference;
            }
            void RemoveAttack(Character player)
            {
                CurrentAttack -= AttackDifference;
                AttackDifference = 0;
            }
            
            _invulnerability.ApplyEffect = Invulnerability;
            _invulnerability.RemoveEffect = RemoveInvulnerability;
            _raiseAttack.ApplyEffect = RaiseAttack;
            _raiseAttack.RemoveEffect = RemoveAttack;
            
            _invulnerability.Apply(this, _abilityEnergyConsumption/2);
            _raiseAttack.Apply(this, _abilityEnergyConsumption/2);
        }
    }
}