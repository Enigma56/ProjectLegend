namespace ProjectLegend.ItemClasses.GearClasses.GearTypes.CommonGear
{
    public sealed class CLTatteredBoots : Legs
    {
        public CLTatteredBoots()
        {
            Name = "Tattered Boots";
            Rarity = new Common();

            GearStats = RollCommonStats();
            DropRate = .5;
        }
    }

    public sealed class CLVoidSandals : Legs //Restorable
    {
        public CLVoidSandals()
        {
            Name = "Heavily-Damaged Void Sandals";
            Rarity = new Common();

            GearStats = RollCommonStats();
            DropRate = .5;
        }
    }
    
    public sealed class CLDesertSandals : Legs
    {
        public CLDesertSandals()
        {
            Name = "Desert Sandals";
            Rarity = new Common();

            GearStats = RollCommonStats();
            DropRate = .5;
        }
    }
    
    public sealed class CLFootGuards : Legs
    {
        public CLFootGuards()
        {
            Name = "Wooden Foot Guards";
            Rarity = new Common();

            GearStats = RollCommonStats();
            DropRate = .5;
        }
    }
    
    public sealed class CLCombatBoots : Legs
    {
        public CLCombatBoots()
        {
            Name = "Shredded Combat Boots";
            Rarity = new Common();

            GearStats = RollCommonStats();
            DropRate = .5;
        }
    }
}