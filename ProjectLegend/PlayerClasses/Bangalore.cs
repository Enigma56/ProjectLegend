

namespace ProjectLegend.PlayerClasses
{
    public class Bangalore : Player
    {
        public Bangalore()
        {
            Health = 70;
            Attack = 15;
            
            Accuracy = 1;
            Vitality = 0;
            Strength = 0;
            Evasion = 0.0;

            Exp = 0;
            ExpThresh = 10;
            Level = 1;
        }
        
        public override void Passive()
        {
            throw new System.NotImplementedException();
        }
        
        //Active ability - activated by player
        public override void Active()
        {
            throw new System.NotImplementedException();
        }
        
        //Ultimate - activated by player
        public override void Ultimate()
        {
            throw new System.NotImplementedException();
        }

        
    }
}