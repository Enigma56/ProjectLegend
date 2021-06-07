

namespace ProjectLegend.PlayerClasses
{
    public class Bangalore : Player
    {
        public Bangalore()
        {
            MaxHealthVal = 70;
            MaxAttackValue = 15;
            CurrentHealthVal = MaxHealthVal;
            CurrentAttackVal = MaxAttackValue;
            
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