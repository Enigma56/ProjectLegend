using System;

namespace ProjectLegend.Items
{
    public class Gear : Item
    {
        public static Guid Id { get; set; }
        public Gear()
        {
            Name = "Gear Not Implemented";
            Id = Guid.NewGuid();
        }
        
    }
}