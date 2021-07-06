using System;
using System.Collections;
using System.Reflection.Metadata;
using ProjectLegend.CharacterClasses;


namespace ProjectLegend.ItemClasses.Consumables
{
    public abstract class Potions : Consumable
    {
        protected Potions()
        {
            DropRate = 1.0;
        }
    }
    
    public sealed class HealthPotion : Potions
    {
        public static Guid Id { get; }

        private readonly double _healValue = .15;

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
            // if (StackSize > 0)
            // {
                int heal = (int) (player.MaxHealth * _healValue);

                if (player.CurrentHealth + heal > player.MaxHealth)
                {
                    player.CurrentHealth = player.MaxHealth;
                    Console.WriteLine("You are now at max health!");
                }
                else
                {
                    player.CurrentHealth += heal;
                    Console.WriteLine($"You have been healed for {heal} health!");
                }
            // }
            // else
            // {
            //     Console.WriteLine("You do not have enough Health Potions!");
            // }
        }
        
    }

    public sealed class EnergyPotion : Potions
    {
        public static Guid Id { get; }

        private readonly int _energyRefill = 100;

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
            // if (StackSize > 0)
            // {
                if (player.CurrentEnergy + _energyRefill > player.MaxEnergy)
                {
                    player.CurrentEnergy = player.MaxEnergy;
                    Console.WriteLine("Your energy is nor completely filled!");
                }
                else
                {
                    player.CurrentEnergy += _energyRefill;
                    Console.WriteLine($"{_energyRefill} energy has been added to your current energy!");
                }
            // }
            // else
            // {
            //     Console.WriteLine("You do not have enough Energy Potions!");
            // }
        }
    }
}