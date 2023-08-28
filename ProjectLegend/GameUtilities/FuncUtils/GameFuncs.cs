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

        public void ChooseCharacter()
        {
            Console.WriteLine("Choose your character! Type out full name to select");
            bool legendChosen = false;
            while (legendChosen is false)
            {
                string chosenCharacter = Utils.ReadInput(UserQueries.PlayerLegends)[0];
                legendChosen = UserQueries.CharacterSelection(chosenCharacter);
            }
        }
        
        public void ParseGeneralChoice(World world, string[] commands)
         {
             string cmd = commands[0];
             bool flags = commands.Length > 1 && commands[1].StartsWith("-");
             UserQueries.GenGommandParse(world, commands, cmd, flags);
         }

         private void ParseCombatChoice(Enemy enemy)
         {
             bool validInput = false; // value is changed inside CombatCommandParse
             while (validInput is false)
             {
                 string[] commands = Utils.ReadInput(_combatCommands);
                 string cmd = commands[0];
                 bool flags = commands.Length > 1 && commands[1].StartsWith("-");
                 validInput = UserQueries.CombatCommandParse(enemy, commands, cmd, flags);
             }
         }

         public void PlaySelection(World world, string map, string location)
         {
             var instance = world.MapDict[map].LocationDict[location];
             EnemyClusterFight(instance);
         }

         public void ChooseMap() //Parameters dont needs to be used
         {
             void ChooseLocation() //Parameters dont need to be used
             {
                 var locationChosen = (false, "");
                 while (locationChosen.Item1 == false)
                 {
                     string locationChoice = Utils.ReadInput(WorldUtils.LocationChoices)[0]; //stores decision
                     locationChosen = UserQueries.ParseLocationChoices(locationChoice); //checks for a parsed decision
                 }

                 GameManager.CurrentLocation = locationChosen.Item2;
                 
                 Action locationSelection = ChooseLocation;
                 var locationSelectionNode = new LinkedListNode<Action>(locationSelection);
                 GameManager.BackPointers.AddAfter(GameManager.BackPointers.First.Next, locationSelectionNode);
                 if (locationChosen.Item2.Equals("back"))
                 {
                     Action stepBack = GameManager.BackPointers.First.Next.Value; //gets "2nd" value in linkedlist
                     GameManager.BackPointers.RemoveLast(); //removes locationSelection from linked list
                     stepBack();
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
             Action mapSelection = ChooseMap;
             var mapSelectionNode = new LinkedListNode<Action>(mapSelection);
             GameManager.BackPointers.AddAfter(GameManager.BackPointers.First, mapSelectionNode);
                 //will never be null bc this method wont get called until the game starts and the first node is added

                 if (mapChosen.Item2.Equals("back"))
                 {
                     Action stepBack = GameManager.BackPointers.First.Value;
                     GameManager.BackPointers.RemoveLast(); //removes map selection from linked list
                     stepBack(); //executes general selection
                 }
             

             ChooseLocation();
         }


         private void EnemyClusterFight(Location location)
         {
             var locationClusters = location.Enemies;
             
             while (locationClusters.Count > 0)
             {
                 var enemies = locationClusters.Dequeue();
                 while (enemies.Cluster.Count > 0)
                 {
                     var currEnemy = enemies.Cluster.Dequeue();
                     FightEnemy(currEnemy);
                     
                     Console.WriteLine("You have {0} enemies remaining in this wave", enemies.Cluster.Count);
                 }
                 Console.WriteLine("You have {0} waves remaining", locationClusters.Count);
             }
             location.Close();
         }
         
         private void FightEnemy(Enemy enemy)
         {
             Console.WriteLine("Enemy type: {0}", enemy);
             Utils.Separator('-');
             Console.WriteLine("Starting Stats:");
             ViewStats(enemy);
             Utils.Separator('-');
             Fighting = true;
             while (Fighting)
             {
                 if (enemy.Dead)
                 {
                     Fighting = false;
                     break;
                 }
                 ParseCombatChoice(enemy);
                 
                 /*if (player is Lifeline medic) // Convert this to per-turn 
                     medic.PassiveHeal();*/
                 if (Player.Instance.Energy.Current >= Player.Instance.ActiveCost)
                     Console.WriteLine("You have enough energy for your active ability!");
             }
         }

         public void BattlePhase(Enemy enemy) //Processes attack and defense
         {
             void AttackPhase() //Player attack
             {
                 bool attackEnemy = Player.Instance.AttackChance(enemy);
                 if (attackEnemy)
                 {
                     enemy.Health.Current -= Player.Instance.Attack.Current;
                 }
             }

             bool CheckEnemyDeath()
             {
                 bool enemyIsDead = enemy.Health.Current <= 0 || enemy.Dead; //check left then right
                 if (enemyIsDead) enemy.Dead = true; //redundancy in setting Dead state of enemy
                 return enemyIsDead;
             }

             void DefensePhase() //Player defense (enemy attack)
             {
                 bool attackPlayer = Player.Instance.DefenseChance(enemy);
                 if (attackPlayer is true)
                 {
                     Player.Instance.Health.Current -= enemy.Attack.Current;
                 }
             }

             bool CheckPlayerDeath()
             {
                 Player.Instance.Dead = Player.Instance.Health.Current <= 0;
                 return Player.Instance.Dead;
             }

             void EndPhase() //end of combat phase
             {
                 CharacterUtilities.ProcessBuffs(enemy);
                 Utils.Separator('-');
                 Console.WriteLine("Remaining Stats:");
                 ViewHealth(enemy);

                 if (Player.Instance.Energy.Current < Player.Instance.Energy.Max)
                 {
                     Player.Instance.Energy.Current += Energy.EnergyPerTurn;
                     Console.WriteLine(Environment.NewLine + $"You gained {Energy.EnergyPerTurn} energy!");
                 }
                 
                 else
                 {
                     Utils.Separator('-');
                     Console.WriteLine("Your energy is full! Use abilities your abilities!");
                 }
             }
             
             bool enemyDeath = CheckEnemyDeath();
             if (enemyDeath is false)
             {
                 AttackPhase(); 
             } 
             
             enemyDeath = CheckEnemyDeath(); //Checks enemy death again after attack phase to see if they died or not
             if (enemyDeath)
             {
                 EndPhase();
                 Utils.Separator('-');
                 Console.WriteLine("You killed the enemy!");
                 Utils.Separator('-');
                 PlayerDrops(enemy);
             }
             else //Player defends
             {
                 DefensePhase();
                 
                 bool playerDeath = CheckPlayerDeath();
                 if( playerDeath )
                 {
                     Fighting = false;
                     Utils.ExitSequence("death");
                     Player.Instance.DeathCount += 1;
                 }
                 else
                 {
                     EndPhase();
                 }
             }
         }

         private void PlayerDrops(Enemy enemy)
         {
             void ExpDrop()
             {
                 Player.Instance.Exp += enemy.ExpDrop;
                 if (Player.Instance.Exp >= Player.Instance.ExpThresh)
                 {
                     Player.Instance.AddLevel();
                 }
                 Player.Instance.DisplayXpInfo();
             }

             void ItemDrop()
             {
                 var rollGenerator = new Random();
                 double itemRoll = Math.Round(rollGenerator.NextDouble(), 2);
                 Item enemyDrop = enemy.GetDrop();
                 if (itemRoll < enemyDrop.DropRate)
                 {
                     enemyDrop.AddOrDiscard();
                 }
             }
             
             ExpDrop();
             ItemDrop();
             Utils.DropItem(GameManager.CommonGear).AddOrDiscard(); //Must take in GearPool type as parameter
         }

         private void ViewStats(Enemy enemy)
         {
             string playerStats = $"Your  Health: {Player.Instance.Health.Current,-6}Your  Attack: {Player.Instance.Attack.Current}";
             string enemyStats = $"Enemy Health: {enemy.Health.Current,-6}Enemy Attack: {enemy.Attack.Current}";
             Console.WriteLine(playerStats + 
                               Environment.NewLine +
                               enemyStats);
         }

         private void ViewHealth(Enemy enemy)
         {
             Console.WriteLine($"Your  Health: {Player.Instance.Health.Current}" + 
                               Environment.NewLine +
                               $"Enemy Health: {enemy.Health.Current}");
         }

         public string[] GenCommands()
         {
             return _genCommands;
         }
    }
}