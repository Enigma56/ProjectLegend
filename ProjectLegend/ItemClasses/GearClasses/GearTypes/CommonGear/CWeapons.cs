namespace ProjectLegend.ItemClasses.GearClasses.GearTypes.CommonGear
{
    public sealed class CWWoodStick : Weapon 
    {
        public CWWoodStick()
        {
            Name = "Sharp Wood Stick";
            Rarity = new Common();

            GearStats = RollCommonStats();
            DropRate = .5;
        }
    }

    public sealed class CWObsidianHilt : Weapon
    {
        public CWObsidianHilt()
        {
            Name = "Obsidian Sword Hilt";
            Rarity = new Common();

            GearStats = RollCommonStats();
            DropRate = .5;
        }
    }
    
    public sealed class CWIronSword : Weapon
    {
        public CWIronSword()
        {
            Name = "Dull Iron Sword";
            Rarity = new Common();

            GearStats = RollCommonStats();
            DropRate = .5;
        }
    }
    
    public sealed class CWVineSword : Weapon
    {
        public CWVineSword()
        {
            Name = "Limp Vine Sword";
            Rarity = new Common();

            GearStats = RollCommonStats();
            DropRate = .5;
        }
    }
    
    public sealed class CWWoodSword : Weapon
    {
        public CWWoodSword()
        {
            Name = "Chipped Wooden Sword";
            Rarity = new Common();

            GearStats = RollCommonStats();
            DropRate = .5;
        }
    }
}