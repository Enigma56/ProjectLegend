using System;

using ProjectLegend.CharacterClasses;
using ProjectLegend.CharacterClasses.Legends;
using ProjectLegend.ItemClasses;
using ProjectLegend.GameUtilities.FaceUtils;

namespace ProjectLegend.GameUtilities.FuncUtils
{
    public static class UserQueries
    {
        public static readonly string[] PlayerLegends = {"Bangalore", "Bloodhound", "Gibraltar", "Lifeline", "Pathfinder", "Wraith"};
        
        public static Player CharacterSelection(string input)
        {
            switch (input)
            {
                //Offensive
                case "bangalore":
                    return new Bangalore();
                case "wraith":
                    return new Wraith();
                //Defensive
                case "gibraltar":
                    return new Gibraltar();
                //Support
                case "lifeline":
                    return new Lifeline();
                //Recon
                case "bloodhound":
                    return new Bloodhound();
                case "pathfinder":
                    return new Pathfinder();
                default:
                    Console.WriteLine("Not a valid character!");
                    break;
            }
            return null;
        }

        public static void GenGommandParse(Game game,Player player, string[] commands, string input, bool flags)
        {
            string flag = "";
            if (flags && commands.Length > 1)
                flag = commands[1];
            
            switch(input)
            {
                case "fight":
                    game.GameFuncs.FightEnemy(player);
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

        public static bool CombatCommandParse(GameFuncs gameFuncs, Player player, Enemy enemy,string[] commands, string input, bool flags)
        {
            string flag = "";
            if (flags && commands.Length > 1)
                flag = commands[1];
            
            bool parsed = false;
            switch (input)
            {
                case "attack":
                    ParseAttackFlags(player, enemy, flag);
                    gameFuncs.BattlePhase(player, enemy); //Check if attack lands
                    Utils.Separator('-');
                    parsed = true;
                    break;
                case "stats":
                    Console.WriteLine(player.ToString());
                    parsed = true;
                    break;
                case "buffs":
                    player.DisplayBuffs();
                    parsed = true;
                    break;
                case "inventory":
                    if(flags) //parse flags from user
                        ParseInventoryFlags(player, commands, flag);
                    else
                    {
                        player.DisplayInventory();
                    }
                    parsed = true;
                    break;
                    
                default:
                    Console.WriteLine("Not a valid command!");
                    break;
            }
            return parsed;
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