using System.Collections.Generic;
using ProjectLegend.GameWorld.RoyalMarsh.Locations.Encampments;

namespace ProjectLegend.GameWorld.RoyalMarsh
{
    public sealed class RoyalMarshMap : Map
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
            Locations = new[] { "caves" };
            Initialize();
        }
        
        public override void Initialize()
        { 
            // Add locations
            WorldUtils.AddRMLocations(LocationDict);
            
            //Add enemies to each location
            foreach (string loc in LocationDict.Keys)
            {
                LocationDict[loc].Instantiate(3); //Here is where the number of waves is added to the location
            }
            //Initialize all other location-specific activity
        }
    }
}