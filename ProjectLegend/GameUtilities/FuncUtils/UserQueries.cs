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
        public static readonly string[] PlayerLegends = {"MilitaryWoman", "BloodIt", "BigBrotha", "MinuteMedic", "HappyRobot", "PortalGirl"};
        
        public static Player CharacterSelection(string input)
        { 
            //Offensive
            if (input.Equals(MilitaryWoman.Name.ToLower()))
            {
                return new MilitaryWoman();
            }
            else if (input.Equals(PortalGirl.Name.ToLower()))
            {
                return new PortalGirl();
            }
            else if (input.Equals(BigBrotha.Name.ToLower()))
            {
                return new BigBrotha();
            }
            else if (input.Equals(MinuteMedic.Name.ToLower()))
            {
                return new MinuteMedic();
            }
            else if (input.Equals(BloodIt.Name.ToLower()))
            {
                return new BloodIt();
            }
            else if (input.Equals(HappyRobot.Name.ToLower()))
            {
                return new HappyRobot();
            }
            else
            {
                Console.WriteLine("Not a valid character!");
            }
            
            return null;
        }
        
        public static void GenGommandParse(World world, Player player, string[] commands, string input, bool flags)
        {
            string flag = "";
            if (flags && commands.Length > 1)
                flag = commands[1];
            
            switch(input)
            {
                case "select":
                    GameManager.GameFuncs.ChooseMap(player); //encapsulates ChooseLocation as well
                    GameManager.GameFuncs.PlaySelection(world, GameManager.CurrentMap, GameManager.CurrentLocation, player);
                    break;
                case "inventory":
                    //Do you want to use a consumable from the inventory?
                    if(flags) //parse flags from user
                        ParseInventoryFlags(player, commands, flag);
                    else
                    {
                        player.DisplayInventory();
                    }
                    break;
                case "exit":
                    //Exit out of the game
                    //TODO: Is there a better way to exit the game?
                    Utils.ExitSequence(player, "finish");
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
                    return (true, "royalmarsh"); //TODO: This instantiates a map
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

        public static bool CombatCommandParse(Player player, Enemy enemy, string[] commands, string input, bool flags)
        {
            string flag = "";
            if (flags && commands.Length > 1)
                flag = commands[1];
            
            switch (input)
            {
                case "attack":
                    if(!flag.Equals(""))
                        ParseAttackFlags(player, enemy, flag);
                    GameManager.GameFuncs.BattlePhase(player, enemy); //Check if attack lands
                    Utils.Separator('-');
                    return true;
                    
                case "stats":
                    Console.WriteLine(player.ToString());
                    return true;
                    
                case "buffs":
                    player.DisplayBuffs();
                    return true;
                    
                case "inventory":
                    if(flags) //parse flags from user
                        ParseInventoryFlags(player, commands, flag);
                    else
                    {
                        player.DisplayInventory();
                    }
                    return true;
                
                default:
                    Console.WriteLine("Not a valid command!");
                    return false;
            }
        }

        private static void ParseInventoryFlags(Player player, string[] commands, string flag)
        {
            switch (flag)
            {
                case "-e":
                    Utils.Separator('-');
                    player.DisplayEquipment();
                    Utils.Separator('-');
                    break;
                case "-u":
                    player.TryUseConsumable(commands);
                    break;
                default:
                    Console.WriteLine("Not a valid flag!");
                    break;
            }
        }

        private static void ParseAttackFlags(Player player, Enemy enemy, string flag)
        {
            switch (flag)
            {
                case "-a":
                    Utils.Separator('*');
                    Console.WriteLine("You attempted to activate your Active ability!");
                    player.Active(enemy);
                    break;
                case "-u":
                    Console.WriteLine("You attempted to activate your ultimate ability!");
                    player.Ultimate(enemy);
                    break;
                default:
                    Console.WriteLine("Not a valid flag!");
                    break;
            }
        }
        
    }
}