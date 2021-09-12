using ProjectLegend.ItemClasses.GearClasses;

namespace ProjectLegend.Maps.KingsCanyon.Locations
{
    public abstract class Location
    {
        public string Name { get; protected set; }
        
        //Types of Locations
        
        public GearPool LocationLoot { get; protected set; }
        
        public bool Completed { get; set; }
        
        protected Location()
        {
            
        }
        
    }
}