using System.Collections.Generic;

using ProjectLegend.GameWorld.RoyalMarsh;
using ProjectLegend.GameWorld.RoyalMarsh.Locations.Encampments;

namespace ProjectLegend.GameWorld
{
    public static class WorldUtils
    {
        public static string[] MapChoices = {RoyalMarshMap.ID, "back"};
        public static string[] LocationChoices = {Caves.ID, Marsh.ID, "back"};
        
        public static void AddMaps(Dictionary<string, Map> mapDict)
        {
            mapDict[RoyalMarshMap.ID] = new RoyalMarshMap();
        }
        public static void AddRMLocations(Dictionary<string, Location> locationDict)
        {
            locationDict[Caves.ID] = new Caves();
            locationDict[Marsh.ID] = new Marsh();
        }
    }
}