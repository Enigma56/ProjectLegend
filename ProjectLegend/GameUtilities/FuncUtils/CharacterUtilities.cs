using System;
using ProjectLegend.CharacterClasses;
using ProjectLegend.GameUtilities.BuffUtilities;


namespace ProjectLegend.GameUtilities.FuncUtils
{
    public static class CharacterUtilities
    {
        public static void ProcessBuffs(Enemy enemy)
        {
            //Process player buffs
            if (Player.Instance.Buffs.Count > 0)
            {
                for(int i = Player.Instance.Buffs.Count - 1; i >=0; i--) //process buffs directly rather than making copy
                {
                    if(Player.Instance.Buffs[i] is TurnBuff playerBuff)
                        playerBuff.ProcessTurnEffect(Player.Instance);
                    else
                    {
                        Player.Instance.Buffs[i].MinusOneTurn();
                    }
                    
                    if (Player.Instance.Buffs[i].TurnsRemaining == 0) //Duration Check
                    {
                        Player.Instance.Buffs[i].Applied = false;
                        Utils.Separator('-');
                        Console.WriteLine($"{Player.Instance.Buffs[i].Name} has expired!");
                        
                        Player.Instance.Buffs[i].Remove(Player.Instance);
                    }
                }
            }

            if (enemy.Buffs.Count <= 0) return;
            
            Buff[] currentEnemyBuffs = enemy.Buffs.ToArray();
            foreach (var buff in currentEnemyBuffs)
            {
                buff.MinusOneTurn();

                if (buff.TurnsRemaining != 0) continue;
                
                buff.Remove(enemy); //needs to be able to remove any buff, not just actives
                buff.Applied = false;
                Utils.Separator('-');
                Console.WriteLine($"{buff.Name} has expired!");
            }
        }
        
         public static void ApplyMultipleBuffs(this Character character, Buff[] buffs, int totalEnergyCost = 0)
                {
                    int costPerBuff = totalEnergyCost / buffs.Length;
                    if (buffs.Length > 0)
                    {
                        foreach (var buff in buffs)
                        {
                            buff.Apply(character, costPerBuff);
                        }
                    }
                }

         /// <summary>
         /// Extension method for player. Takes in an enemy and calculates the chance to hit that enemy based on
         /// random number generator.
         /// </summary>
         /// <param name="player"></param>
         /// <param name="enemy"></param>
         /// <returns></returns>
        public static bool AttackChance(this Player player, Enemy enemy) //player attacks enemy
        {
            var rand = new Random();
            double playerRoll = Math.Round(rand.NextDouble(), 2);
            
            //State checks
            if (player.Stunned)
                return false;
            if (enemy.Invulnerable)
                return false;
            if (playerRoll <= player.Accuracy) 
                return true;
           
            Console.WriteLine("Your attack missed the enemy!");
            return false;
            
        }

        public static bool DefenseChance(this Player player, Enemy enemy) //enemy attacks player
        {
            var rand = new Random();
            double evade = Math.Round(rand.NextDouble(), 2);
            double enemyRoll = Math.Round(rand.NextDouble(), 2);

            //State checls
            if (player.Invulnerable) 
                return false;
            if (enemy.Stunned)
                return false;
            if (evade <= player.Evasion.Total)
            {
                Console.WriteLine("You evaded the enemies attack!");
                return false;
            }
            if (enemyRoll <= enemy.Accuracy)
                return true;

            Console.WriteLine("The enemy missed their attack!");
            return false;
            
        }
        
        public static void DisplayXpInfo(this Player player)
        {
            Console.WriteLine($"XP remaining: {player.Exp}/{player.ExpThresh}");
        }
    }
}