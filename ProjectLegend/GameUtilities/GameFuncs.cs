using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using ProjectLegend.CharacterClasses;
using ProjectLegend.CharacterClasses.Legends;
using ProjectLegend.ItemClasses;
using ProjectLegend.ItemClasses.Consumables;


namespace ProjectLegend.GameUtilities
{
    public class GameFuncs
    {
        private readonly string[] _genCommands = {"fight", "inventory", "help", "exit"};
        private readonly string[] _combatCommands = {"attack", "buffs", "stats", "inventory"};
        private readonly string[] _flags = { "-a", "-u" };
        
        private readonly string[] _playerLegends = {"Bangalore", "Bloodhound", "Gibraltar", "Lifeline", "Pathfinder", "Wraith"};
        private readonly string[] _yesNo = {"yes", "no"};
        private readonly Dictionary<string, string> _commandInfo = new ();
        
        private bool Fighting { get; set; }

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
                string chosenCharacter = Utils.ReadInput(_playerLegends)[0];
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
                    case "gibraltar":
                        player = new Gibraltar();
                        break;
                    //Support
                    case "lifeline":
                        player = new Lifeline();
                        break;
                    //Recon
                    case "bloodhound":
                        player = new Bloodhound();
                        break;
                    case "pathfinder":
                        player = new Pathfinder();
                        break;
                    default:
                        Console.WriteLine("Not a valid character!");
                        break;
                }
            }
            return player;
        }
        
        public void ParseGeneralCommand(Game game, string[] commands, Player player)
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
                           Utils.Separator('-');
                           
                           // if clause ?(then) ... :(else) ...
                           try
                           {
                               Console.WriteLine(_genCommands.Contains(commands[1])
                                   ? $"Command: {commands[1]}" + Environment.NewLine +$"Info: {_commandInfo[commands[1]]}"
                                   : "Command not in list!");
                               Utils.Separator('-');
                           }
                           catch (IndexOutOfRangeException)
                           {
                               Console.WriteLine("No command was found! Please provide a command after \'help\'");
                           }
                           break;
                       case "inventory":
                           player.DisplayInventory();
                           player.TryUseConsumable(commands);
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

         private void ParseCombatCommand(Player player, Enemy enemy)
         {
             bool validInput = false;
             while (validInput is false)
             {
                 string[] commands = Utils.ReadInput(_combatCommands);
                 string cmd = commands[0];
                 switch (cmd)
                 {
                     case "attack":
                         validInput = true;
                         Console.WriteLine(
                             "Use flags -a and -u after \"attack\" to use active and ultimate abilities when you have enough energy!");
                         if (commands.Length > 1 && _flags.Contains(commands[1]))
                         {
                             switch (commands[1])
                             {
                                 case "-a":
                                     Utils.Separator('*');
                                     Console.WriteLine("You activated your Active ability!");
                                     player.Active(enemy);
                                     break;
                                 case "-u":
                                     Console.WriteLine("You activated your ultimate ability!");
                                     player.Ultimate(enemy);
                                     break;
                             }
                         }
                         BattlePhase(player, enemy); //Check if attack lands
                         Utils.Separator('-');
                         break;
                     case "stats":
                         validInput = true;
                         Console.WriteLine(player.ToString());
                         break;
                     case "buffs":
                         validInput = true;
                         player.DisplayBuffs();
                         break;
                     case "inventory":
                         validInput = true;
                         player.DisplayInventory();
                         player.TryUseConsumable(commands);
                         break;
                     default:
                         Console.WriteLine("Not a valid command!");
                         break;
                 }
             }
         }

         private void FightEnemy(Player player)
         {
             var enemy = new Enemy();

             Utils.Separator('-');
             Console.WriteLine("Starting Stats:");
             ViewStats(player, enemy);
             Utils.Separator('-');
             Fighting = true;
             Fight:
                 while (Fighting)
                 {
                     if (enemy.Dead)
                     {
                         Fighting = false;
                         goto Fight; //Immediately checks expression
                     }
                     ParseCombatCommand(player, enemy);
                     
                     if (player is Lifeline lifeline) //only activates after input is finished being processed
                         lifeline.PassiveHeal();
                 }

             Response:
                if (player.Dead == false)
                {
                     Console.WriteLine("Would you like to fight another enemy?");
                     Utils.Separator('-');
                     string response = Utils.ReadInput(_yesNo)[0];
                     if (Equals(response, "yes"))
                     {
                         enemy = RespawnEnemy();
                         Fighting = true;
                         Utils.Separator('-');
                         Console.WriteLine("Re-Starting Stats:");
                         ViewStats(player, enemy);
                         Utils.Separator('-');
                         goto Fight;
                     }
                     else if (!Equals(response, "yes") && !Equals(response, "no"))
                     {
                         Console.WriteLine("Please type yes or no!");
                         goto Response;
                     }
                     else
                     {
                         Utils.Separator('-');
                         Console.WriteLine("Exiting back to main loop!");
                     }
                }
         }

         private void BattlePhase(Player player, Enemy enemy) //Processes attack and defense
         {
             void AttackPhase() //Player attack
             {
                 bool attackEnemy = player.AttackChance(enemy);
                 if (attackEnemy is true)
                 {
                     enemy.CurrentHealth -= player.CurrentAttack;
                 }
             }

             bool CheckEnemyDeath()
             {
                 bool enemyIsDead = enemy.CurrentHealth <= 0 || enemy.Dead; //check left then right
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
                 player.Dead = player.CurrentHealth <= 0;
                 return player.Dead;
             }

             void EndPhase() //end of combat phase
             {
                 CharacterUtilities.ProcessBuffs(player, enemy);
                 Utils.Separator('-');
                 Console.WriteLine("Remaining Stats:");
                 ViewHealth(player, enemy);

                 if (player.CurrentEnergy < player.MaxEnergy)
                 {
                     player.CurrentEnergy += player.EnergyPerTurn;
                     Console.WriteLine(Environment.NewLine + $"You gained {player.EnergyPerTurn} energy!");
                 }
                 
                 else
                 {
                     Utils.Separator('-');
                     Console.WriteLine("Your energy is full! Use abilities your abilities!");
                 }
             }
             bool enemyDeath = CheckEnemyDeath();
             if (enemyDeath) { } //a basic way to do nothing if the enemy is already dead from an ability
             else{ AttackPhase(); } 
             
             enemyDeath = CheckEnemyDeath(); //I want a fix for this redundancy
             if ( enemyDeath )
             {
                 EndPhase();
                 Utils.Separator('-');
                 Console.WriteLine("You killed the enemy!");
                 Utils.Separator('-');
                 PlayerDrops(player, enemy);
             }
             else
             {
                 DefensePhase();
                 
                 bool playerDeath = CheckPlayerDeath();
                 if( playerDeath )
                 {
                     Fighting = false;
                     Utils.ExitSequence(player, "death"); //TODO: send player back to main menu with refreshed(not reset) stats on death
                     player.DeathCount += 1;
                 }
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

         private void PlayerDrops(Player player, Enemy enemy)
         {
             void ExpDrop()
             {
                 player.Exp += enemy.ExpDrop;
                 if (player.Exp >= player.ExpThresh)
                 {
                     player.AddLevel();
                 }
                 player.DisplayXpInfo();
             }

             void ItemDrop()
             {
                 var rollGenerator = new Random();
                 double itemRoll = Math.Round(rollGenerator.NextDouble(), 2);
                 Item enemyDrop = enemy.GetDrop();
                 if (itemRoll < enemyDrop.DropRate)
                 {
                     enemyDrop.AddOrDiscard(player);
                 }
             }
             ExpDrop();
             ItemDrop();
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
    }
}