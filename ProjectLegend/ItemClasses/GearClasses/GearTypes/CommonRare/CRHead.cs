using System;
using System.Collections.Generic;

using ProjectLegend.GameUtilities;

namespace ProjectLegend.ItemClasses.GearClasses.GearTypes.CommonRare
{
    public sealed class CHead1 : Head, ICommon 
    {
        
        public CHead1()
        {
            Name = "Tattered Helm";
            CommonStatRolls(NumLegendaryRolls);
        }

        public void CommonStatRolls(int rolls)
        {
            gearStats = Utils.GetRandomStats(CharacterStats.AllStats, rolls);
            foreach (var stat in gearStats)
            {
                dynamic min = stat.Range.GetMin();
                dynamic max = stat.Range.GetMax();
                
                Stat.RollStat(stat, min, max);
            }
        }

        public override string ToString()
        {
            string StatString()
            {
                string stats = "";
                foreach (var stat in gearStats)
                {
                    stats += Environment.NewLine + stat;
                }
                return stats;
            }
            
            return $"{Name}" + StatString();
        }
    }
}