﻿using System;
using System.Collections.Generic;

using ProjectLegend.Items;
using ProjectLegend.Items.Consumables;

namespace ProjectLegend.CharacterClasses
{
    public class Enemy : Character
    {
        private Random itemGenerator = new Random();
        Dictionary<int, Item> drops = new()
        {
            {1, new HealthPotion()},
            {2, new EnergyPotion()}
        };  
        public int ExpDrop { get; set; }

        public Enemy()
        {
            var rand = new Random();
            MaxHealth = rand.Next(20, 50);
            MaxAttack = rand.Next(5, 10);
            
            CurrentHealth = MaxHealth;
            CurrentAttack = MaxAttack;
            
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
            return $"Enemy Stats" + Environment.NewLine + $"Health = {MaxHealth}, Attack = {CurrentAttack}";
        }
    }
    

}