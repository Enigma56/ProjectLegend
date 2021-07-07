using System;

using ProjectLegend.CharacterClasses;

namespace ProjectLegend.ItemClasses.Consumables
{
    public abstract class Consumable : Item
    {
        private static Guid _id;
        public int MaxStackSize { get; }
        public int StackSize { get; set; }
        protected int DropAmount { get; }
        protected int UseAmount { get; }
        //How? -> check if consumable is present in inventory, if so, increment stack size, otherwise add it to next available slot
        //when using consumables, ensure stack size > 0, if using decrements below 1, remove the item from inventory
        
        protected Consumable()
        {
            IsStackable = true;
            MaxStackSize = 20;
            StackSize = 0;
            
            DropAmount = 1;
            UseAmount = 1;
        }
        public abstract void Use(Player player);
        
        //Methods
        public void Increment()
        {
            StackSize += DropAmount;
        }

        public void Decrement()
        {
            StackSize -= UseAmount;
        }

        public override string ToString()
        {
            if(StackSize == 0)
                return $"{Name}: {DropAmount}";
            else
            {
                return $"{Name}: {StackSize}";
            }
        }
    }
}