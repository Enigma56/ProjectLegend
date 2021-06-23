using System;
using System.Collections;
using System.Collections.Generic;
using ProjectLegend.Items;
using ProjectLegend.Items.Consumables;


namespace ProjectLegend.CharacterClasses
{
    public abstract class Player : Character
    {
        public Item[] Inventory { get; set; }
        public Item Hand { get; set; }
        public bool HandIsEmpty { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }
        public int ExpThresh { get; set; }
        
        public int MaxEnergy { get; set; }
        public int CurrentEnergy { get; set; }
        public int EnergyPerTurn { get; set; }
        
        
        public int Vitality { get; set; }
        public int Strength { get; set; }
        
        public double UnbuffedEvasion { get; set; }
        public double UnbuffedEvasionCap { get; set; }
        public double TotalEvasion { get; set; }
        public double EvasionPerLevel { get; set; }
        
        public bool CanUpdatePassive { get; set; }
        

        protected Player()
        {
            Inventory = new Item[10];
            
            Buffs = new List<Buff>();
            
            MaxEnergy = 1000;
            CurrentEnergy = 0;
            EnergyPerTurn = 50;
            
            Accuracy = 1;
            Vitality = 0; 
            Strength = 0; 
            
            TotalEvasion = 0.0;
            UnbuffedEvasion = 0.0;
            UnbuffedEvasionCap = .01;
            EvasionPerLevel = .005;
            
            Level = 1;
            Exp = 0;
            ExpThresh = 10;
        }
        
       
        public abstract void Passive(); //No flag; always active
        public abstract void UpdatePassive();
        public abstract void Active(Enemy enemy); //Flag -a
        public abstract void Ultimate(Enemy enemy); //Flag: -u
        
        public override string ToString()
        {
            return "Stats: " + Environment.NewLine + 
                   $"Health = {CurrentHealth}" + Environment.NewLine + 
                   $"Attack = {CurrentAttack}" + Environment.NewLine +
                   $"Current Energy = {CurrentEnergy} && Max Energy = {MaxEnergy}" + Environment.NewLine +
                   $"Vitality = {Vitality}" + Environment.NewLine +
                   $"Strength = {Strength}" + Environment.NewLine +
                   $"Evasion = {TotalEvasion * 100:##.##}%";
                    
        }
    }
}