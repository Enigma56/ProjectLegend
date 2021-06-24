namespace ProjectLegend.CharacterClasses.Enemies
{
    
    public sealed class BinSpider :  Enemy
    {
        public BinSpider()
        {
            MaxHealth = StatGenerator.Next(10, 20); 
            MaxAttack = StatGenerator.Next(5, 10);
        }
    }

    public sealed class CarthageSpider : Enemy
    {
        public CarthageSpider()
        {
            MaxHealth = StatGenerator.Next(20, 40); 
            MaxAttack = 10;
        }
        
    }
}