namespace ProjectLegend.ItemClasses.GearClasses.GearTypes.CommonGear
{
    public sealed class CAWoodArmour : Chest
    {
        public CAWoodArmour()
        {
            Name = "Wood Armour";
            Rarity = new Common();
            
            GearStats = RollCommonStats();
            DropRate = .5;
        }
    }
    
    public sealed class CAChainMail : Chest
    {
        public CAChainMail()
        {
            Name = "Weak Chain Mail Armour";
            Rarity = new Common();
            
            GearStats = RollCommonStats();
            DropRate = .5;
        }
    }
    
    public sealed class CAMIPlate : Chest
    {
        public CAMIPlate()
        {
            Name = "Shattered Mineral-Infused Plate";
            Rarity = new Common();
            
            GearStats = RollCommonStats();
            DropRate = .5;
        }
    }
    
    public sealed class CAVineCloak : Chest
    {
        public CAVineCloak()
        {
            Name = "Vine-Wrapped Cloak";
            Rarity = new Common();
            
            GearStats = RollCommonStats();
            DropRate = .5;
        }
    }
    
    public sealed class CADirtArmour : Chest
    {
        public CADirtArmour()
        {
            Name = "Rocky Dirty Armour";
            Rarity = new Common();
            
            GearStats = RollCommonStats();
            DropRate = .5;
        }
    }
}