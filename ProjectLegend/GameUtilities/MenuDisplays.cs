using System;
using ProjectLegend.CharacterClasses;
using ProjectLegend.ItemClasses.GearClasses;

namespace ProjectLegend.GameUtilities
{
    public static class MenuDisplays
    {
        public static void DisplayEquipment(this Player player)
        {
            string gearDisplay = "";
            for(int slot = 0; slot < player.GearInventory.Length; slot++)
            {
                string gearName = player.GearInventory[slot] != null ? player.GearInventory[slot].Name : "None"; 
                gearDisplay += $"{(GearType) slot}: {gearName}" + Environment.NewLine;
            }
            gearDisplay = gearDisplay.TrimEnd();
            Console.WriteLine(gearDisplay);
        }
        
        public static void DisplayInventory(this Player player)
        {
            string stringArray = "[";
            for (int i = 0; i < player.Inventory.Length; i++)
            {
                if (i % 2 == 0)
                    stringArray += Environment.NewLine;
                stringArray += $"{i + 1}: {Utils.PrintArrayElement(player.Inventory[i])}, "; // creates string representation of array
            }

            stringArray += $"{player.Inventory.Length}: {Utils.PrintArrayElement(player.Inventory[^1])}]";
            Console.WriteLine(stringArray);
        }
        
        public static void DisplayBuffs(this Character player)
        {
            if (player.Buffs.Count > 0)
            {
                string buffs = "";
                foreach (var buff in player.Buffs)
                {
                    buffs += $"{buff}: Turns Remaining {buff.TurnsRemaining}" + Environment.NewLine;
                }
                Console.Write(buffs);
            }
            else
            {
                Console.WriteLine("No Active Buffs!");
            }
        }
        
    }
}