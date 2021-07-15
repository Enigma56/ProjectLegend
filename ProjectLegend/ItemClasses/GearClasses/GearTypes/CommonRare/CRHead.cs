using System;
using System.Collections.Generic;

using ProjectLegend.GameUtilities;

namespace ProjectLegend.ItemClasses.GearClasses.GearTypes.CommonRare
{
    public sealed class CHead1 : Head, ICommon //TODO: Implement first piece of common/rare gear
    {
        public List<Stat> headStats { get; set; }
        public CHead1() //TODO: Test implementation of stats
        {
            Name = "Tattered Helm";
            CommonStatRolls(NumCommonRolls);
        }

        public void CommonStatRolls(int rolls)
        {
            headStats = Utils.GetRandomStats(CharacterStats.StandardStats, rolls);
            foreach (var stat in headStats)
            {
                object min = stat.Range.GetMin();
                object max = stat.Range.GetMax();
                
                if(stat.Type.Equals("add"))
                    stat.RollStatValues((int) min, (int) max);
                else
                {
                    stat.RollStatValues((double) min, (double) max);
                }
            }
        }

        public override string ToString()
        {
            string StatString()
            {
                string stats = "";
                foreach (var stat in headStats)
                {
                    stats += Environment.NewLine + $"{stat.Name}: {stat.StatValue}";
                }
                return stats;
            }
            
            return $"{Name}" + StatString();
        }
    }
}