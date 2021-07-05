using ProjectLegend.GameUtilities.BuffUtilities;

namespace ProjectLegend.CharacterClasses.Legends
{
    public sealed class Bloodhound : Player
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

        public override void Active(Enemy enemy) //CHECK
        {
            void ReduceEnemyAttack(Character character)
            {
                character.AttackMultiplier = .9;
            }

            void RemoveAttackReduction(Character character)
            {
                character.AttackMultiplier = 1.0;
            }

            var damageReduction = new Debuff("All-Father's Assistance", 2);
            damageReduction.ApplyEffect = ReduceEnemyAttack;
            damageReduction.RemoveEffect = RemoveAttackReduction;
            
            damageReduction.Apply(enemy, 400);
        }

        public override void Ultimate(Enemy enemy) //CHECK
        {
            void Weakness(Character character)
            {
                character.AttackMultiplier = 1.5;
            }

            void RemoveWeakness(Character character)
            {
                character.AttackMultiplier = 1.0;
            }

            var strength = new Buff("All-Father's Omnipotence", 2);
            strength.ApplyEffect = Weakness;
            strength.RemoveEffect = RemoveWeakness;
            strength.Apply(this, 600);
        }
    }
}