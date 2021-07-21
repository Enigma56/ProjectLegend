using ProjectLegend.GameUtilities.BuffUtilities;

namespace ProjectLegend.CharacterClasses.Legends
{
    public sealed class Pathfinder : Player
    {
        public Pathfinder()
        {
            Health.Max = 100;
            Attack.Max = 10;
            
            Passive();
            CanUpdatePassive = false;
            
            Health.Current = Health.Max;
            Attack.Current = Attack.Max;
        }
        
        public override void Passive()
        {
            Evasion.Total += .05;
        }

        public override void UpdatePassive()
        {
            throw new System.NotImplementedException();
        }

        public override void Active(Enemy enemy)
        {
            void Evasive(Character character)
            {
                Evasion.Total += .3;
            }

            void RemoveEvasive(Character character)
            {
                Evasion.Total -= .3;
            }
            var evasive = new Buff("Grapple Jump", 1);
            evasive.ApplyEffect = Evasive;
            evasive.RemoveEffect = RemoveEvasive;
            
            evasive.Apply(this, 400);
        }

        public override void Ultimate(Enemy enemy)
        {
            int energyCost = 700;
            if(Energy.Current >= energyCost)
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