using System.Collections.Generic;
using ProjectLegend.World.RoyalMarsh;

namespace ProjectLegend.World
{
    public class World
    {
        public Dictionary<string, Map> MapDict { get; }

        public string[] MapChoices = {RoyalMarshMap.ID, "TestCase1", "TestCase2"};

        public World()
        {
            MapDict = new Dictionary<string, Map>();
        }

        //TODO: Add all maps into the world
        public void Initialize()
        {
            WorldUtils.AddMaps(MapDict);
        }
    }
}