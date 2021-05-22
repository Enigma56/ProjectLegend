using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using ProjectLegend.PlayerClasses;


namespace ProjectLegend
{
    public class GameFuncs
    {
        private readonly string[] _genCommands = {"fight", "help", "exit"};
        private readonly Dictionary<string, string> _commandInfo = new ();
        private const string _separator = "--------------------------";

        public GameFuncs()
        {
            _commandInfo.Add("fight", "Engage in a fight with an enemy");
            _commandInfo.Add("help", "Prints a list of all available commands");
            _commandInfo.Add("exit", "Exits the game");
        }
        public void ParseGeneralCommand(Game g, string[] commands, Player p)
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
                           Separator();

                           if (_genCommands.Contains(commands[1]))
                           {
                               // retrieve command
                               Console.WriteLine($"Command: {commands[1]}\nInfo: {_commandInfo[commands[1]]}");
                           }
                           else
                           {
                               Console.WriteLine("Command not in list!");
                           }
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
                     Separator();
                     Console.WriteLine("Remaining Stats:");
                     Console.WriteLine($"Your Health: {p.Health}\n" +
                                       $"Enemy Health: {e.Health}");
                     Separator();
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
             // Eventually have an array of choices
             
             var blood = new Bloodhound();
             return blood;
         }

         private void FightEnemy(Player p)
         {
             var e = new Enemy();
             Separator();
             Console.WriteLine("Starting Stats:");
             Console.WriteLine($"Your Health: {p.Health}\tYour Attack: {p.Attack}\n" +
                               $"Enemy Health: {e.Health}\tEnemy Attack: {e.Attack}");
             Separator();
             
             Console.WriteLine("Your options are: attack");
             bool fighting = true;
             Fight:
                 while (fighting)
                 {
                     if (e.Health <= 0)
                     {
                         DroppedExp(p, e);
                         Console.WriteLine("You killed the enemy!");
                         fighting = false;
                         goto Fight; //Immediately checks expression
                     }

                     string[] commands = Utils.ReadInput();

                     ParseFightCommand(commands, p, e);
                 }
             
             Console.WriteLine("Would you like to fight another enemy? Enter yes or no");
             Separator();
             string response = Utils.ReadInput()[0];
             if (response.Equals("yes"))
             {
                 e = RespawnEnemy();
                 fighting = true;
                 Separator();
                 Console.WriteLine("Re-Starting Stats:");
                 Console.WriteLine($"Your Health: {p.Health}\tYour Attack: {p.Attack}\n" +
                                   $"Enemy Health: {e.Health}\tEnemy Attack: {e.Attack}");
                 Separator();
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
                 Console.WriteLine($"You Leveled Up!\nCurrent level: {p.Level}");
             }
         }
         
         public void PrintCommands()
         {
             _genCommands.ArrayToString();
         }

         public void Separator()
         {
             Console.WriteLine(_separator);
         }

    }
}