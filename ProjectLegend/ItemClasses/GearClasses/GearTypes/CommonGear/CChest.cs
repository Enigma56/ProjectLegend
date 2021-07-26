namespace ProjectLegend.ItemClasses.GearClasses.GearTypes.CommonGear
{
    public sealed class WoodArmour : Chest
    {
        public WoodArmour()
        {
            Name = "Wood Armour";
            Rarity = new Common();
            
            GearStats = RollCommonStats();
            DropRate = .5;
        }
    }
    
    public sealed class CAWeakChainMail : Chest
    {
        public CAWeakChainMail()
        {
            Name = "Weak Chain Mail Armour";
            Rarity = new Common();
            
            GearStats = RollCommonStats();
            DropRate = .5;
        }
    }
    
    public sealed class CAShatteredMIPlate : Chest
    {
        public CAShatteredMIPlate()
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
    
    public sealed class CARockyDirtArmour : Chest
    {
        public CARockyDirtArmour()
        {
            Name = "";
            Rarity = new Common();
            
            GearStats = RollCommonStats();
            DropRate = .5;
        }
    }
}