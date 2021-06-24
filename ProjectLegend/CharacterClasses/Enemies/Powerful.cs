namespace ProjectLegend.CharacterClasses.Enemies
{
    public class Leviathan : Enemy
    {
        public Leviathan()
        {
            MaxHealth = StatGenerator.Next(10, 20); 
            MaxAttack = StatGenerator.Next(5, 10);
        }
    }

    public class Goliath : Enemy
    {
        public Goliath()
        {
            MaxHealth = StatGenerator.Next(10, 20); 
            MaxAttack = StatGenerator.Next(5, 10);
        }
    }
}