using System.Collections.Generic;
using ProjectLegend.CharacterClasses.Enemies;

namespace ProjectLegend.GameWorld.RoyalMarsh.Locations.Encampments
{
    public class Marsh : Location
    {
        public static string ID = "marsh";

        public Marsh()
        {
            Name = "Marsh";
            Enemies = new Queue<EnemyCluster>();
        }
        public override void Instantiate(int enemyWaves)
        {
            for (int wave = 0; wave < enemyWaves; wave++) {
                Enemies.Enqueue(new EnemyCluster(3));
            }
        }
    }
}