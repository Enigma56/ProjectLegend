using System;
using System.Collections.Generic;
using System.Linq;

using ProjectLegend.CharacterClasses;
using ProjectLegend.CharacterClasses.Enemies;
using ProjectLegend.CharacterClasses.Legends;
using ProjectLegend.ItemClasses;
using ProjectLegend.ItemClasses.GearClasses;
using ProjectLegend.World;

namespace ProjectLegend.GameUtilities.FuncUtils
{
    public class GameFuncs
    {
        private readonly string[] _genCommands = {"fight", "inventory", "help", "exit"};
        private readonly string[] _combatCommands = {"attack", "buffs", "stats", "inventory"};

        private bool Fighting { get; set; }

        public Player ChooseCharacter()
        {
            Console.WriteLine("Choose your character! Type out full name to select");
            Player player = null;
            while (player is null)
            {
                string chosenCharacter = Utils.ReadInput(UserQueries.PlayerLegends)[0];
                player = UserQueries.CharacterSelection(chosenCharacter);
            }
            return player;
        }
        
        public void ParseGeneralCommand(Game game, string[] commands, Player player)
         {
             string cmd = commands[0];
             bool flags = commands.Length > 1 && commands[1].StartsWith("-");
             UserQueries.GenGommandParse(game, player, commands, cmd, flags);
         }

         private void ParseCombatCommand(Player player, Enemy enemy)
         {
             bool validInput = false; // value is changed inside CombatCommandParse
             while (validInput is false)
             {
                 string[] commands = Utils.ReadInput(_combatCommands);
                 string cmd = commands[0];
                 bool flags = commands.Length > 1 && commands[1].StartsWith("-");
                 validInput = UserQueries.CombatCommandParse(this, player, enemy, commands, cmd, flags);
             }
         }

         public void ChooseMap(Dictionary<string, Map> mapDict, string[] mapChoices)
         {
             bool chosen = false;
             while (chosen is false)
             {
                 
                 //somehow get out of the loop
                 string choice = Utils.ReadInput(mapChoices)[0];
             }
         }

         //TODO: Refactor and move to cluster
         //Refactor to choose map and then fight enemy clusters
         public void FightEnemy(Player player)
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
                         break;
                     }
                     ParseCombatCommand(player, enemy);
                     
                     if (player is MinuteMedic medic) // Convert this to per-turn 
                         medic.PassiveHeal();
                     if (player.Energy.Current >= player.ActiveCost)
                         Console.WriteLine("You have enough energy for your active ability!");
                 }
                 
            if (player.Dead == false)
            {
                 Console.WriteLine("Would you like to fight another enemy?");
                 Utils.Separator('-');
                 bool continueToFight = Utils.YesOrNo();
                 if (continueToFight)
                 {
                     Utils.Separator('-');
                     Console.WriteLine("Starting Stats:");
                     enemy = RespawnEnemy();
                     ViewStats(player, enemy);
                     Utils.Separator('-');
                     Fighting = true;
                     goto Fight;
                 }
                 else
                 {
                     Utils.Separator('-');
                     Console.WriteLine("Exiting back to main loop!");
                 }
            }
         }

         public void BattlePhase(Player player, Enemy enemy) //Processes attack and defense
         {
             void AttackPhase() //Player attack
             {
                 bool attackEnemy = player.AttackChance(enemy);
                 if (attackEnemy is true)
                 {
                     enemy.Health.Current -= player.Attack.Current;
                 }
             }

             bool CheckEnemyDeath()
             {
                 bool enemyIsDead = enemy.Health.Current <= 0 || enemy.Dead; //check left then right
                 if (enemyIsDead) enemy.Dead = true;
                 return enemyIsDead;
             }

             void DefensePhase() //Player defense (enemy attack)
             {
                 bool attackPlayer = player.DefenseChance(enemy);
                 if (attackPlayer is true)
                 {
                     player.Health.Current -= enemy.Attack.Current;
                 }
             }

             bool CheckPlayerDeath()
             {
                 player.Dead = player.Health.Current <= 0;
                 return player.Dead;
             }

             void EndPhase() //end of combat phase
             {
                 CharacterUtilities.ProcessBuffs(player, enemy);
                 Utils.Separator('-');
                 Console.WriteLine("Remaining Stats:");
                 ViewHealth(player, enemy);

                 if (player.Energy.Current < player.Energy.Max)
                 {
                     player.Energy.Current += Energy.EnergyPerTurn;
                     Console.WriteLine(Environment.NewLine + $"You gained {Energy.EnergyPerTurn} energy!");
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
                     Utils.ExitSequence(player, "death");
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
             string playerStats = $"Your  Health: {player.Health.Current,-6}Your  Attack: {player.Attack.Current}";
             string enemyStats = $"Enemy Health: {enemy.Health.Current,-6}Enemy Attack: {enemy.Attack.Current}";
             Console.WriteLine(playerStats + 
                               Environment.NewLine +
                               enemyStats);
         }

         private void ViewHealth(Player player, Enemy enemy)
         {
             Console.WriteLine($"Your  Health: {player.Health.Current}" + 
                               Environment.NewLine +
                               $"Enemy Health: {enemy.Health.Current}");
         }

         public string[] GenCommands()
         {
             return _genCommands;
         }
    }
}