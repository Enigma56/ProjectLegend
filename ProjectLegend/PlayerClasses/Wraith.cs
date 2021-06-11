using System;
using System.Runtime.CompilerServices;

namespace ProjectLegend.PlayerClasses
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
            Active();
            
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
            void AddEvasive(Player player)
            {
            TotalEvasion += _activeEvasionBonus;
            }
            void RemoveEvasive(Player player)
            {
                TotalEvasion -= _activeEvasionBonus;
            }
            _abilityEnergyConsumption = 300;
            
            _evasive.ApplyEffect = AddEvasive;
            _evasive.RemoveEffect = RemoveEvasive;

            this.CheckBuffApplication(_evasive, _abilityEnergyConsumption);
        }

        //Ultimate - activated by player
        public override void Ultimate() //Go invulnerable for one attack stage and raise attack by 25%
        {
            _abilityEnergyConsumption = 500;
            
            void Invulnerability(Player player)
            {
                EvasionDifference = 1 - TotalEvasion;
                TotalEvasion += EvasionDifference; //caps evasion at 1.0
            }
            void RemoveInvulnerability(Player player)
            {
                TotalEvasion -= EvasionDifference;
                EvasionDifference = 0;
            }
            void RaiseAttack(Player player)
            {
                AttackDifference = (int) Math.Ceiling(CurrentAttack * _ultimateAttackMultiplier);
                CurrentAttack += AttackDifference;
            }
            void RemoveAttack(Player player)
            {
                CurrentAttack -= AttackDifference;
                AttackDifference = 0;
            }
            
            _invulnerability.ApplyEffect = Invulnerability;
            _invulnerability.RemoveEffect = RemoveInvulnerability;
            
            _raiseAttack.ApplyEffect = RaiseAttack;
            _raiseAttack.RemoveEffect = RemoveAttack;

            Buff[] buffs = { _invulnerability, _raiseAttack };
            this.CheckBuffApplication(buffs, _abilityEnergyConsumption);
        }
    }
}