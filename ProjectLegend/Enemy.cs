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
            ExpDrop = 10;
        }

        public int Health { get; set; }

        public int Attack { get; set; }
        
        public int ExpDrop { get; set; }

        public void IncreaseExpDrop()
        {
            ExpDrop *= 3 / 2;
        }
        
        public override string ToString()
        {
            return $"Enemy Stats\nHealth = {Health}, Attack = {Attack}";
        }
    }
    

}