namespace ProjectLegend.CharacterClasses
{
    public sealed class Gibraltar : Player
    {
        //private int shield = 5;
        //private int shieldDifference { get; set; }

        public Gibraltar()
        {
            MaxHealth = 100;
            MaxAttack = 10;
            
            CurrentHealth = MaxHealth;
            CurrentAttack = MaxAttack;
            
            CurrentEnergy = 0;
            
            Passive();
        }
        public override void Passive()
        {
            int additionalHealth = (int) (MaxHealth * .05);
            MaxHealth += additionalHealth;

            CanUpdatePassive = false;
        }

        public override void UpdatePassive()
        {
            throw new System.NotImplementedException();
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

            var block = new Buff("Defensive Shield", 2);
            block.ApplyEffect = ApplyBlock;
            block.RemoveEffect = RemoveBlock;
            
            block.Apply(this, 600);
        }

        public override void Ultimate(Enemy enemy)
        {
            enemy.CurrentHealth /= 2;
        }
    }
}