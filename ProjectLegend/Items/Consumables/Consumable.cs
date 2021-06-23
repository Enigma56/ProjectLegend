using System.Collections.Generic;
using ProjectLegend.CharacterClasses;

namespace ProjectLegend.Items.Consumables
{
    public class Consumable : Item
    {
        //Fields
        public int MaxStackSize { get; set; }
        public int StackSize { get; set; }

        protected Consumable()
        {
            IsStackable = true;
            StackSize = 0;
            MaxStackSize = 20;
        }
        
        //Methods

        public void Increment()
        {
            StackSize += 1;
        }

        public void Decrement()
        {
            StackSize -= 1;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}