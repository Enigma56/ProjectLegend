using System;
using ProjectLegend.Items.Consumables;

namespace ProjectLegend.CharacterClasses.Legends
{
    public class Lifeline : Player
    {
        public Lifeline()
        {
            MaxHealth = 100;
            MaxAttack = 20;
            
            CurrentEnergy = 0;

            Passive();

            CurrentHealth = MaxHealth;
            CurrentAttack = MaxAttack;
        }
        
        public override void Passive()
        {
            //Heal 5% health every turn - rounded up
            int heal = (int) (MaxHealth * .05);

            if (CurrentHealth + heal > MaxHealth)
                CurrentHealth = MaxHealth;
            else
            {
                CurrentHealth += heal;
            }

            CanUpdatePassive = false;
        }

        public override void UpdatePassive()
        {
            throw new System.NotImplementedException();
        }

        public override void Active(Enemy enemy) //heals 20% of current max health over 5 turns
        {
            var drone = new TurnBuff("Healing Drone", 5);
            drone.HasTurnEffect = true;
            
            void HealApply(Character character) //Snapshot of current Max Health
            {
                int healthPerTurn = MaxHealth / drone.Duration;
                if (CurrentHealth == MaxHealth)
                {
                    Console.WriteLine("Cannot go above max health!");
                }
                else if (CurrentHealth + healthPerTurn > MaxHealth)
                {
                    CurrentHealth = MaxHealth;
                    Console.WriteLine("You have hit max health!");
                }
                else
                {
                    CurrentHealth += healthPerTurn;
                }

            }

            void HealRemove(Character character)
            {
            }
            //var drone = new Buff("Healing Drone", 5);
            drone.TurnEffect = HealApply;
            drone.RemoveEffect = HealRemove;
            
            drone.Apply(this, 0);
        }

        public override void Ultimate(Enemy enemy) 
        {
            throw new System.NotImplementedException(); //Cannot be added to game until Gear has been added
        }
    }
}