using System;
using System.Collections.Generic;
using ProjectLegend.CharacterClasses;
using ProjectLegend.ItemClasses.GearClasses.GearTypes;

namespace ProjectLegend.ItemClasses.GearClasses
{
    public abstract class Gear : Item
    {
        protected static readonly int NumCommonRolls = 1;
        protected static readonly int NumRareRolls = 2;
        protected static readonly int NumEpicRolls = 3;
        protected static readonly int NumLegendaryRolls = 4;
        protected static readonly int NumHeirloomRolls = 4;
        
        public RarityType Rarity { get; protected set; }
        public List<Stat> GearStats { get; set; }
        public int Slot { get; set; }
        public string Type { get; protected set; }
        
        //public bool IsRestorable {get;}

        protected Gear()
        {
            Name = "No Name!";

            IsStackable = false;
            GearStats = new List<Stat>();
        }
        
        public string ToStringWithStats()
        {
            string StatString()
            {
                string stats = "";
                foreach (var stat in GearStats)
                {
                    stats += Environment.NewLine + stat;
                }
                return stats;
            }
            
            return $"{Name}" + StatString();
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public enum GearType
    {
        Head = 0,
        Chest = 1,
        Legs = 2,
        Weapon = 3
    }
}