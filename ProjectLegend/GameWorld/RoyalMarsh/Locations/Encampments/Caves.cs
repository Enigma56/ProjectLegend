using System.Collections.Generic;
using ProjectLegend.CharacterClasses.Enemies;
using ProjectLegend.GameUtilities;

namespace ProjectLegend.GameWorld.RoyalMarsh.Locations.Encampments
{
    public class Caves : Location
    {
        public static string ID = "caves";
        
        public Caves() //Instances are automatically reset upon selection
        {
            Name = "Caves";
            Enemies = new Queue<EnemyCluster>();
        }

        public override void Instantiate(int enemyWaves)
        {
            for (int wave = 0; wave < enemyWaves; wave++) {
                Enemies.Enqueue(new EnemyCluster(3));
            }
        }

        public override void Play()
        {
        }

        public override void Close() //todo: determine what else to put inside this function
        {
            Completed = true;
        }

        public override string ToString()
        {
            return Name;
        }
        
    }
}