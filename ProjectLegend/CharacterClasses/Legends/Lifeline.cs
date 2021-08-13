using System;

using ProjectLegend.GameUtilities.BuffUtilities;
using ProjectLegend.ItemClasses.GearClasses;

namespace ProjectLegend.CharacterClasses.Legends
{
    public sealed class Lifeline : Player
    {
        public Lifeline()
        {
            Health.Max = 40;
            Attack.Max = 20;
            Health.Current = Health.Max;
            Attack.Current = Attack.Max;

            ActiveCost = 300;
            UltimateCost = 600;

            CanUpdatePassive = false;
        }
        
        public override void Passive()
        {
            throw new NotImplementedException();
        }

        public void PassiveHeal()
        {
            //Heal 5% health every turn - rounded up
            int heal = (int) (Health.Max * .05);

            if (Health.Current + heal > Health.Max)
                Health.Current = Health.Max;
            else
            {
                Health.Current += heal;
            }
        }

        public override void UpdatePassive()
        {
            //throw new NotImplementedException();
        }

        public override void Active(Enemy enemy) //heals 20% of current max health over 5 turns
        {
            var drone = new TurnBuff("Healing Drone", 5);
            drone.HasTurnEffect = true;
            
            void HealApply(Character character) //Snapshot of current Max Health
            {
                int healthPerTurn = Health.Max / drone.Duration;
                if (Health.Current == Health.Max)
                {
                    Console.WriteLine("Cannot go above max health!");
                }
                else if (Health.Current + healthPerTurn > Health.Max)
                {
                    Health.Current = Health.Max;
                    Console.WriteLine("You have hit max health!");
                }
                else
                {
                    Health.Current += healthPerTurn;
                }

            }
            void HealRemove(Character character)
            {
            }
            
            //var drone = new Buff("Healing Drone", 5);
            drone.TurnEffect = HealApply;
            drone.RemoveEffect = HealRemove;
            
            drone.Apply(this, ActiveCost);
        }

        public override void Ultimate(Enemy enemy) 
        {
            //Real ulti cannot be added until gear is added into the game
            
            //Temp. ultimate
            if (Energy.Current >= UltimateCost)
            {
                Console.WriteLine($"You have been fully healed! {Health.Current} -> {Health.Max}");
                Health.Current = Health.Max;
            }
            else
            {
                Console.WriteLine("Not enough energy!");
            }
        }
    }
}