﻿using ProjectLegend.CharacterClasses;

using ProjectLegend.ItemClasses.Consumables;
using ProjectLegend.ItemClasses.GearClasses;

using ProjectLegend.GameUtilities;
using ProjectLegend.GameUtilities.FaceUtils;

namespace ProjectLegend.ItemClasses
{
    public static class InventoryUtilities
    {
        public static void AddOrDiscard(this Item droppedItem)
        {
            Player.Instance.Hand = droppedItem;
            Utils.Separator('-');
            Console.WriteLine("Would you like to add or discard the item in hand?"+ Environment.NewLine + 
                              $"Item in hand: {Player.Instance.Hand}");
            string choice;
            do
            {
                choice = Utils.ReadInput(new[] { "Add", "Discard" })[0];
            } while (!(Equals(choice, "add") | Equals(choice, "discard")));

            switch (choice)
            {
                case "add":
                    Player.Instance.AddToInventory(Player.Instance.Hand);
                    break;
                case "discard":
                    Player.Instance.Hand = null;
                    break;
                default:
                    Console.WriteLine("not a valid choice");
                    break;
            }
        }
        
        /// <summary>
        /// Takes a dropped item and adds it to the players hand, inventory. Otherwise, it gets
        /// discarded
        /// </summary>
        /// <param name="player"></param>
        /// <param name="droppedItem"></param>
        private static void AddToInventory(this Player player, Item droppedItem)
        {
            int nextAvailableSlot =
                Array.IndexOf(player.Inventory, player.Inventory.FirstOrDefault(slot => slot == null));
            if (droppedItem.IsStackable && droppedItem is Consumable consumable)
            {
                int consumableSlot = Utils.GetItemIndex(player.Inventory, droppedItem);
                if (consumableSlot != -1 &&((Consumable)player.Inventory[consumableSlot]).StackSize < consumable.MaxStackSize)
                {
                    ((Consumable) player.Inventory[consumableSlot]).Increment();
                }
                else
                {
                    player.Inventory[nextAvailableSlot] = droppedItem;
                    ((Consumable) player.Inventory[nextAvailableSlot]).Increment();
                }
            }
            else //adds to newest slot 
            {
                if (nextAvailableSlot != -1)
                {
                    player.Inventory[nextAvailableSlot] = droppedItem;
                    player.Inventory[nextAvailableSlot].InventorySlot = nextAvailableSlot;
                }
                else
                {
                    Console.WriteLine("Your inventory is full! please swap an item" + Environment.NewLine +
                                      $"Item in hand: {player.Hand}");
                    player.DisplayInventory();
                    Console.Write("Please enter the slot of the item you want to replace: ");

                    bool slotParsed = false;
                    do
                    {
                        string replaceItemSlot = Utils.ReadInput(new[] {""})[0]; //only accepts first integer provided by user 
                        if (int.TryParse(replaceItemSlot, Utils.IntegerCultureAndFormat().numberStyles,
                            Utils.IntegerCultureAndFormat().culture, out int replaceItemIndex))
                        {

                            bool indexInRange = 1 <= replaceItemIndex && replaceItemIndex <= player.Inventory.Length;
                            if (indexInRange)
                            {
                                var targetItem = player.Inventory[replaceItemIndex - 1]; //item that will be replaced
                                player.Hand.Swap(targetItem, player);
                                slotParsed = true;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Please enter a valid inventory slot!");
                        }
                    } while (slotParsed is false);
                }
            }
        }

        private static void Swap(this Item hand, Item target, Player player)
        {
            player.Inventory[target.InventorySlot] = hand;
            hand.InventorySlot = target.InventorySlot;
            target.InventorySlot = -1;

            player.Hand = target;
            player.Hand.AddOrDiscard();
        }

        public static void TryUseConsumable(this Player player, string[] commands)
        {
            if (commands.Length > 2)
            {
                if (int.TryParse(commands[2], Utils.IntegerCultureAndFormat().numberStyles,
                    Utils.IntegerCultureAndFormat().culture, out int slot))
                {
                    slot = slot - 1;
                    if (slot is > 0 and < 10)
                    {
                        if (player.Inventory[slot] is Consumable consumable)
                        {
                            consumable.Use(player);
                            if (consumable.StackSize == 0) //Removes potion from inventory if applicable
                            {
                                player.Inventory[slot] = null;
                            }

                            player.DisplayInventory();
                        }

                        else
                        {
                            Console.WriteLine("Item trying to be used is not a consumable!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Slot number not valid!");
                    }
                }
            }
        }

        public static void TryEquipGear(this Player player, string[] commands)
        {
            if (commands.Length > 2)
            {
                if (int.TryParse(commands[2], Utils.IntegerCultureAndFormat().numberStyles,
                    Utils.IntegerCultureAndFormat().culture, out int slot)) //Parses the ingeteger
                {
                    slot = slot - 1; //reduction for indexing
                    if (slot is >= 0 and < 10)
                    {
                        if (player.Inventory[slot] is Gear gear)
                        {
                            player.Equip(gear);
                            player.Inventory[slot] = null;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Slot number not valid!");
                    }
                }
            }
        }
        
        public static void TryUnEquipGear(this Player player, string[] commands)
        {
            if (commands.Length > 2)
            {
                if (int.TryParse(commands[2], Utils.IntegerCultureAndFormat().numberStyles,
                    Utils.IntegerCultureAndFormat().culture, out int slot)) //Parses the integer
                {
                    slot = slot - 1;
                    if (slot is >= 0 and < 4 & player.GearInventory[slot] != null)
                    {
                        player.UnEquip(player.GearInventory[slot]);
                    }
                    else
                    {
                        Console.WriteLine("Slot is empty!");
                    }
                }
            }
        }

        /// <summary>
        /// Responsible for equipping a new piece of gear on the player. If applicable, will swap pieces of gear in
        /// a conflicting circumstance.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="newGear"></param>
        public static void Equip(this Player player, Gear newGear)
        {
            int gearSlot = newGear.Slot;
            Gear oldGear = null;

            if (player.GearInventory[gearSlot] != null) //replaces old gear if slots are conflicting
            {
                oldGear = player.GearInventory[gearSlot];
                Console.WriteLine($"{oldGear.Name} has been removed and added to inventory!");
                player.AddToInventory(oldGear);
            }

            player.GearInventory[gearSlot] = newGear;
            player.UpdatePlayerStats(newGear, oldGear, "new/replace");
            Console.WriteLine("{0} has been equipped!", newGear);
        }

        public static void UnEquip(this Player player, Gear gear) //When asking if the user wants to unequip the piece of gear
        {
            int gearSlot = gear.Slot;
            if (player.GearInventory[gearSlot] == null) //cannot un-equip when slot is empty
            {
                Console.WriteLine("Current slot is empty!");
            }
            else
            {
                player.GearInventory[gearSlot] = null;
                player.AddToInventory(gear); //Adds it into the next available slot
                Console.WriteLine($"{gear.Name} has been removed and added to inventory!");
                player.UpdatePlayerStats(gear, null, "replace");
            }
        }
    }
}