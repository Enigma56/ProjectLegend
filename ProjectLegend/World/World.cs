using System.Collections.Generic;
using ProjectLegend.World.RoyalMarsh;

namespace ProjectLegend.World
{
    public class World
    {
        public Dictionary<string, Map> MapDict { get; }

        public string[] MapChoices = {RoyalMarshMap.ID, "TestCase1", "TestCase2"};

        public World()//where in the fuck did this word come from
        {
            MapDict = new Dictionary<string, Map>();
        }
        
        public void Initialize() //Boots up the world
        {
            WorldUtils.AddMaps(MapDict);
        }
    }
}