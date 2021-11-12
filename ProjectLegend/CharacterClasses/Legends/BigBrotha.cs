using ProjectLegend.GameUtilities.BuffUtilities;

namespace ProjectLegend.CharacterClasses.Legends
{
    public sealed class BigBrotha : Player
    {
        private int PassiveHealthIncrease { get; set; }

        public BigBrotha()
        {
            Health.Max = 100;
            Attack.Max = 10;
            Health.Current = Health.Max;
            Attack.Current = Attack.Max;
            
            ActiveCost = 500;
            UltimateCost = 900;
            
            Passive();
            CanUpdatePassive = true;
        }
        public override void Passive()
        {
            PassiveHealthIncrease = (int) (Health.Max * .05);
            Health.Max += PassiveHealthIncrease;
        }

        public override void UpdatePassive()
        {
            int oldHealthIncrease = PassiveHealthIncrease;

            PassiveHealthIncrease = (int) ((Health.Max - oldHealthIncrease) * .05);
            Health.Max -= oldHealthIncrease;
            Health.Max += PassiveHealthIncrease;

            Health.Current = Health.Max;
        }

        public override void Active(Enemy enemy)
        {
            void ApplyBlock(Character character)
            {
                Invulnerable = true;
            }

            void RemoveBlock(Character character)
            {
                Invulnerable = false;
            }

            var block = new Buff("Defensive Shield", 1);
            block.ApplyEffect = ApplyBlock;
            block.RemoveEffect = RemoveBlock;
            
            block.Apply(this, ActiveCost);
        }

        public override void Ultimate(Enemy enemy)
        {
            if (Energy.Current >= UltimateCost)
            {
                enemy.Health.Current /= 2;
                System.Console.WriteLine("You have bombed the enemy!");
            }
            else
            {
                System.Console.WriteLine("Not enough energy!");
            }
        }
        
    }
}