
namespace ProjectLegend.ItemClasses
{
    public class ItemPool
    {
        
    }
    
    public abstract class Item
    {
        public string Name { get; init; }
        public bool IsStackable { get; init; }
        
        public int InventorySlot { get; set; }
        
        public double DropRate { get; protected init; }
        
        public override string ToString()
        {
            return Name;
        }
    }
    
    
}