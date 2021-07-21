using ProjectLegend.GameUtilities.BuffUtilities;

namespace ProjectLegend.CharacterClasses.Legends
{
    public sealed class Gibraltar : Player
    {
        //private int shield = 5;
        private int PassiveHealthIncrease { get; set; }

        public Gibraltar()
        {
            Health.Max = 100;
            Attack.Max = 10;
            
            Passive();
            CanUpdatePassive = true;
            
            Health.Current = Health.Max;
            Attack.Current = Attack.Max;
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
            
            block.Apply(this, 500);
        }

        public override void Ultimate(Enemy enemy)
        {
            int energyCost = 900;
            if (Energy.Current >= energyCost)
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