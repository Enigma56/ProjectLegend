using System;

namespace ProjectLegend
{
    public class Enemy
    {
        public Enemy()
        {
            Random rand = new Random();
            Health = rand.Next(20, 50);
            Attack = rand.Next(5, 10);
        }

        public int Health { get; set; }

        public int Attack { get; set; }
        
        public override string ToString()
        {
            return $"Enemy Stats\nHealth = {Health}, Attack = {Attack}";
        }
    }
    

}