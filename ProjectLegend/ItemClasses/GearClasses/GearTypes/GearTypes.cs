﻿using System;
using System.Collections.Generic;
using ProjectLegend.GameUtilities;

namespace ProjectLegend.ItemClasses.GearClasses.GearTypes
{
    public abstract class Head : Gear //Omnifensive
    {
        protected Head()
        {
            Slot = 0;
            Type = "Head";
        }

        protected List<Stat> RollCommonStats()
        {
            var stats = Utils.GetRandomStats(StatPools.OmnifensiveCommonStats, NumCommonRolls);
            return stats;
        }
        
        protected List<Stat> RollRareStats()
        {
            var stats = Utils.GetRandomStats(StatPools.OmnifensiveRareAndAboveStats, NumRareRolls);
            return stats;
        }
    }

    public abstract class Chest : Gear //Defensive
    {
        protected Chest()
        {
            Slot = 1;
            Type = "Chest";
        }
        
        protected List<Stat> RollCommonStats()
        {
            var stats = Utils.GetRandomStats(StatPools.DefensiveCommonStats, NumCommonRolls);
            return stats;
        }
        
        protected List<Stat> RollRareStats()
        {
            var stats = Utils.GetRandomStats(StatPools.DefensiveRareAndAboveStats, NumRareRolls);
            return stats;
        }
    }

    public abstract class Legs : Gear //Omnifensive
    {

        protected Legs()
        {
            Slot = 2;
            Type = "Legs";
        }
        
        protected List<Stat> RollCommonStats()
        {
            var stats = Utils.GetRandomStats(StatPools.OmnifensiveCommonStats, NumCommonRolls);
            return stats;
        }
        
        protected List<Stat> RollRareAndAboveStats()
        {
            var stats = Utils.GetRandomStats(StatPools.OmnifensiveRareAndAboveStats, NumRareRolls);
            return stats;
        }
    }

    public abstract class Weapon : Gear //Offensive
    {

        protected Weapon()
        {
            Slot = 3;
            Type = "Weapon";
        }
        
        protected List<Stat> RollCommonStats()
        {
            var stats = Utils.GetRandomStats(StatPools.OffensiveCommonStats, NumCommonRolls);
            return stats;
        }
        
        protected List<Stat> RollRareStats()
        {
            var stats = Utils.GetRandomStats(StatPools.OffensiveRareAndAboveStats, NumRareRolls);
            return stats;
        }
    }
}