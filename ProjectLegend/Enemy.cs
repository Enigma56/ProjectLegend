using System;

namespace ProjectLegend
{
    public class Enemy
    {
        public Enemy()
        {
            var rand = new Random();
            Health = rand.Next(20, 50);
            Attack = rand.Next(5, 10);
            Accuracy = 1;
            
            ExpDrop = 10;
            Dead = false;
        }

        public int Health { get; set; }

        public int Attack { get; }
        
        public int Accuracy { get; set; }
        
        public int ExpDrop { get; set; }

        public bool Dead { get; set; }

        public void IncreaseExpDrop() //NOT IN USE
        {
            ExpDrop *= 3 / 2;
        }
        
        
        
        public override string ToString()
        {
            return $"Enemy Stats" + Environment.NewLine + $"Health = {Health}, Attack = {Attack}";
        }
    }
    

}