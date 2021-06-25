namespace ProjectLegend.CharacterClasses.Legends
{
    public sealed class Gibraltar : Player
    {
        //private int shield = 5;
        private int PassiveHealthIncrease { get; set; }

        public Gibraltar()
        {
            MaxHealth = 100;
            MaxAttack = 10;
            
            Passive();
            
            CurrentHealth = MaxHealth;
            CurrentAttack = MaxAttack;
        }
        public override void Passive()
        {
            PassiveHealthIncrease = (int) (MaxHealth * .05);
            MaxHealth += PassiveHealthIncrease;

            CanUpdatePassive = true;
        }

        public override void UpdatePassive()
        {
            int oldHealthIncrease = PassiveHealthIncrease;

            PassiveHealthIncrease = (int) ((MaxHealth - oldHealthIncrease) * .05);
            MaxHealth -= oldHealthIncrease;
            MaxHealth += PassiveHealthIncrease;

            CurrentHealth = MaxHealth;
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