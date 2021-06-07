using System.Linq.Expressions;

namespace ProjectLegend.PlayerClasses
{
    public class Wraith : Player
    {
        public Wraith()
        {
            MaxHealthVal = 50;
            MaxAttackValue = 20;
            
            CurrentHealthVal = MaxHealthVal;
            CurrentAttackVal = MaxAttackValue;
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