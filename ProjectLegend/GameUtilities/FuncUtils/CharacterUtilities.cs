using System;
using ProjectLegend.CharacterClasses;
using ProjectLegend.GameUtilities.BuffUtilities;


namespace ProjectLegend.GameUtilities.FuncUtils
{
    public static class CharacterUtilities
    {
        public static void ProcessBuffs(Player player, Enemy enemy)
        {
            //Process player buffs
            if (player.Buffs.Count > 0)
            {
                for(int i = player.Buffs.Count - 1; i >=0; i--) //process buffs directly rather than making copy
                {
                    if(player.Buffs[i] is TurnBuff playerBuff)
                        playerBuff.ProcessTurnEffect(player);
                    else
                    {
                        player.Buffs[i].MinusOneTurn();
                    }
                    
                    if (player.Buffs[i].TurnsRemaining == 0) //Duration Check
                    {
                        player.Buffs[i].Applied = false;
                        Utils.Separator('-');
                        Console.WriteLine($"{player.Buffs[i].Name} has expired!");
                        
                        player.Buffs[i].Remove(player);
                    }
                }
            }

            if (enemy.Buffs.Count > 0)
            {
                Buff[] currentEnemyBuffs = enemy.Buffs.ToArray();
                foreach (var buff in currentEnemyBuffs)
                {
                    buff.MinusOneTurn();
                    
                    if (buff.TurnsRemaining == 0)
                    {
                        buff.Remove(enemy); //needs to be able to remove any buff, not just actives
                        buff.Applied = false;
                        Utils.Separator('-');
                        Console.WriteLine($"{buff.Name} has expired!");
                    }
                }
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

        public static bool AttackChance(this Player player, Enemy enemy) //player attacks enemy
        {
            var rand = new Random();
            double playerRoll = Math.Round(rand.NextDouble(), 2);
            if (player.Stunned)
                return false;
            else if (enemy.Invulnerable)
                return false;
            else if (playerRoll <= player.Accuracy) 
                return true;
            else
            {
                Console.WriteLine("Your attack missed the enemy!");
                return false;
            }
        }

        public static bool DefenseChance(this Player player, Enemy enemy) //enemy attacks player
        {
            var rand = new Random();
            double evade = Math.Round(rand.NextDouble(), 2);
            double enemyRoll = Math.Round(rand.NextDouble(), 2);

            if (player.Invulnerable) 
                return false;
            else if (enemy.Stunned)
                return false;
            else if (evade <= player.Evasion.Total)
            {
                Console.WriteLine("You evaded the enemies attack!");
                return false;
            }
            else if (enemyRoll <= enemy.Accuracy)
                return true;
            else
            {
                Console.WriteLine("The enemy missed their attack!");
                return false;
            }
        }
        
        public static void DisplayXpInfo(this Player player)
        {
            Console.WriteLine($"XP remaining: {player.Exp}/{player.ExpThresh}");
        }
    }
}