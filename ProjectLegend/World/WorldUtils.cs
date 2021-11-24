using System;
using System.Collections.Generic;
using System.IO.Compression;
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
        
        public static Map ChooseMap(Dictionary<string, Map> mapDict, string choice) //Function for choosing the map
        {
            if (choice.Equals(RoyalMarshMap.ID))
            {
                return mapDict[choice];
            }
            else if (choice.Equals("testcase1"))
            {
                Console.WriteLine("testcase1...not entering any map");
                return null;
            }
            else
            {
                Console.WriteLine("Not a valid map!");
                return null;
            }
        }

        public static void ChooseLocation()
        {
        }
    }
}