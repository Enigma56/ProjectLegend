using System;

namespace ProjectLegend.Items
{
    public class Item
    {
        public string Name { get; set; }
        public bool IsStackable { get; init; }
        
        public int InventorySlot { get; set; }
        
        public double DropRate { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
    
    
}