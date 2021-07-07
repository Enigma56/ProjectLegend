using System;

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
        private readonly double _healValue = .2;

        public HealthPotion()
        {
            Name = "Health Potion";
        }

        public override void Use(Player player)
        {
            int heal = (int) (player.MaxHealth * _healValue);

            if (player.CurrentHealth == player.MaxHealth)
                Console.WriteLine("You're already at max health!");
            else if (player.CurrentHealth + heal > player.MaxHealth)
            {
                Decrement();
                player.CurrentHealth = player.MaxHealth;
                Console.WriteLine("You are now at max health!");
            }
            else
            {
                Decrement();
                player.CurrentHealth += heal;
                Console.WriteLine($"You have been healed for {heal} health!");
            }
        }
        
    }

    public sealed class EnergyPotion : Potions
    {

        private readonly int _energyRefill = 100;
        
        public EnergyPotion()
        {
            Name = "Energy Potion";
        }

        public override void Use(Player player)
        {
            if (player.CurrentEnergy == player.MaxEnergy)
                Console.WriteLine("You're already at max energy!");
            if (player.CurrentEnergy + _energyRefill > player.MaxEnergy)
            {
                Decrement();
                player.CurrentEnergy = player.MaxEnergy;
                Console.WriteLine("Your energy is filled!");
            }
            else
            {
                Decrement();
                player.CurrentEnergy += _energyRefill;
                Console.WriteLine($"{_energyRefill} energy has been added!");
            }
        }
    }
}