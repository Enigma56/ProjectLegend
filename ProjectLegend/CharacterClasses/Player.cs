using System;
using System.Collections.Generic;

using ProjectLegend.ItemClasses;
using ProjectLegend.GameUtilities.BuffUtilities;
using ProjectLegend.ItemClasses.GearClasses;


namespace ProjectLegend.CharacterClasses
{
    public abstract class Player : Character
    {
        public CharacterStats PlayerStats { get; set; }
        public Gear[] GearInventory { get; }
        public Item[] Inventory { get; }
        public Item Hand { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }
        public int ExpThresh { get; set; }
        
        public int MaxEnergy { get; }
        public int CurrentEnergy { get; set; }
        public int EnergyPerTurn { get; }
        
        
        public int BaseVitality { get; set; }
        public int MaxVitality { get; set; }
        public int BaseStrength { get; set; }
        public int MaxStrength { get; set; }

        public double UnbuffedEvasion { get; set; }
        public double UnbuffedEvasionCap { get; }
        public double TotalEvasion { get; set; }
        public double EvasionPerLevel { get; set; }
        
        public bool CanUpdatePassive { get; init; }
        
        public int DeathCount { get; set; }
        

        protected Player()
        {
            GearInventory = new Gear[4];
            Inventory = new Item[10];

            PlayerStats = new CharacterStats();
            Buffs = new List<Buff>();

            MaxEnergy = 1000;
            CurrentEnergy = 0;
            EnergyPerTurn = 50;

            Accuracy = 1;
            BaseStrength = 0;
            BaseVitality = 0;
            MaxStrength = 500;
            MaxVitality = 500;

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

        //TODO: factor in other stats when calculating attack and HP; add function that updates stat values dynamically?
        
        public void AddLevel()
        {
            LevelUpdate();
            StatLevelUpdate();
        }
        private void LevelUpdate()
        { 
            int xp = Exp;
            int oldLevel = Level;
                
            Exp = 0;
            Exp += (xp % ExpThresh);
            Level++;
            int oldExpThresh =ExpThresh;
            int thresholdIncrease = (int) Math.Ceiling(Math.Pow(Level, 2) * Math.Log10(Level));
            ExpThresh += thresholdIncrease;

            Console.WriteLine("You Leveled Up!" + Environment.NewLine + 
                              $"Current level: {oldLevel} --> {Level}" + Environment.NewLine + 
                              $"XP Required {oldExpThresh} --> {ExpThresh}");
        }
        
        private void StatLevelUpdate()
        {
            int oldMaxHealthVal = MaxHealth;
            int healthIncrease = (int) Math.Ceiling((Math.Pow(Level, 2) + BaseVitality) / 5);
            MaxHealth += healthIncrease;
            CurrentHealth = MaxHealth; //Fully heal on every level up
                
            int oldAttackVal = MaxAttack;
            int attackIncrease = (int) Math.Ceiling((Math.Pow(Level, 2) + BaseStrength) / 20);
            MaxAttack += attackIncrease;
            CurrentAttack = MaxAttack;
                
            double oldEvasionVal = TotalEvasion;
            if (UnbuffedEvasion < UnbuffedEvasionCap)
            {
                UnbuffedEvasion += EvasionPerLevel;
                TotalEvasion += EvasionPerLevel;
            }

            if (CanUpdatePassive)
            {
                UpdatePassive();
            }
                

            Console.WriteLine(Environment.NewLine + $"Max Health Up! {oldMaxHealthVal} --> {CurrentHealth}"
                                                  + Environment.NewLine + $"Attack Up! {oldAttackVal} --> {MaxAttack}");
                
            Console.WriteLine(UnbuffedEvasion < UnbuffedEvasionCap 
                ? $"Evasion Up! {oldEvasionVal * 100:#0.0#}% --> {TotalEvasion * 100:##.##}%" + Environment.NewLine 
                : $"Max Evasion from levels hit! {oldEvasionVal * 100:##.##}% --> {TotalEvasion * 100:##.##}%"); // 0 represents always-appearing digit; # is optional
        }

        public void UpdatePlayerStats() //TODO: ensure that stats are updated when the player equips/removes gear
        {
            void StrengthUpdate()
            {
                
            }
            //update strength
            //update vitality
        }
        
        public override string ToString()
        {
            return "Stats: " + Environment.NewLine + 
                   $"Health = {CurrentHealth}" + Environment.NewLine + 
                   $"Attack = {CurrentAttack}" + Environment.NewLine +
                   $"Current Energy = {CurrentEnergy} && Max Energy = {MaxEnergy}" + Environment.NewLine +
                   $"Vitality = {BaseVitality}" + Environment.NewLine +
                   $"Strength = {BaseStrength}" + Environment.NewLine +
                   $"Evasion = {TotalEvasion * 100:#0.0#}%";
                    
        }
    }
}