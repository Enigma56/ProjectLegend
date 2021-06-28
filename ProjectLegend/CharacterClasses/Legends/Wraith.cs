using System;
using ProjectLegend.GameUtilities;
namespace ProjectLegend.CharacterClasses.Legends
{
    public sealed class Wraith : Player
    {
        //Passive
        private double _passiveEvasionBonus = .05;
        private double _passiveAttackMultiplier = .05; //in percent
        private int PassiveAttackIncrease { get; set; }

        private int PassiveDifference { get; set; }
        //Active
        private double _activeEvasionBonus = .3;
        
        //Ultimate
        private double _ultimateAttackMultiplier = .25;
        private double EvasionDifference { get; set; }
        private int AttackDifference { get; set; }
        
        public Wraith()
        {
            MaxHealth = 50;
            MaxAttack = 20;
            
            CurrentEnergy = 0;

            Passive();

            CurrentHealth = MaxHealth;
            CurrentAttack = MaxAttack;
        }
        
        
        public override void Passive() //Increased evasion and attack
        {
            TotalEvasion += _passiveEvasionBonus;
            
            PassiveAttackIncrease = (int) (MaxAttack * _passiveAttackMultiplier);
            MaxAttack += PassiveAttackIncrease;

            CanUpdatePassive = true;
        }

        public override void UpdatePassive() //updates the passive stats per level 
        {
            int oldPassiveIncrease = PassiveAttackIncrease;
            PassiveAttackIncrease = (int) ((MaxAttack - oldPassiveIncrease) * _passiveAttackMultiplier);
            MaxAttack -= oldPassiveIncrease;
            MaxAttack += PassiveAttackIncrease;

            CurrentAttack = MaxAttack;
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
            
            Buff evasive = new Buff("Evasive", 1);
            evasive.ApplyEffect = AddEvasive;
            evasive.RemoveEffect = RemoveEvasive;

            evasive.Apply(this, 300);
        }

        //Ultimate - activated by player
        public override void Ultimate(Enemy enemy) //Go invulnerable for one attack stage and raise attack by 25%
        {

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
            void RaiseAttack(Character character)
            {
                AttackDifference = (int) Math.Ceiling(CurrentAttack * _ultimateAttackMultiplier);
                CurrentAttack += AttackDifference;
            }
            void RemoveAttack(Character character)
            {
                CurrentAttack -= AttackDifference;
                AttackDifference = 0;
            }
            var invulnerability = new Buff("Spectral Movement", 1);
            var raiseAttack = new Buff("Sharpened Mind", 1);
            
            invulnerability.ApplyEffect = Invulnerability;
            invulnerability.RemoveEffect = RemoveInvulnerability;
            raiseAttack.ApplyEffect = RaiseAttack;
            raiseAttack.RemoveEffect = RemoveAttack;

            
            Buff[] buffs = { invulnerability, raiseAttack };

            this.ApplyMultipleBuffs(buffs, 500);
        }
    }
}