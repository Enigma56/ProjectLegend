using System.Collections.Generic;

namespace ProjectLegend.ItemClasses.GearClasses
{
    public static class StatPools
    {
        public static List<Stat> OmnifensiveCommonStats { get; }
        public static List<Stat> OmnifensiveRareAndAboveStats { get; }
        
        public static List<Stat> OffensiveCommonStats { get; }
        public static List<Stat> OffensiveRareAndAboveStats { get; }
        
        public static List<Stat> DefensiveCommonStats { get; }
        public static List<Stat> DefensiveRareAndAboveStats { get; }

        static StatPools()
        {
            OmnifensiveCommonStats = new List<Stat>()
            {
                new Strength(),
                new Vitality(),
                new FlatAttIncrease(),
                new FlatHpIncrease(),
                new AttackPercentIncrease(), //Rare and above
                new HealthPercentIncrease()
            };
            OmnifensiveRareAndAboveStats = new List<Stat>()
            {
                new Strength(),
                new Vitality(),
                new FlatAttIncrease(),
                new FlatHpIncrease(),
                new AttackPercentIncrease(), //Rare and above
                new HealthPercentIncrease()
            };

            OffensiveCommonStats = new List<Stat>()
            {
                new Strength(),
                new FlatAttIncrease()
            };
            
            OffensiveRareAndAboveStats = new List<Stat>()
            {
                new Strength(),
                new FlatAttIncrease(),
                new AttackPercentIncrease()
            };

            DefensiveCommonStats = new List<Stat>()
            {
                new Vitality(),
                new FlatHpIncrease()
            };
            
            DefensiveRareAndAboveStats = new List<Stat>()
            {
                new Vitality(),
                new FlatHpIncrease(),
                new HealthPercentIncrease()
            };
        }
        
    }
}