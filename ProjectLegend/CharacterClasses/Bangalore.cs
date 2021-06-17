

namespace ProjectLegend.CharacterClasses
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
            CanUpdatePassive = false;
        }
        
        public override void UpdatePassive()
        {
            throw new System.NotImplementedException();
        }

        //Active ability - activated by player
        public override void Active(Enemy enemy)
        {
            void ReduceAccuracy(Character character)
            {
                enemy.Accuracy -= .33;
            }

            void RecoverAccuracy(Character character)
            {
                enemy.Accuracy += .33;
            }

            var smoked = new Debuff("Smoke Bomb", 2);
            smoked.ApplyEffect = ReduceAccuracy;
            smoked.RemoveEffect = RecoverAccuracy;
            
            smoked.Apply(enemy, 250);

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
            var stunned = new Debuff("Rolling Thunder", 1);
            stunned.ApplyEffect = ApplyStun;
            stunned.RemoveEffect = RemoveStun;

            stunned.Apply(enemy);
        }
    }
}