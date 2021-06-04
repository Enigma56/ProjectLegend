

namespace ProjectLegend.PlayerClasses
{
    public class Bangalore : Player
    {
        public Bangalore()
        {
            Health = 70;
            Attack = 15;
            Exp = 0;
            ExpThresh = 10;
            Level = 1;
        }
        
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