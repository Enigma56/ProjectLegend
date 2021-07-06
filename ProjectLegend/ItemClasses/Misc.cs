using System.Collections;

namespace ProjectLegend.ItemClasses
{
    public class Misc : Item
    {
        public int MaxStackSize { get; set; }
        public int StackSize { get; set; }
        
        public Misc()
        {
            Name = "Misc";
            StackSize = 0;
            MaxStackSize = 100;
        }
        
        public void Increment()
        {
            StackSize += 1;
        }
    }
}