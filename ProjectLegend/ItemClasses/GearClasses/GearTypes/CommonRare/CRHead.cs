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
            CommonStatRolls(NumCommonRolls);
        }

        public void CommonStatRolls(int rolls)
        {
            gearStats = Utils.GetRandomStats(CharacterStats.StandardStats, rolls);
            foreach (var stat in gearStats)
            {
                stat.RollStat();
            }
        }
    }
}