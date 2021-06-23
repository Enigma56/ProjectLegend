using System;
using System.Collections;
using System.Reflection.Metadata;
using ProjectLegend.CharacterClasses;


namespace ProjectLegend.Items.Consumables
{
    public abstract class Potions : Consumable
    {
        protected Potions()
        {
            DropRate = 1.0;
        }
        public abstract void Use(Player player);
    }
    
    public sealed class HealthPotion : Potions
    {
        public static Guid Id { get; set; }

        private double _healValue = .15;

        static HealthPotion()
        {
            Id = Guid.NewGuid();
        }
        public HealthPotion()
        {
            Name = "Health Potion";
        }

        public override void Use(Player player)
        {
            if (StackSize > 0)
            {
                int heal = (int) (player.MaxHealth * _healValue);

                if (player.CurrentHealth + heal > player.MaxHealth)
                {
                    player.CurrentHealth = player.MaxHealth;
                }
                else
                {
                    player.CurrentHealth += heal;
                }
            }
            else
            {
                Console.WriteLine("You do not have enough Health Potions!");
            }
        }
        
    }

    public sealed class EnergyPotion : Potions
    {
        public static Guid Id { get; set; }

        static EnergyPotion()
        {
            Id = Guid.NewGuid();
        }
        public EnergyPotion()
        {
            Name = "Energy Potion";
        }

        public override void Use(Player player)
        {
            if (StackSize > 0)
            {
                int energyReplenishment = 100;

                if (player.CurrentEnergy + energyReplenishment > player.MaxEnergy)
                {
                    player.CurrentEnergy = player.MaxEnergy;
                }
                else
                {
                    player.CurrentEnergy += energyReplenishment;
                }
            }
            else
            {
                Console.WriteLine("You do not have enough Energy Potions!");
            }
            
        }
    }
}