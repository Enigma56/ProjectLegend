using System;
using System.Collections.Generic;
using System.Linq;
using ProjectLegend.CharacterClasses;


namespace ProjectLegend
{
    public class GameFuncs
    {
        private readonly string[] _genCommands = {"fight", "help", "exit"};
        private readonly string[] _combatCommands = {"attack", "stats"};
        private readonly string[] _flags = { "-a", "-u" };
        
        private readonly string[] _playerLegends = {"Bangalore", "Wraith"};
        private readonly string[] _yesNo = {"yes", "no"};
        private readonly Dictionary<string, string> _commandInfo = new ();

        public GameFuncs()
        {
            _commandInfo.Add("fight", "Engage in a fight with an enemy");
            _commandInfo.Add("help", "Prints a list of all available commands");
            _commandInfo.Add("exit", "Exits the game");
        }
        
        public Player ChooseCharacter()
        {
            Console.WriteLine("Choose your character! Type out full name to select");
            Player player = null;
            while (player is null)
            {
                string chosenCharacter = Utils.ReadInput(PlayerLegends())[0];
                switch (chosenCharacter)
                {
                    //Offensive
                    case "bangalore":
                        player = new Bangalore();
                        break;
                    case "wraith":
                        player = new Wraith();
                        break;
                    //Defensive
                    //Suport
                    //Recon
                    default:
                        Console.WriteLine("Not a valid character!");
                        break;
                }
            }
            return player;
        }
        
        public void ParseGeneralCommand(string[] commands, Player player)
         {
             string cmd = commands[0];
             switch(cmd)
                   {
                       case "fight":
                           FightEnemy(player);
                           break;
                       case "help":
                           Console.Write("Available commands are: ");
                           Utils.ToString(_genCommands);
                           Console.WriteLine("Type a command after help to see more info on it!");
                           Utils.Separator();
                           
                           // if clause ?(then) ... :(else) ...
                           try
                           {
                               Console.WriteLine(_genCommands.Contains(commands[1])
                                   ? $"Command: {commands[1]}" + Environment.NewLine +$"Info: {_commandInfo[commands[1]]}"
                                   : "Command not in list!");
                               Utils.Separator();
                           }
                           catch (IndexOutOfRangeException)
                           {
                               Console.WriteLine("No command was found! Please provide a command after \'help\'");
                           }

                           break;
                       case "exit":
                           //Exit out of the game
                           Utils.ExitSequence(player);
                           break;
                       default:
                           Console.WriteLine("Command not found!");
                           break;
                   }
               }

         private void ParseCombatCommand(string[] commands, Player player, Enemy enemy)
         {
             string cmd = commands[0];
             switch (cmd)
             {
                 case "attack":
                     //activate abilities
                     if (commands.Length > 1 && _flags.Contains(commands[1]))
                     {
                         switch (commands[1])
                         {
                             case "-a":
                                 Console.WriteLine("entered -a");
                                 break;
                             case "-u":
                                 Console.WriteLine("entered -u");
                                 break;
                         }
                     }
                     //Check if attack lands
                     BattlePhase(player, enemy);
                     Utils.Separator();
                     Console.WriteLine("Remaining Stats:");
                     ViewHealth(player, enemy);
                     Utils.Separator();
                     break;
                 case "stats":
                     Console.WriteLine(player.ToString());
                     break;
                 case "buffs":
                     player.DisplayBuffs();
                     break;
                 default:
                     Console.WriteLine("Not a valid command!");
                     break;
             }
         }

         private void FightEnemy(Player player)
         {
             var enemy = new Enemy();

             Utils.Separator();
             Console.WriteLine("Starting Stats:");
             ViewStats(player, enemy);
             Utils.Separator();
             bool fighting = true;
             Fight:
                 while (fighting)
                 {
                     if (enemy.Dead)
                     {
                         fighting = false;
                         goto Fight; //Immediately checks expression
                     }
                     string[] commands = Utils.ReadInput(CombatCommands());
                     ParseCombatCommand(commands, player, enemy);
                 }

                 Console.WriteLine("Would you like to fight another enemy? Enter yes or no");
                 Utils.Separator();
             Response:
                 string response = Utils.ReadInput(_yesNo)[0];
                 if (Equals(response, "yes"))
                 {
                     enemy = RespawnEnemy();
                     fighting = true;
                     Utils.Separator();
                     Console.WriteLine("Re-Starting Stats:");
                     ViewStats(player, enemy);
                     Utils.Separator();
                     goto Fight;
                 }
                 else if ( !Equals(response, "yes") && !Equals(response, "no"))
                 {
                     Console.WriteLine("Please type yes or no!");
                     goto Response;
                 }
                 else { Console.WriteLine("Exiting back to main loop!"); }
         }

         private void BattlePhase(Player player, Enemy enemy) //Processes attack and defense
         {
             void AttackPhase() //Player attack
             {
                 bool attackEnemy = player.AttackChance();
                 if (attackEnemy is true)
                 {
                     enemy.CurrentHealth -= player.CurrentAttack;
                 }
             }

             bool CheckEnemyDeath()
             {
                 bool enemyIsDead = enemy.CurrentHealth <= 0;
                 if (enemyIsDead) enemy.Dead = true;
                 return enemyIsDead;
             }

             void DefensePhase() //Player defense (enemy attack)
             {
                 bool attackPlayer = player.DefenseChance(enemy);
                 if (attackPlayer is true)
                 {
                     player.CurrentHealth -= enemy.CurrentAttack;
                 }
             }

             bool CheckPlayerDeath()
             {
                 bool playerIsDead = player.CurrentHealth <= 0;
                 return playerIsDead;
             }

             void EndPhase() //end of combat phase
             {
                 CharacterUtilities.ProcessBuffs(player, enemy);
                 if(player.CurrentEnergy < player.MaxEnergy) 
                     player.CurrentEnergy += player.EnergyPerTurn;
                 else
                 {
                     Console.WriteLine("Your energy is full! Use abilities your abilities!");
                 }
             }

             AttackPhase();
             
             bool enemyDeath = CheckEnemyDeath();
             if ( enemyDeath )
             {
                 EndPhase();
                 Utils.Separator();
                 Console.WriteLine("You killed the enemy!");
                 Utils.Separator();
                 DroppedExp(player, enemy);
                 player.DisplayXpInfo();
             }
             else
             {
                 DefensePhase();
                 
                 bool playerDeath = CheckPlayerDeath();
                 if( playerDeath ) Utils.ExitSequence(player);
                 else
                 {
                     EndPhase();
                 }
             }
         }

         /**
          * Respawns an enemy
          */
         private Enemy RespawnEnemy() //Fight an enemy and spawn another when alive one dies
         {
             var e = new Enemy();
             return e;
         }

         private void DroppedExp(Player player, Enemy enemy)
         {
             player.Exp += enemy.ExpDrop;
             if (player.Exp >= player.ExpThresh)
             {
                 player.AddLevel();
             }
         }
         
         public void PrintCommands()
         {
             Utils.ToString(_genCommands);
         }

         private void ViewStats(Player player, Enemy enemy)
         {
             string playerStats = $"Your  Health: {player.CurrentHealth,-6}Your  Attack: {player.CurrentAttack}";
             string enemyStats = $"Enemy Health: {enemy.CurrentHealth,-6}Enemy Attack: {enemy.CurrentAttack}";
             Console.WriteLine(playerStats + 
                               Environment.NewLine +
                               enemyStats);
         }

         private void ViewHealth(Player player, Enemy enemy)
         {
             Console.WriteLine($"Your  Health: {player.CurrentHealth}" + 
                               Environment.NewLine +
                               $"Enemy Health: {enemy.CurrentHealth}");
         }

         public string[] GenCommands()
         {
             return _genCommands;
         }

         public string[] CombatCommands()
         {
             return _combatCommands;
         }

         public string[] PlayerLegends()
         {
             return _playerLegends;
         }

    }
}