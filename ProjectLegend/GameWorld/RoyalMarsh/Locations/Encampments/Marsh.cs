using ProjectLegend.CharacterClasses;

namespace ProjectLegend.GameWorld.RoyalMarsh.Locations.Encampments
{
    public class Marsh : Location
    {
        public static string ID = "marsh";

        public Marsh()
        {
            Name = "Marsh";
        }
        public override void Instantiate(int enemyWaves)
        {
            throw new System.NotImplementedException();
        }
        public override void Play()
        {
        }
    }
}