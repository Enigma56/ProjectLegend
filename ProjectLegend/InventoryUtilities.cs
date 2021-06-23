﻿using System;
using System.Linq;

using ProjectLegend.CharacterClasses;
using ProjectLegend.Items;

namespace ProjectLegend
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
                choice = Utils.ReadInput(new string[] {"Add", "Discard"})[0];
            } while (!(Equals(choice, "add") | Equals(choice, "discard")));

            switch (choice)
            {
                case "add":
                    player.AddToInventory(player.Hand);
                    break;
                case"discard":
                    //effectively a "do nothing"
                    break;
                default:
                    Console.WriteLine("not a valid choice");
                    break;
            }
        }
        
        public static void AddToInventory(this Player player, Item droppedItem)
        {
            var nextAvailableSlot = Array.IndexOf(player.Inventory,player.Inventory.FirstOrDefault(slot => slot == null));
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
                bool inputInRange;
                do
                {
                    string replaceItemSlot = Utils.ReadInput()[0];
                    int replaceItemIndex = int.Parse(replaceItemSlot);
                    inputInRange = 1 <= replaceItemIndex && replaceItemIndex <= player.Inventory.Length;
                    if (inputInRange)
                    {
                        var targetItem = player.Inventory[replaceItemIndex - 1]; //item that will be replaced
                        player.Hand.Swap(targetItem, player);
                    }
                        
                    else
                    {
                        Console.WriteLine("Please enter a valid inventory slot!");
                    }
                } while (inputInRange is false);
            }
        }

        public static void Swap(this Item hand, Item target, Player player)
        {
            player.Inventory[target.InventorySlot] = hand;
            hand.InventorySlot = target.InventorySlot;
            target.InventorySlot = -1;
            
            player.Hand = target;
        }

    }
}