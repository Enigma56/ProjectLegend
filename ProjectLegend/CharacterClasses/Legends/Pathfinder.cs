namespace ProjectLegend.CharacterClasses.Legends
{
    public class Pathfinder : Player
    {
        public Pathfinder()
        {
            MaxHealth = 100;
            MaxAttack = 10;
            
            Passive();
            
            CurrentHealth = MaxHealth;
            CurrentAttack = MaxAttack;
        }
        
        public override void Passive()
        {
            TotalEvasion += .05;

            CanUpdatePassive = false;
        }

        public override void UpdatePassive()
        {
            throw new System.NotImplementedException();
        }

        public override void Active(Enemy enemy)
        {
            void Evasive(Character character)
            {
                TotalEvasion += .3;
            }

            void RemoveEvasive(Character character)
            {
                TotalEvasion -= .3;
            }
            var evasive = new Buff("Grapply Jump", 1);
            evasive.ApplyEffect = Evasive;
            evasive.RemoveEffect = RemoveEvasive;
            
            evasive.Apply(this, 400);
        }

        public override void Ultimate(Enemy enemy)
        {
            enemy.Dead = true; //immediately kills the enemy
            //In a future update, revanant will take this ability
        }
    }
}