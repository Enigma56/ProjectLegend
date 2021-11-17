using System.Collections.Generic;
using ProjectLegend.World.RoyalMarsh;
using ProjectLegend.World.RoyalMarsh.Locations.Encampments;

namespace ProjectLegend.World
{
    public static class WorldUtils
    {
        public static void AddMaps(Dictionary<string, Map> mapDict)
        {
            mapDict[RoyalMarshMap.ID] = new RoyalMarshMap();
        }
        public static void AddRMLocations(Dictionary<string, Location> locationDict)
        {
            locationDict[Caves.ID] = new Caves();
        }
    }
}