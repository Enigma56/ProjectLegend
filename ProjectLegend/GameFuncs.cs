using System;
using System.Collections.Generic;
using System.Linq;
using ProjectLegend.PlayerClasses;


namespace ProjectLegend
{
    public class GameFuncs
    {
        private readonly string[] _genCommands = {"fight", "help", "exit"};
        private readonly Dictionary<string, string> _commandInfo = new ();

        public GameFuncs()
        {
            _commandInfo.Add("fight", "Engage in a fight with an enemy");
            _commandInfo.Add("help", "Prints a list of all available commands");
            _commandInfo.Add("exit", "Exits the game");
        }
        public void ParseGeneralCommand(string[] commands, Player p)
         {
             string cmd = commands[0];
                   
                   switch(cmd)
                   {
                       case "fight":
                           FightEnemy(p);
                           break;
                       case "help":
                           Console.Write("Available commands are: ");
                           _genCommands.ArrayToString();
                           Console.WriteLine("Type a command after help to see more info on it!");
                           Utils.Separator();
                           
                           // if clause ?(then) ... :(else) ...
                           try
                           {
                               Console.WriteLine(_genCommands.Contains(commands[1])
                                   ? $"Command: {commands[1]}" + Environment.NewLine +$"Info: {_commandInfo[commands[1]]}"
                                   : "Command not in list!");
                           }
                           catch (IndexOutOfRangeException)
                           {
                               Console.WriteLine("No command was found! Please provide a command after \'help\'");
                           }

                           break;
                       case "exit":
                           //Exit out of the game
                           Utils.ExitSequence(p);
                           break;
                       default:
                           Console.WriteLine("Command not found!");
                           break;
                   }
               }

         private void ParseFightCommand(string[] commands, Player p, Enemy e)
         {
             string cmd = commands[0];
             switch (cmd)
             {
                 case "attack":
                     p.Health -= e.Attack;
                     e.Health -= p.Attack;
                     Utils.Separator();
                     Console.WriteLine("Remaining Stats:");
                     ViewHealth(p, e);
                     if (p.Health <= 0)
                     {
                         Console.WriteLine("You have passed away D:");
                         Utils.ExitSequence(p);
                     }
                     Utils.Separator();
                     break;
                 default:
                     Console.WriteLine("Not a valid command!");
                     break;
                     
             }
         }
         
         /// <summary>
         /// Choose your character from a predetermined List(Future implementation)
         /// !!!CURRENTLY IN TESTING!!!
         /// </summary>
         /// <returns>chosen player</returns>
         public Player ChooseCharacter()
         {
             Console.WriteLine("Choose your character! Type out full name to select");
             Console.WriteLine("Options: Bloodhound, Wraith");
             string chosenCharacter = Utils.ReadInput()[0];
             Player player = null;
             switch (chosenCharacter)
             {
                 case "bloodhound":
                     player = new Bloodhound();
                     break;
                 case "wraith":
                     player = new Wraith();
                     break;
                 default:
                     Console.WriteLine("Not a valid character!");
                     break;
             }
             return player;
         }

         private void FightEnemy(Player p)
         {
             var e = new Enemy();
             
             Utils.Separator();
             Console.WriteLine("Starting Stats:");
             ViewStats(p, e);
             Utils.Separator();
             
             bool fighting = true;
             Fight:
                 while (fighting)
                 {
                     if (e.Health <= 0)
                     {
                         DroppedExp(p, e);
                         p.DisplayXpInfo();
                         Utils.Separator();
                         Console.WriteLine("You killed the enemy!");
                         fighting = false;
                         goto Fight; //Immediately checks expression
                     }
                     Console.WriteLine("Your options are: attack");
                     string[] commands = Utils.ReadInput();

                     ParseFightCommand(commands, p, e);
                 }
             
             Console.WriteLine("Would you like to fight another enemy? Enter yes or no");
             Utils.Separator();
             string response = Utils.ReadInput()[0];
             if (response.Equals("yes"))
             {
                 e = RespawnEnemy();
                 fighting = true;
                 Utils.Separator();
                 Console.WriteLine("Re-Starting Stats:");
                 ViewStats(p, e);
                 Utils.Separator();
                 goto Fight;
             }
             else{Console.WriteLine("Exiting back to main loop!");}
             
         }
         
         /**
          * Respawns an enemy
          */
         private Enemy RespawnEnemy() //Fight an enemy and spawn another when alive one dies
         {
             var e = new Enemy();
             return e;
         }

         private void DroppedExp(Player p, Enemy e)
         {
             p.Exp += e.ExpDrop;
             if (p.Exp >= p.ExpThresh)
             {
                 p.AddLevel();
             }
         }
         
         public void PrintCommands()
         {
             _genCommands.ArrayToString();
         }

         private void ViewStats(Player p, Enemy e)
         {
             Console.WriteLine($"Your  Health: {p.Health}\tYour  Attack: {p.Attack}" + 
                               Environment.NewLine +
                               $"Enemy Health: {e.Health}\tEnemy Attack: {e.Attack}");
         }

         private void ViewHealth(Player p, Enemy e)
         {
             Console.WriteLine($"Your Health: {p.Health}" + 
                               Environment.NewLine +
                               $"Enemy Health: {e.Health}");
         }

    }
}