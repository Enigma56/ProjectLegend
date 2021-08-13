using ProjectLegend.GameUtilities.BuffUtilities;

namespace ProjectLegend.CharacterClasses.Legends
{
    public sealed class Bangalore : Player
    {
        public Bangalore()
        {
            Health.Max = 70;
            Attack.Max = 15;
            Health.Current = Health.Max;
            Attack.Current = Attack.Max;

            ActiveCost = 250;
            UltimateCost = 600;
            
            Passive();
            CanUpdatePassive = false;
        }
        
        public override void Passive()
        {
            Evasion.Total += .05;
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
            
            smoked.Apply(enemy, ActiveCost);

        }

        //Ultimate - activated by player
        public override void Ultimate(Enemy enemy)
        {
            void ApplyStun(Character character)
            {
                character.Stunned = true;
            }

            void RemoveStun(Character character)
            {
                character.Stunned = false;
            }
            var stunned = new Debuff("Rolling Thunder", 2);
            stunned.ApplyEffect = ApplyStun;
            stunned.RemoveEffect = RemoveStun;

            stunned.Apply(enemy, UltimateCost);
        }
    }
}