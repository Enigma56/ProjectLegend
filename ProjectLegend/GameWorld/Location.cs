using System.Collections.Generic;

using ProjectLegend.GameUtilities;
using ProjectLegend.CharacterClasses;
using ProjectLegend.CharacterClasses.Enemies;

namespace ProjectLegend.GameWorld
{
    public abstract class Location
    {
        public string Name { get; protected init; }
        public bool Completed { get; set; }

        public Queue<EnemyCluster> Enemies { get; protected set; }

        //Types of Locations
        
        //public GearPool LocationLoot { get; protected set; }
        public abstract void Instantiate(int enemyWaves);
        public abstract void Play();

        public void Close(Player player) //The same closing process for the completion of every single map 
        {
            Completed = true;
            //Resets linked list
            var header = GameManager.BackPointers.First.Value;
            GameManager.BackPointers.Clear();
            
            //executes main game loop
            GameManager.BackPointers.AddFirst(header);
            var gameLoop = GameManager.BackPointers.First.Value;
            gameLoop(player);
        }

    }
}