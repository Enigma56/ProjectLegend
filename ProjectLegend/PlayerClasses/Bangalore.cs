

namespace ProjectLegend.PlayerClasses
{
    public sealed class Bangalore : Player
    {
        public Bangalore()
        {
            MaxHealth = 70;
            MaxAttack = 15;
            
            CurrentHealth = MaxHealth;
            CurrentAttack = MaxAttack;

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