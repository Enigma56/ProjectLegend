﻿using System.Collections.Generic;
using ProjectLegend.CharacterClasses.Enemies;

namespace ProjectLegend.World.RoyalMarsh.Locations.Encampments
{
    public class Caves : Location
    {
        public static string ID { get; }
        static Caves()
        {
            ID = "caves";
        }
        public Caves()
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

        public override string ToString()
        {
            return Name;
        }
        
    }
}