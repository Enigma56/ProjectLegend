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
        
        // Passive ability
        
        //Active ability
        
        //Ultimate

        public void AddLevel()
        {
            if (Exp >= ExpThresh)
            {
                int xp = Exp;
                Exp = 0;
                Exp += (xp % ExpThresh);
                ExpThresh *=  3 / 2;
                Level++;
            }
        }
        
        public override string ToString()
        {
            return $"Stats:\nHealth = {Health}, Attack = {Attack}";
        }
    }
}