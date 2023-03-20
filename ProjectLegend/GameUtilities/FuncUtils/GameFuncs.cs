using System;
using System.Collections.Generic;

using ProjectLegend.CharacterClasses;
using ProjectLegend.CharacterClasses.Legends;
using ProjectLegend.GameWorld;
using ProjectLegend.ItemClasses;
using ProjectLegend.ItemClasses.GearClasses;

namespace ProjectLegend.GameUtilities.FuncUtils
{
    //Responsible for All of the Game Functions that are needed to run the game
    public class GameFuncs
    {
        private readonly string[] _genCommands = {"select", "inventory", "help", "exit"};
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
        
        public void ParseGeneralChoice(World world, string[] commands, Player player)
         {
             string cmd = commands[0];
             bool flags = commands.Length > 1 && commands[1].StartsWith("-");
             UserQueries.GenGommandParse(world, player, commands, cmd, flags);
         }

         private void ParseCombatChoice(Player player, Enemy enemy)
         {
             bool validInput = false; // value is changed inside CombatCommandParse
             while (validInput is false)
             {
                 string[] commands = Utils.ReadInput(_combatCommands);
                 string cmd = commands[0];
                 bool flags = commands.Length > 1 && commands[1].StartsWith("-");
                 validInput = UserQueries.CombatCommandParse(player, enemy, commands, cmd, flags);
             }
         }

         public void PlaySelection(World world, string map, string location, Player player)
         {
             var instance = world.MapDict[map].LocationDict[location];
             EnemyClusterFight(instance, player);
         }

         public void ChooseMap(Player player) //Parameters dont needs to be used
         {
             void ChooseLocation(Player player1) //Parameters dont need to be used
             {
                 var locationChosen = (false, "");
                 while (locationChosen.Item1 == false)
                 {
                     string locationChoice = Utils.ReadInput(WorldUtils.LocationChoices)[0]; //stores decision
                     locationChosen = UserQueries.ParseLocationChoices(locationChoice); //checks for a parsed decision
                 }

                 GameManager.CurrentLocation = locationChosen.Item2;
             
                 Action<Player> locationSelection = ChooseLocation;
                 var locationSelectionNode = new LinkedListNode<Action<Player>>(locationSelection);
                 
                 //TODO: Issues with null pointers here
                 GameManager.BackPointers.AddAfter(GameManager.BackPointers.First.Next, locationSelectionNode);

                 if (locationChosen.Item2.Equals("back"))
                 {
                     Action<Player> stepBack = GameManager.BackPointers.First.Next.Value; //gets "2nd" value in linkedlist
                     GameManager.BackPointers.RemoveLast(); //removes locationSelection from linked list
                     stepBack(player);
                 }
             }
             
             var mapChosen = (false, "");
             while (mapChosen.Item1 == false)
             {
                 string mapChoice = Utils.ReadInput(WorldUtils.MapChoices)[0]; //stores decision
                 mapChosen = UserQueries.ParseMapChoices(mapChoice); //checks for a parsed decision
             }

             GameManager.CurrentMap = mapChosen.Item2;
             
             //Adds MapSelection to the LinkedList
             Action<Player> mapSelection = ChooseMap;
             var mapSelectionNode = new LinkedListNode<Action<Player>>(mapSelection);
             //TODO: error gets thrown here
             var firstPointer = GameManager.BackPointers.First;
             GameManager.BackPointers.AddAfter(GameManager.BackPointers.First, mapSelectionNode);
                 //will never be null bc this method wont get called until the game starts and the first node is added

                 if (mapChosen.Item2.Equals("back"))
                 {
                     Action<Player> stepBack = GameManager.BackPointers.First.Value;
                     GameManager.BackPointers.RemoveLast(); //removes map selection from linked list
                     stepBack(player); //executes general selection
                 }
             

             ChooseLocation(player);
         }


         private void EnemyClusterFight(Location location, Player player)
         {
             var locationClusters = location.Enemies;
             
             while (locationClusters.Count > 0)
             {
                 var enemies = locationClusters.Dequeue();
                 while (enemies.Cluster.Count > 0)
                 {
                     var currEnemy = enemies.Cluster.Dequeue();
                     FightEnemy(player, currEnemy);
                     
                     Console.WriteLine("You have {0} enemies remaining in this wave", enemies.Cluster.Count);
                 }
                 Console.WriteLine("You have {0} waves remaining", locationClusters.Count);
             }
             location.Close(player);
         }
         
         private void FightEnemy(Player player, Enemy enemy)
         {
             Console.WriteLine("Enemy type: {0}", enemy);
             Utils.Separator('-');
             Console.WriteLine("Starting Stats:");
             ViewStats(player, enemy);
             Utils.Separator('-');
             Fighting = true;
             while (Fighting)
             {
                 if (enemy.Dead)
                 {
                     Fighting = false;
                     break;
                 }
                 ParseCombatChoice(player, enemy);
                 
                 if (player is Lifeline medic) // Convert this to per-turn 
                     medic.PassiveHeal();
                 if (player.Energy.Current >= player.ActiveCost)
                     Console.WriteLine("You have enough energy for your active ability!");
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
             Utils.DropItem(GameManager.CommonGear).AddOrDiscard(player); //Must take in GearPool type as parameter
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