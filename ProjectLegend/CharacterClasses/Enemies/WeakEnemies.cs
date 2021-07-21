namespace ProjectLegend.CharacterClasses.Enemies
{
    
    public sealed class BinSpider :  Enemy
    {
        public BinSpider()
        {
            Health.Max = StatGenerator.Next(10, 20); 
            Attack.Max = StatGenerator.Next(5, 10);
        }
    }

    public sealed class CarthageSpider : Enemy
    {
        public CarthageSpider()
        {
            Health.Max = StatGenerator.Next(20, 40); 
            Attack.Max = 10;
        }
        
    }
}