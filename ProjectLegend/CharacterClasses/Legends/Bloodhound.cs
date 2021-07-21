using ProjectLegend.GameUtilities.BuffUtilities;

namespace ProjectLegend.CharacterClasses.Legends
{
    public sealed class Bloodhound : Player
    {
        private int DamageReduction { get; set; }
        public Bloodhound()
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
            if (Accuracy < .8)
                Accuracy = .8;
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
                character.Attack.AtkMultiplier = .9;
            }

            void RemoveAttackReduction(Character character)
            {
                character.Attack.AtkMultiplier = 1.0;
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
                character.Attack.AtkMultiplier = 1.5;
            }

            void RemoveWeakness(Character character)
            {
                character.Attack.AtkMultiplier = 1.0;
            }

            var strength = new Buff("All-Father's Omnipotence", 2);
            strength.ApplyEffect = Weakness;
            strength.RemoveEffect = RemoveWeakness;
            strength.Apply(this, 600);
        }
    }
}