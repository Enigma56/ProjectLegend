using System;

namespace ProjectLegend.PlayerClasses
{
    public abstract class Player
    {
        
        //Character Stats
        public int Health { get; set; }
        public int Attack { get; init; }

        public int Exp { get; set; }

        public int ExpThresh { get; set; }

        public int Level { get; set; }

        public void AddLevel()
        {
            int xp = Exp;
            Exp = 0;
            Exp += (xp % ExpThresh);
            ExpThresh = ExpThresh * 3 / 2;
            Level++;
            Console.WriteLine($"You Leveled Up!\nCurrent level: {Level}");
            Console.WriteLine($"XP Threshold: {ExpThresh}");
        }

        public void DisplayXpInfo()
        {
            Console.WriteLine($"XP remaining: {Exp}/{ExpThresh}");
        }
        
        public override string ToString()
        {
            return $"Stats:\nHealth = {Health}, Attack = {Attack}";
        }
    }
}