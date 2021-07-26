using System;

namespace ProjectLegend.ItemClasses.GearClasses.GearTypes
{ 
    //TODO: Shift from Attributes to Classes

    public abstract class RarityType
    {
        public string Rarity { get; protected set; }
        
    }

    public class Common : RarityType
    {
        public Common()
        {
            Rarity = "Common";
        }
    }
    
    public class Rare : RarityType
    {
        public Rare()
        {
            Rarity = "Rare";
        }
    }
    
    //NOT YET IMPLEMENTED
    public class Epic : RarityType
    {
    }
    
    public class Legendary : RarityType
    {
    }
    
    public class HeirloomAttribute : RarityType
    {
    }
    
    
    
}