using System;
using System.Collections.Generic;
using System.Linq;


namespace ProjectLegend.Maps
{
    public abstract class Map
    {
        public Dictionary<string, Location> LocationList; //perhaps use a Location ID to hash into the instance
        public List<string> CompletedLocationKey;
        public List<string> IncompleteLocationKey;

        public bool Complete { get; set; }

        public void CheckCompletion()
        {
            if (IncompleteLocationKey.Count == 0)
            {
                Complete = true;
                Console.WriteLine("You've completed all the maps!");
            }
        }
    }
}