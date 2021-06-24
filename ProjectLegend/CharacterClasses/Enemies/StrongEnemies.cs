namespace ProjectLegend.CharacterClasses.Enemies
{
    public sealed class Flyer : Enemy
    {
        public Flyer()
        {
            MaxHealth = StatGenerator.Next(35, 50); 
            MaxAttack = StatGenerator.Next(15, 20);
        }
    }

    public sealed class Prowler : Enemy
    {
        public Prowler()
        {
            MaxHealth = StatGenerator.Next(25, 40); 
            MaxAttack = StatGenerator.Next(10, 15);
        }
    }
}