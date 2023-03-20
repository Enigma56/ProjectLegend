using System;

using ProjectLegend.CharacterClasses;
using ProjectLegend.CharacterClasses.Legends;
using ProjectLegend.ItemClasses;
using ProjectLegend.GameUtilities.FaceUtils;
using ProjectLegend.GameWorld;

namespace ProjectLegend.GameUtilities.FuncUtils
{
    public static class UserQueries
    {
        public static readonly string[] PlayerLegends = {"Bangalore", "Bloodhound", "Gibraltar", "Lifeline", "Pathfinder", "Wraith"};
        
        /// <summary>
        /// Sets Player actions as the chosen legend
        /// </summary>
        /// <param name="input"></param>
        public static bool CharacterSelection(string input)
        {
            //No difference between this and switch for if-else branching
            //Offensive
            if (input.Equals(Wraith.Name.ToLower()))
            {
                Wraith wraithShell = new Wraith();
                //Set player abilities here from Wraith Class
                //I want to set the variables of player to wraith
                wraithShell.SetPlayerAsWraith();

                return true;
            }

            //TODO: IMPLEMENT ONE AT A TIME AFTER WRAITH IS FINISHED
            /*
            if (input.Equals(Bangalore.Name.ToLower()))
            {
                return new Bangalore();
            }
            if (input.Equals(Gibraltar.Name.ToLower()))
            {
                return new Gibraltar();
            }
            if (input.Equals(Lifeline.Name.ToLower()))
            {
                return new Lifeline();
            }
            if (input.Equals(Bloodhound.Name.ToLower()))
            {
                return new Bloodhound();
            }
            if (input.Equals(Pathfinder.Name.ToLower()))
            {
                return new Pathfinder();
            }*/
            Console.WriteLine("Not a valid character!");

            return false;
        }
        
        public static void GenGommandParse(World world, string[] commands, string input, bool flags)
        {
            string flag = "";
            if (flags && commands.Length > 1)
                flag = commands[1];
            
            switch(input)
            {
                case "select":
                    GameManager.GameFuncs.ChooseMap(); //encapsulates ChooseLocation as well
                    GameManager.GameFuncs.PlaySelection(world, GameManager.CurrentMap, GameManager.CurrentLocation);
                    break;
                case "inventory":
                    //Do you want to use a consumable from the inventory?
                    Utils.Separator('-');
                    Console.WriteLine("Inventory flags: " + Utils.ToString(new[] {"-e", "-u", "-eg", "-ueg"}));
                    if(flags) //parse flags from user
                        ParseInventoryFlags(commands, flag);
                    else
                    {
                        Player.Instance.DisplayInventory();
                    }
                    break;
                case "exit":
                    //Exit out of the game
                    Utils.ExitSequence("finish");
                    break;
                default:
                    Console.WriteLine("Command not found!");
                    break;
            }
        }
        
        public static (bool Parsed, string Choice) ParseMapChoices(string input)
        {
            switch (input)
            {
                case "royalmarsh":
                    //display locations to choose from and their status
                    //Adds map selection to linked list
                    return (true, "royalmarsh");
                case "back":
                    //Take them back to main choice option
                    //Exit this layer of a loop to go back to main game loop
                    return (true, "back"); //take the player back one step; Make sure null does not conflict
                default:
                    Console.WriteLine("Not a valid map!");
                    return (false, "");
            }
            
        }

        public static (bool Parsed, string Response) ParseLocationChoices(string input)
        {
            switch (input)
            {
                case "caves":
                    //Begin a caves instance and run!
                    return (true, "caves");
                case "back":
                    //close this lop and return to map choice loop
                    return (true, "back");
                default:
                    return (false, "nil");
            }
        }

        public static bool CombatCommandParse(Enemy enemy, string[] commands, string input, bool flags)
        {
            string flag = "";
            if (flags && commands.Length > 1)
                flag = commands[1];
            
            switch (input)
            {
                case "attack":
                    Console.WriteLine("Inventory flags: " + Utils.ToString(new[] {"-a", "-u"}));
                    if(flag.Length > 0)
                        ParseAttackFlags(enemy, flag);
                    GameManager.GameFuncs.BattlePhase(enemy); //Check if attack lands
                    Utils.Separator('-');
                    return true;
                    
                case "stats":
                    Console.WriteLine(Player.Instance.ToString());
                    return true;
                    
                case "buffs":
                    Player.Instance.DisplayBuffs();
                    return true;
                    
                case "inventory":
                    if(flags) //parse flags from user
                        ParseInventoryFlags(commands, flag);
                    else
                    {
                        Player.Instance.DisplayInventory();
                    }
                    return true;
                
                default:
                    Console.WriteLine("Not a valid command!");
                    return false;
            }
        }

        private static void ParseInventoryFlags(string[] commands, string flag)
        {
            switch (flag)
            {
                case "-e":
                    Utils.Separator('-');
                    Player.Instance.DisplayEquipment();
                    Utils.Separator('-');
                    break;
                case "-eg":
                    Player.Instance.TryEquipGear(commands);
                    break;
                case "-ueg":
                    Player.Instance.TryUnEquipGear(commands);
                    break;
                case "-u":
                    Player.Instance.TryUseConsumable(commands);
                    break;
                default:
                    Console.WriteLine("Not a valid flag!");
                    break;
            }
        }

        private static void ParseAttackFlags(Enemy enemy, string flag)
        {
            switch (flag)
            {
                case "-a":
                    Utils.Separator('*');
                    Console.WriteLine("You attempted to activate your Active ability!");
                    Player.Instance.Active(enemy);
                    break;
                case "-u":
                    Console.WriteLine("You attempted to activate your ultimate ability!");
                    Player.Instance.Ultimate(enemy);
                    break;
                default:
                    Console.WriteLine("Not a valid flag!");
                    break;
            }
        }
        
    }
}