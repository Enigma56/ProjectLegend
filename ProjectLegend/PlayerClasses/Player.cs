using System;

namespace ProjectLegend.PlayerClasses
{
    public abstract class Player
    {
        protected Player()
        {
            MaxEnergy = 1000;
            CurrentEnergy = 0;
            EnergyPerTurn = 50;
            
            Accuracy = 1;
            Vitality = 0; 
            Strength = 0; 
            
            Evasion = 0.0;
            EvasionCap = .1;
            
            Exp = 0;
            ExpThresh = 10;
            Level = 1;
        }
        
        public int MaxEnergy { get; set; }
        
        public int CurrentEnergy { get; set; }
        
        public int EnergyPerTurn { get; set; }
        //Character Stats
        protected int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }

        protected int MaxAttack { get; set; }
        public int CurrentAttack { get; protected set; }
        
        public double Accuracy { get; set; }
        public int Vitality { get; set; }
        
        public int Strength { get; set; }
        public double Evasion { get; set; }
        protected double EvasionCap { get; init; }
        
        public int Exp { get; set; }

        public int ExpThresh { get; set; }

        public int Level { get; set; }

        public abstract void Passive(); //No flag; always active

        public abstract void Active(); //Flag -a

        public abstract void Ultimate(); //Flag: -u
        public void AddLevel()
        {
            void LevelUpdate()
            {
                int xp = Exp;
                int oldLevel = Level;
                
                Exp = 0;
                Exp += (xp % ExpThresh);
                Level++;
                int oldExpThresh = ExpThresh;
                int thresholdIncrease = (int) Math.Ceiling(Math.Pow(Level, 2) * Math.Log10(Level));
                ExpThresh += thresholdIncrease;

                Console.WriteLine("You Leveled Up!" + Environment.NewLine + 
                                  $"Current level: {oldLevel} --> {Level}" + Environment.NewLine + 
                                  $"XP Required {oldExpThresh} --> {ExpThresh}");
            }
            void StatUpdate()
            {
                /*
                int oldStrengthVal = Strength;
                Strength += 1;

                int oldVitalityVal = Vitality;
                Vitality += 1;
                */

                int oldHealthVal = CurrentHealth;
                int healthIncrease = (int) Math.Ceiling(Math.Pow(Level, 2) / 5);
                MaxHealth += healthIncrease;
                CurrentHealth = MaxHealth; //Fully heal on every level up
                
                int oldAttackVal = CurrentAttack;
                int attackIncrease = (int) Math.Ceiling(Math.Pow(Level, 2) / 20);
                MaxAttack += attackIncrease;
                CurrentAttack = MaxAttack;
                
                double oldEvasionVal = Evasion;
                if(Evasion < EvasionCap) Evasion += .005;

                Console.WriteLine(Environment.NewLine + $"Health Up! {oldHealthVal} --> {CurrentHealth}"
                                                      + Environment.NewLine + $"Attack Up! {oldAttackVal} --> {CurrentAttack}");
                
                Console.WriteLine(//$"Strength Up! {oldStrengthVal} --> {Strength}"  + Environment.NewLine +
                                  //$"Vitality Up! {oldVitalityVal} --> {Vitality}"   + Environment.NewLine + 
                                  $"Evasion Up! {oldEvasionVal * 100:##.##}% --> {Evasion * 100:##.##}%" + Environment.NewLine); // 0 represents always-appearing digit; # is optional
            }
            
            LevelUpdate();
            StatUpdate();
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
                   $"Evasion = {Evasion * 100:##.##}%";
                    
        }
    }
}