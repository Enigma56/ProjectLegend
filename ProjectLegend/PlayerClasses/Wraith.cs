using System.Linq.Expressions;

namespace ProjectLegend.PlayerClasses
{
    public class Wraith : Player
    {
        public Wraith()
        {
            Health = 50;
            Attack = 20;
            
            Accuracy = 1;
            Vitality = 0;
            Strength = 0;
            Evasion = 0.0;

            Exp = 0;
            ExpThresh = 10;
            Level = 1;
        }
        
        // Passive ability - always active
        public override void Passive() //Increased evasion and attack
        {
            throw new System.NotImplementedException();
        }
        
        
        //Active ability - activated by player
        public override void Active() //Raise evasion from X to 50%
        {
            throw new System.NotImplementedException();
        }
        
        
        //Ultimate - activated by player
        public override void Ultimate() //Go invulnerable for one attack stage and raise attack by 25%
        {
            throw new System.NotImplementedException();
        }



    }
}