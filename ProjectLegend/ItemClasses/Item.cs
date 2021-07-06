using System;

namespace ProjectLegend.ItemClasses
{
    public class Item
    {
        public string Name { get; init; }
        public bool IsStackable { get; init; }
        
        public int InventorySlot { get; set; }
        
        public double DropRate { get; init; }
        public override string ToString()
        {
            return Name;
        }
    }
    
    
}