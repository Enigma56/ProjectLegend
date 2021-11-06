using System;

namespace ProjectLegend.GameUtilities.FuncUtils
{
    public class RandomGenerators
    {
        public static Random IntGenerator { get; }

        static RandomGenerators()
        {
            IntGenerator = new Random();
        }
    }
}