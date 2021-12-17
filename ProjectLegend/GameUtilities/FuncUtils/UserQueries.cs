using System;
using System.Security.Cryptography.X509Certificates;
using ProjectLegend.CharacterClasses;
using ProjectLegend.CharacterClasses.Legends;
using ProjectLegend.ItemClasses;
using ProjectLegend.GameUtilities.FaceUtils;
using ProjectLegend.World;
using ProjectLegend.World.RoyalMarsh;

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
        
        public static void GenGommandParse(Game game, Player player, string[] commands, string input, bool flags)
        {
            string flag = "";
            if (flags && commands.Length > 1)
                flag = commands[1];
            
            switch(input)
            {
                case "select": //"select"
                    game.GameFuncs.FightEnemy(player); //TODO: change to "maps"
                    //
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
                    game.Running = false;
                    Utils.ExitSequence(player, "finish");
                    break;
                default:
                    Console.WriteLine("Command not found!");
                    break;
            }
        }

        public static bool CombatCommandParse(GameFuncs gameFuncs, Player player, Enemy enemy, string[] commands, string input, bool flags)
        {
            string flag = "";
            if (flags && commands.Length > 1)
                flag = commands[1];
            
            switch (input)
            {
                case "attack":
                    ParseAttackFlags(player, enemy, flag);
                    gameFuncs.BattlePhase(player, enemy); //Check if attack lands
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
        
        public static bool ParseMapChoices(string input)
        {

            switch (input)
            {
                case "rm":
                    //display locations to choose from and their status
                    return true; //TODO: This instantiates a map
                case "back":
                    //Take them back to main choice option
                    return true; //take the player back one step; Make sure null does not conflict
                default:
                    Console.WriteLine("Not a valid map!");
                    return false;
            }
            
        }

        public static bool ParseLocationChoices(string input)
        {
            switch (input)
            {
                case "caves":
                    //Begin a caves instance and run!
                    return true;
                case "back":
                    
                    return true;
                default:
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