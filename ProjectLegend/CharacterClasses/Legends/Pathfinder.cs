namespace ProjectLegend.CharacterClasses.Legends
{
    public sealed class Pathfinder : Player
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
            var evasive = new Buff("Grapple Jump", 1);
            evasive.ApplyEffect = Evasive;
            evasive.RemoveEffect = RemoveEvasive;
            
            evasive.Apply(this, 400);
        }

        public override void Ultimate(Enemy enemy)
        {
            int energyCost = 700;
            if(CurrentEnergy >= energyCost)
            {
                enemy.Dead = true; //immediately kills the enemy
                System.Console.WriteLine("You have run from battle!");
            }
            else
            {
                System.Console.WriteLine("not enough energy!");
            }
            //In a future update, revanant will take this ability
            
        }
    }
}