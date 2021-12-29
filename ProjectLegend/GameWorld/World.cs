using System.Collections.Generic;

namespace ProjectLegend.GameWorld
{
    public class World
    {
        public Dictionary<string, Map> MapDict { get; }

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