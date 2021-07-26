using System;
using System.Collections.Generic;
using ProjectLegend.GameUtilities;

namespace ProjectLegend.ItemClasses.GearClasses.GearTypes
{

    //TODO: Add the stat pools to each type of gear according to type of gear
    
    public abstract class Head : Gear //Omnifensive
    {
        protected Head()
        {
            Slot = 0;
        }

        protected List<Stat> RollCommonStats()
        {
            var stats = Utils.GetRandomStats(CharacterStats.OmnifensiveCommonStats, NumCommonRolls);
            return stats;
        }
        
        protected List<Stat> RollRareStats()
        {
            var stats = Utils.GetRandomStats(CharacterStats.OmnifensiveRareAndAboveStats, NumRareRolls);
            return stats;
        }
    }

    public abstract class Chest : Gear //Defensive
    {
        protected Chest()
        {
            Slot = 1;
        }
        
        protected List<Stat> RollCommonStats()
        {
            var stats = Utils.GetRandomStats(CharacterStats.DefensiveCommonStats, NumCommonRolls);
            return stats;
        }
        
        protected List<Stat> RollRareStats()
        {
            var stats = Utils.GetRandomStats(CharacterStats.DefensiveRareAndAboveStats, NumRareRolls);
            return stats;
        }
    }

    public abstract class Legs : Gear //Omnifensive
    {

        protected Legs()
        {
            Slot = 2;
        }
        
        protected List<Stat> RollCommonStats()
        {
            var stats = Utils.GetRandomStats(CharacterStats.OmnifensiveCommonStats, NumCommonRolls);
            return stats;
        }
        
        protected List<Stat> RollRareStats()
        {
            var stats = Utils.GetRandomStats(CharacterStats.OmnifensiveRareAndAboveStats, NumRareRolls);
            return stats;
        }
    }

    public abstract class Weapon : Gear //Offensive
    {

        protected Weapon()
        {
            Slot = 3;
        }
        
        protected List<Stat> RollCommonStats()
        {
            var stats = Utils.GetRandomStats(CharacterStats.OffensiveCommonStats, NumCommonRolls);
            return stats;
        }
        
        protected List<Stat> RollRareStats()
        {
            var stats = Utils.GetRandomStats(CharacterStats.OffensiveRareAndAboveStats, NumRareRolls);
            return stats;
        }
    }
}