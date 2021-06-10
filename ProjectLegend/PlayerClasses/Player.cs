using System;
using System.Collections;

namespace ProjectLegend.PlayerClasses
{
    public abstract class Player
    {
        public int Level { get; set; }
        public int Exp { get; set; }
        public int ExpThresh { get; set; }
        
        public ArrayList Buffs { get; }
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
            Buffs = new ArrayList();
            
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
            
            Exp = 0;
            ExpThresh = 10;
            Level = 1;
        }
        
       
        public abstract void Passive(); //No flag; always active

        public abstract void Active(); //Flag -a

        public abstract void Ultimate(); //Flag: -u
        
        // public void AddLevel()
        // {
        //     void LevelUpdate()
        //     {
        //         int xp = Exp;
        //         int oldLevel = Level;
        //         
        //         Exp = 0;
        //         Exp += (xp % ExpThresh);
        //         Level++;
        //         int oldExpThresh = ExpThresh;
        //         int thresholdIncrease = (int) Math.Ceiling(Math.Pow(Level, 2) * Math.Log10(Level));
        //         ExpThresh += thresholdIncrease;
        //
        //         Console.WriteLine("You Leveled Up!" + Environment.NewLine + 
        //                           $"Current level: {oldLevel} --> {Level}" + Environment.NewLine + 
        //                           $"XP Required {oldExpThresh} --> {ExpThresh}");
        //     }
        //     void StatUpdate()
        //     {
        //         int oldHealthVal = CurrentHealth;
        //         int healthIncrease = (int) Math.Ceiling(Math.Pow(Level, 2) / 5);
        //         MaxHealth += healthIncrease;
        //         CurrentHealth = MaxHealth; //Fully heal on every level up
        //         
        //         int oldAttackVal = CurrentAttack;
        //         int attackIncrease = (int) Math.Ceiling(Math.Pow(Level, 2) / 20);
        //         MaxAttack += attackIncrease;
        //         CurrentAttack = MaxAttack;
        //         
        //         double oldEvasionVal = Evasion;
        //         if(Evasion < EvasionCap) Evasion += .005;
        //
        //         Console.WriteLine(Environment.NewLine + $"Health Up! {oldHealthVal} --> {CurrentHealth}"
        //                                               + Environment.NewLine + $"Attack Up! {oldAttackVal} --> {CurrentAttack}");
        //         
        //         Console.WriteLine(//$"Strength Up! {oldStrengthVal} --> {Strength}"  + Environment.NewLine +
        //                           //$"Vitality Up! {oldVitalityVal} --> {Vitality}"   + Environment.NewLine + 
        //                           $"Evasion Up! {oldEvasionVal * 100:##.##}% --> {Evasion * 100:##.##}%" + Environment.NewLine); // 0 represents always-appearing digit; # is optional
        //     }
        //     
        //     LevelUpdate();
        //     StatUpdate();
        // }
        
        public delegate void Ability();
        protected void ApplyBuff(Buff buff, Ability abilityBuff)
        {
                    buff.TurnsRemaining = buff.Duration;
                    Buffs.Add(buff);
                    abilityBuff(); 
        }

        protected void RemoveBuff(Buff buff)
        {
            Buffs.Remove(buff);
        }
        
        
        public void DisplayXpInfo()
        {
            Console.WriteLine($"XP remaining: {Exp}/{ExpThresh}");
        }
        
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