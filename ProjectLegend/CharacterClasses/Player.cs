using System;
using System.Collections.Generic;

using ProjectLegend.Items;
using ProjectLegend.GameUtilities.BuffUtilities;


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
        
        public int DeathCount { get; set; }
        

        protected Player()
        {
            Inventory = new Item[5];
            
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
        
        //TODO: factor in other stats when calculating attack and HP; add function that updates stat values dynamically?
        public void AddLevel()
        {
            void LevelUpdate()
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
            void StatUpdate()
            {
                int oldMaxHealthVal = MaxHealth;
                int healthIncrease = (int) Math.Ceiling(Math.Pow(Level, 2) / 5);
                MaxHealth += healthIncrease;
                CurrentHealth = MaxHealth; //Fully heal on every level up
                
                int oldAttackVal = MaxAttack;
                int attackIncrease = (int) Math.Ceiling(Math.Pow(Level, 2) / 20);
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
            
            LevelUpdate();
            StatUpdate();
        }
        
        public override string ToString()
        {
            return "Stats: " + Environment.NewLine + 
                   $"Health = {CurrentHealth}" + Environment.NewLine + 
                   $"Attack = {CurrentAttack}" + Environment.NewLine +
                   $"Current Energy = {CurrentEnergy} && Max Energy = {MaxEnergy}" + Environment.NewLine +
                   $"Vitality = {Vitality}" + Environment.NewLine +
                   $"Strength = {Strength}" + Environment.NewLine +
                   $"Evasion = {TotalEvasion * 100:#0.0#}%";
                    
        }
    }
}