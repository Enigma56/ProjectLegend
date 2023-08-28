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
            int heal = (int) (player.Health.Max * _healValue);

            if (player.Health.Current == player.Health.Max)
                Console.WriteLine("You're already at max health!");
            else if (player.Health.Current + heal > player.Health.Max)
            {
                Decrement();
                player.Health.Current = player.Health.Max;
                Console.WriteLine("Your heal put you to max health!");
            }
            else
            {
                Decrement();
                player.Health.Current += heal;
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
            if (player.Energy.Current == player.Energy.Max)
                Console.WriteLine("You're already at max energy!");
            if (player.Energy.Current + _energyRefill > player.Energy.Max)
            {
                Decrement();
                player.Energy.Current = player.Energy.Max;
                Console.WriteLine("Your energy is filled!");
            }
            else
            {
                Decrement();
                player.Energy.Current += _energyRefill;
                Console.WriteLine($"{_energyRefill} energy has been added!");
            }
        }
    }
}