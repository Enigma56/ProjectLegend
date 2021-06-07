using System;

namespace ProjectLegend.PlayerClasses
{
    public abstract class Player
    {
        protected Player()
        {
            Accuracy = 1;
            Vitality = 0; 
            Strength = 0; 
            
            Evasion = 0.0;
            EvasionCap = .1;
            
            Exp = 0;
            ExpThresh = 10;
            Level = 1;
        }
        //Character Stats
        public int MaxHealthVal { get; set; }
        public int CurrentHealthVal { get; set; }

        public int MaxAttackValue { get; set; }
        public int CurrentAttackVal { get; set; }
        
        public double Accuracy { get; set; }
        public int Vitality { get; set; }
        
        public int Strength { get; set; }
        public double Evasion { get; set; }
        public double EvasionCap { get; init; }
        
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

                int oldHealthVal = CurrentHealthVal;
                int healthIncrease = (int) Math.Ceiling(Math.Pow(Level, 2) / 5);
                MaxHealthVal += healthIncrease;
                CurrentHealthVal = MaxHealthVal; //Fully heal on every level up
                
                int oldAttackVal = CurrentAttackVal;
                int attackIncrease = (int) Math.Ceiling(Math.Pow(Level, 2) / 20);
                
                CurrentAttackVal = CurrentAttackVal + attackIncrease;
                
                double oldEvasionVal = Evasion;
                if(Evasion < EvasionCap) Evasion += .005;

                Console.WriteLine(Environment.NewLine + $"Health Up! {oldHealthVal} --> {CurrentHealthVal}"
                                                      + Environment.NewLine + $"Attack Up! {oldAttackVal} --> {CurrentAttackVal}");
                
                Console.WriteLine(//$"Strength Up! {oldStrengthVal} --> {Strength}"  + Environment.NewLine +
                                  //$"Vitality Up! {oldVitalityVal} --> {Vitality}"   + Environment.NewLine + 
                                  $"Evasion Up! {oldEvasionVal * 100}% --> {Evasion * 100}%" + Environment.NewLine);
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
                   $"Health = {CurrentHealthVal}" + Environment.NewLine + 
                   $"Attack = {CurrentAttackVal}" + Environment.NewLine +
                   $"Vitality = {Vitality}" + Environment.NewLine +
                   $"Strength = {Strength}" + Environment.NewLine +
                   $"Evasion = {Evasion * 100}%";
                    
        }
    }
}