using System;
using System.Collections;
using System.Collections.Generic;

namespace ProjectLegend.PlayerClasses
{
    public abstract class Player
    {
        public int Level { get; set; }
        public int Exp { get; set; }
        public int ExpThresh { get; set; }
        
        public List<Buff> Buffs { get; init; }
        public int MaxEnergy { get; set; }
        public int CurrentEnergy { get; set; }
        public int EnergyPerTurn { get; set; }
        
        //Character Stats
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }

        public int MaxAttack { get; set; }
        public int CurrentAttack { get; set; }
        
        public double Accuracy { get; set; }
        public int Vitality { get; set; }
        public int Strength { get; set; }
        
        public double UnbuffedEvasion { get; set; }
        public double UnbuffedEvasionCap { get; set; }
        public double TotalEvasion { get; set; }
        
        public double EvasionPerLevel { get; set; }
        

        protected Player()
        {
            Buffs = new List<Buff>();
            
            MaxEnergy = 1000;
            CurrentEnergy = 300;
            EnergyPerTurn = 50;
            
            Accuracy = 1;
            Vitality = 0; 
            Strength = 0; 
            
            TotalEvasion = 0.0;
            UnbuffedEvasion = 0.0;
            UnbuffedEvasionCap = .01;
            EvasionPerLevel = .005;
            
            Exp = 0;
            ExpThresh = 10;
            Level = 1;
        }
        
       
        public abstract void Passive(); //No flag; always active
        public abstract void Active(); //Flag -a
        public abstract void Ultimate(); //Flag: -u
        

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