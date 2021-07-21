namespace ProjectLegend.CharacterClasses.Enemies
{
    public sealed class Flyer : Enemy
    {
        public Flyer()
        {
            Health.Max = StatGenerator.Next(35, 50); 
            Attack.Max = StatGenerator.Next(15, 20);
        }
    }

    public sealed class Prowler : Enemy
    {
        public Prowler()
        {
            Health.Max = StatGenerator.Next(25, 40); 
            Attack.Max = StatGenerator.Next(10, 15);
        }
    }
}