using ProjectLegend.GameUtilities.BuffUtilities;

namespace ProjectLegend.CharacterClasses.Legends
{
    public sealed class Pathfinder : Player
    {
        public static string Name = "Pathfinder";
        public Pathfinder()
        {
            Health.Max = 100;
            Attack.Max = 10;
            Health.Current = Health.Max;
            Attack.Current = Attack.Max;

            ActiveCost = 400;
            UltimateCost = 700;
            
            Passive();
            CanUpdatePassive = false;
        }
        
        public override void Passive()
        {
            Evasion.Total += .05;
        }

        public override void UpdatePassive() //Not being used
        {
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
            
            evasive.Apply(this, ActiveCost);
        }

        public override void Ultimate(Enemy enemy)
        {
            if(Energy.Current >= UltimateCost)
            {
                enemy.Dead = true; //immediately kills the enemy
                System.Console.WriteLine("You have run from battle!");
                Energy.Current -= UltimateCost;
            }
            else
            {
                System.Console.WriteLine("not enough energy!");
            }
            //In a future update, revanant will take this ability
            
        }
    }
}