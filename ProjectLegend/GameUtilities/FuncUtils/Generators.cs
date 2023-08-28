namespace ProjectLegend.GameUtilities.FuncUtils
{
    public class Generators
    {
        public static Random RandomNumber { get; }
        public static Random Stat { get; }

        static Generators()
        {
            RandomNumber = new Random();
            Stat = new Random();
        }
    }
}