﻿using System;
using System.Linq;

using ProjectLegend.CharacterClasses;
using ProjectLegend.GameUtilities;
using ProjectLegend.ItemClasses.Consumables;

namespace ProjectLegend.ItemClasses
{
    public static class InventoryUtilities
    {
        public static void AddOrDiscard(this Item droppedItem, Player player)
        {
            player.Hand = droppedItem;
            Console.WriteLine("Would you like to add or discard the item in hand?"+ Environment.NewLine + 
                              $"Item in hand: {player.Hand}");
            string choice;
            do
            {
                choice = Utils.ReadInput("Add", "Discard")[0];
            } while (!(Equals(choice, "add") | Equals(choice, "discard")));

            switch (choice)
            {
                case "add":
                    player.AddToInventory(player.Hand);
                    break;
                case"discard":
                    player.Hand = null;
                    break;
                default:
                    Console.WriteLine("not a valid choice");
                    break;
            }
        }
        
        private static void AddToInventory(this Player player, Item droppedItem)
        {
            Console.WriteLine(droppedItem.GetType());
            int nextAvailableSlot =
                Array.IndexOf(player.Inventory, player.Inventory.FirstOrDefault(slot => slot == null));
            if (droppedItem.IsStackable && droppedItem is Consumable consumable)
            {
                int consumableSlot = Utils.GetItemIndex(player.Inventory, droppedItem);
                if (consumableSlot != -1)
                {
                    if(((Consumable)player.Inventory[consumableSlot]).StackSize < consumable.MaxStackSize)
                        ((Consumable) player.Inventory[consumableSlot]).Increment();
                    else
                    {
                        player.Inventory[nextAvailableSlot] = droppedItem;
                        ((Consumable) player.Inventory[nextAvailableSlot]).Increment();
                    }
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
                        string replaceItemSlot = Utils.ReadInput()[0]; //only accepts first integer provided by user 
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

        public static void Swap(this Item hand, Item target, Player player)
        {
            player.Inventory[target.InventorySlot] = hand;
            hand.InventorySlot = target.InventorySlot;
            target.InventorySlot = -1;
            
            player.Hand = target;
            
            player.Hand.AddOrDiscard(player);
        }

        public static void TryUseConsumable(this Player player, string[] commands)
        {
            if (commands.Length > 1)
            {
                if (commands[1].Equals("use") && commands.Length > 2)
                {
                    if (int.TryParse(commands[2], Utils.IntegerCultureAndFormat().numberStyles,
                        Utils.IntegerCultureAndFormat().culture, out int slot))
                    {
                        slot = slot - 1;
                        if (player.Inventory[slot] is Consumable consumable)
                        {
                            consumable.Use(player);
                            if (consumable.StackSize == 0)
                            {
                                player.Inventory[slot] = null;
                            }
                            player.DisplayInventory();
                        }
                    }
                }
            }
        }

    }
}