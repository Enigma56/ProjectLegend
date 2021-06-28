namespace ProjectLegend.CharacterClasses.Legends
{
    public class Bloodhound : Player
    {
        private int DamageReduction { get; set; }
        public Bloodhound()
        {
            MaxHealth = 100;
            MaxAttack = 10;
            
            Passive();
            
            CurrentHealth = MaxHealth;
            CurrentAttack = MaxAttack;
        }
        
        public override void Passive()
        {
            if (Accuracy < .8)
                Accuracy = .8;
            CanUpdatePassive = true;
        }

        public override void UpdatePassive()
        {
            if (Accuracy < .8)
                Accuracy = .8;
        }

        public override void Active(Enemy enemy)
        {
            void ReduceEnemyAttack(Character character)
            {
                DamageReduction = character.CurrentAttack - (int) (character.CurrentAttack * .9);
                character.CurrentAttack -= DamageReduction;
            }

            void RemoveAttackReduction(Character character)
            {
                character.CurrentAttack += DamageReduction;
            }

            var damageReduction = new Debuff("All-Father's Assistance", 2);
            damageReduction.ApplyEffect = ReduceEnemyAttack;
            damageReduction.RemoveEffect = RemoveAttackReduction;
            
            damageReduction.Apply(enemy, 400);
        }

        public override void Ultimate(Enemy enemy)
        {
            void Weakness(Character character)
            {
                //Damage multiplier should be implemented; multiplier => 1.2
            }

            void RemoveWeakness(Character character)
            {
                //Revert multiplier to 1.0
            }

            var weakness = new Buff("All-Father's Omnipotence", 2);
            weakness.ApplyEffect = Weakness;
            weakness.RemoveEffect = RemoveWeakness;
        }
    }
}