using System;

using ProjectLegend.GameUtilities.FuncUtils;
using ProjectLegend.GameUtilities.BuffUtilities;

namespace ProjectLegend.CharacterClasses.Legends
{
    public sealed class PortalGirl : Player
    {
        public static string Name = "PortalGirl";
        //Passive
        private double _passiveEvasionBonus = .05;
        private double _passiveAttackMultiplier = .05; //in percent
        private int PassiveAttackIncrease { get; set; }

        //Active
        private double _activeEvasionBonus = .3;
        
        //Ultimate
        private double _ultimateAttackMultiplier = .25;
        private double EvasionDifference { get; set; }
        private int AttackDifference { get; set; }

        public PortalGirl()
        {
            Health.Max = 50;
            Attack.Max = 20;
            Health.Current = Health.Max;
            Attack.Current = Attack.Max;
            
            ActiveCost = 300;
            UltimateCost = 600;

            Passive();
            CanUpdatePassive = true;
        }
        
        
        public override void Passive() //Increased evasion and attack
        {
            Evasion.Total += _passiveEvasionBonus;
            
            PassiveAttackIncrease = (int) (Attack.Max * _passiveAttackMultiplier);
            Attack.Max += PassiveAttackIncrease;
        }

        public override void UpdatePassive() //updates the passive stats per level 
        {
            int oldPassiveIncrease = PassiveAttackIncrease;
            PassiveAttackIncrease = (int) ((Attack.Max - oldPassiveIncrease) * _passiveAttackMultiplier);
            Attack.Max -= oldPassiveIncrease;
            Attack.Max += PassiveAttackIncrease;

            Attack.Current = Attack.Max;
        }
        
        //Active ability - activated by player
        public override void Active(Enemy enemy) //Raise evasion by 40% for 1 turn
        {
            void AddEvasive(Character player)
            {
            Evasion.Total += _activeEvasionBonus;
            }
            void RemoveEvasive(Character player)
            {
                Evasion.Total -= _activeEvasionBonus;
            }
            
            Buff evasive = new Buff("Evasive", 1);
            evasive.ApplyEffect = AddEvasive;
            evasive.RemoveEffect = RemoveEvasive;

            evasive.Apply(this, ActiveCost);
        }

        //Ultimate - activated by player
        public override void Ultimate(Enemy enemy) //Go invulnerable for one attack stage and raise attack by 25%
        {
            void Invulnerability(Character player)
            {
                EvasionDifference = 1 - Evasion.Total;
                Evasion.Total += EvasionDifference; //caps evasion at 1.0
            }
            void RemoveInvulnerability(Character player)
            {
                Evasion.Total -= EvasionDifference;
                EvasionDifference = 0;
            }
            void RaiseAttack(Character character)
            {
                AttackDifference = (int) Math.Ceiling(Attack.Current * _ultimateAttackMultiplier);
                Attack.Current += AttackDifference;
            }
            void RemoveAttack(Character character)
            {
                Attack.Current -= AttackDifference;
                AttackDifference = 0;
            }
            var invulnerability = new Buff("Spectral Movement", 1);
            var raiseAttack = new Buff("Sharpened Mind", 1);
            
            invulnerability.ApplyEffect = Invulnerability;
            invulnerability.RemoveEffect = RemoveInvulnerability;
            raiseAttack.ApplyEffect = RaiseAttack;
            raiseAttack.RemoveEffect = RemoveAttack;

            
            Buff[] buffs = { invulnerability, raiseAttack };

            this.ApplyMultipleBuffs(buffs, UltimateCost);
        }
    }
}