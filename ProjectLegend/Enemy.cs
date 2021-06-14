using System;

namespace ProjectLegend
{
    public class Enemy : Character
    {
        public int ExpDrop { get; set; }
        
        //States
        public bool Dead { get; set; }
        public bool Stunned { get; set; }
        public Enemy()
        {
            var rand = new Random();
            MaxHealth = rand.Next(20, 50);
            MaxAttack = rand.Next(5, 10);
            
            CurrentHealth = MaxHealth;
            CurrentAttack = MaxAttack;
            
            Accuracy = 1;
            
            ExpDrop = 10;
            
            Dead = false;
            Stunned = false;
        }

        public void IncreaseExpDrop() //NOT IN USE
        {
            ExpDrop *= 3 / 2;
        }
        
        public override string ToString()
        {
            return $"Enemy Stats" + Environment.NewLine + $"Health = {MaxHealth}, Attack = {CurrentAttack}";
        }
    }
    

}