using System.Collections;
using System.Collections.Generic;

using ProjectLegend.CharacterClasses.Enemies;

namespace ProjectLegend.World
{
    public abstract class Location
    {
        public string Name { get; protected set; }
        public bool Completed { get; set; }

        public string ID { get; set; }

        public Queue<EnemyCluster> Enemies { get; protected set; }

        //Types of Locations
        
        //public GearPool LocationLoot { get; protected set; }
        public abstract void Instantiate(int enemyWaves);

    }
}