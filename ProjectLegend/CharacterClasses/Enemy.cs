using System;
using System.Collections.Generic;
using ProjectLegend.ItemClasses;
using ProjectLegend.ItemClasses.Consumables;

namespace ProjectLegend.CharacterClasses
{
    public class Enemy : Character
    {
        private Random itemGenerator = new();
        Dictionary<int, Item> drops = new()
        {
            {1, new HealthPotion()},
            {2, new EnergyPotion()}
        };

        protected readonly Random StatGenerator = new();
        public int ExpDrop { get; set; }

        public Enemy()
        {
            Health.Max = StatGenerator.Next(10, 20); 
            Attack.Max = StatGenerator.Next(5, 10);
            
            Health.Current = Health.Max;
            Attack.Current = Attack.Max;
            
            Accuracy = 1;
            
            ExpDrop = 10;
            
            Dead = false;
            Stunned = false;
        }

        public void IncreaseExpDrop() //NOT IN USE
        {
            ExpDrop *= 3 / 2;
        }

        public Item GetDrop()
        {
            int itemNum = itemGenerator.Next(1, drops.Count + 1);
            return drops[itemNum];
        }
        
        public override string ToString()
        {
            return "Enemy Stats" + Environment.NewLine + $"Health = {Health.Max}, Attack = {Attack.Current}";
        }
    }
    

}