namespace ProjectLegend.CharacterClasses.Enemies
{
    public class Leviathan : Enemy
    {
        public Leviathan()
        {
            Health.Max = StatGenerator.Next(10, 20); 
            Attack.Max = StatGenerator.Next(5, 10);
        }
    }

    public class Goliath : Enemy
    {
        public Goliath()
        {
            Health.Max = StatGenerator.Next(10, 20); 
            Attack.Max = StatGenerator.Next(5, 10);
        }
    }
}