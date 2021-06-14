

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
            
            Passive();

        }
        
        public override void Passive()
        {
            TotalEvasion += .1;
        }

        //Active ability - activated by player
        public override void Active(Enemy enemy)
        {
            
        }

        //Ultimate - activated by player
        public override void Ultimate(Enemy enemy)
        {
            void ApplyStun(Character character)
            {
                enemy.Stunned = true;
            }

            void RemoveStun(Character character)
            {
                enemy.Stunned = false;
            }
            Debuff stunned = new Debuff("Rolling Thunder", 1);
            stunned.ApplyEffect = ApplyStun;
            stunned.RemoveEffect = RemoveStun;

            stunned.Apply(enemy);
        }
    }
}