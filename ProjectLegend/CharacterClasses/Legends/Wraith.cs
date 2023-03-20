using System;
using System.Runtime.CompilerServices;
using ProjectLegend.GameUtilities.FuncUtils;
using ProjectLegend.GameUtilities.BuffUtilities;

namespace ProjectLegend.CharacterClasses.Legends
{
    public sealed class Wraith : Character
    {
        public static string Name = "Wraith";
        //Passive
        private readonly double _passiveEvasionBonus = .05;
        private double _passiveAttackMultiplier = .05; //in percent
        private int PassiveAttackIncrease { get; set; }

        //Active
        private double _activeEvasionBonus = .3;
        
        //Ultimate
        private double _ultimateAttackMultiplier = .25;
        private double EvasionDifference { get; set; }
        private int AttackDifference { get; set; }

        
        public Wraith() { }

        public void SetPlayerAsWraith()
        {
            Player.Instance.Health.Max = 50;
            Player.Instance.Health.Current = Player.Instance.Health.Max;
            Player.Instance.ActiveCost = 300;
            Player.Instance.UltimateCost = 600;

            Player.Instance.Passive = Passive;
            Player.Instance.UpdatePassive = UpdatePassive;
            Player.Instance.Active = Active;
            Player.Instance.Ultimate = Ultimate;

            Passive();
            Player.Instance.CanUpdatePassive = true;
        }

        private void Passive() //Increased evasion and attack
        {
            Player.Instance.Evasion.Total += _passiveEvasionBonus;
            
            PassiveAttackIncrease = (int) (Attack.Max * _passiveAttackMultiplier);
            Attack.Max += PassiveAttackIncrease;
        }

        private void UpdatePassive() //updates the passive stats per level 
        {
            int oldPassiveIncrease = PassiveAttackIncrease;
            PassiveAttackIncrease = (int) ((Attack.Max - oldPassiveIncrease) * _passiveAttackMultiplier);
            Attack.Max -= oldPassiveIncrease;
            Attack.Max += PassiveAttackIncrease;

            Attack.Current = Attack.Max;
        }
        
        //Active ability - activated by player
        private void Active(Enemy enemy) //Raise evasion by 40% for 1 turn
        {
            void AddEvasive(Character player)
            {
            Player.Instance.Evasion.Total += _activeEvasionBonus;
            }
            void RemoveEvasive(Character player)
            {
                Player.Instance.Evasion.Total -= _activeEvasionBonus;
            }
            
            Buff evasive = new Buff("Evasive", 1);
            evasive.ApplyEffect = AddEvasive;
            evasive.RemoveEffect = RemoveEvasive;

            evasive.Apply(this, Player.Instance.ActiveCost);
        }

        //Ultimate - activated by player
        private void Ultimate(Enemy enemy) //Go invulnerable for one attack stage and raise attack by 25%
        {
            void Invulnerability(Character player)
            {
                EvasionDifference = 1 - Player.Instance.Evasion.Total;
                Player.Instance.Evasion.Total += EvasionDifference; //caps evasion at 1.0
            }
            void RemoveInvulnerability(Character player)
            {
                Player.Instance.Evasion.Total -= EvasionDifference;
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

            this.ApplyMultipleBuffs(buffs, Player.Instance.UltimateCost);
        }
    }
}