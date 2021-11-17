using System.Collections.Generic;

namespace ProjectLegend.World
{
    public class World
    {
        public Dictionary<string, Map> MapDict { get; }

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