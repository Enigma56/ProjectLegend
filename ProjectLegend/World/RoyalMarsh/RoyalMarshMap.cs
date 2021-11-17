using System.Collections.Generic;
using ProjectLegend.World.RoyalMarsh.Locations.Encampments;

namespace ProjectLegend.World.RoyalMarsh
{
    public class RoyalMarshMap : Map
    {
        public static string ID { get; }

        //Mark completed/incomplete maps
        static RoyalMarshMap()
        {
            ID = "royalmarsh";
        }
        public RoyalMarshMap()
        {
            LocationDict = new Dictionary<string, Location>();
            CompileMap();
        }
 
        //TODO: dinish the creation of the method
        private void CompileMap()
        { 
            // Add locations
            WorldUtils.AddRMLocations(LocationDict);
            //Add enemies to each location
            foreach (string loc in LocationDict.Keys)
            {
                LocationDict[loc].Instantiate(3);
            }
            //Initialize all other location-specific activity
        }
    }
}