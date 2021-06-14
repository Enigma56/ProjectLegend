using System;
using ProjectLegend.CharacterClasses;
using System.Linq;

namespace ProjectLegend
{
    public static class CharacterUtilities
    {
        public static void AddLevel(this Player player)
        {
            void LevelUpdate()
            { 
                int xp = player.Exp;
                int oldLevel = player.Level;
                
                player.Exp = 0;
                player.Exp += (xp %player.ExpThresh);
                player.Level++;
                int oldExpThresh =player.ExpThresh;
                int thresholdIncrease = (int) Math.Ceiling(Math.Pow(player.Level, 2) * Math.Log10(player.Level));
                player.ExpThresh += thresholdIncrease;

                Console.WriteLine("You Leveled Up!" + Environment.NewLine + 
                                  $"Current level: {oldLevel} --> {player.Level}" + Environment.NewLine + 
                                  $"XP Required {oldExpThresh} --> {player.ExpThresh}");
            }
            void StatUpdate()
            {
                int oldMaxHealthVal = player.MaxHealth;
                int healthIncrease = (int) Math.Ceiling(Math.Pow(player.Level, 2) / 5);
                player.MaxHealth += healthIncrease;
                player.CurrentHealth = player.MaxHealth; //Fully heal on every level up
                
                int oldAttackVal = player.CurrentAttack;
                int attackIncrease = (int) Math.Ceiling(Math.Pow(player.Level, 2) / 20);
                player.MaxAttack += attackIncrease;
                player.CurrentAttack = player.MaxAttack;
                
                double oldEvasionVal = player.TotalEvasion;
                if (player.UnbuffedEvasion < player.UnbuffedEvasionCap)
                {
                    player.UnbuffedEvasion += player.EvasionPerLevel;
                    player.TotalEvasion += player.EvasionPerLevel;
                }

                Console.WriteLine(Environment.NewLine + $"Max Health Up! {oldMaxHealthVal} --> {player.CurrentHealth}"
                                                      + Environment.NewLine + $"Attack Up! {oldAttackVal} --> {player.CurrentAttack}");
                
                Console.WriteLine(player.UnbuffedEvasion < player.UnbuffedEvasionCap 
                    ? $"Evasion Up! {oldEvasionVal * 100:##.##}% --> {player.TotalEvasion * 100:##.##}%" + Environment.NewLine 
                    : $"Max Evasion from levels hit! {oldEvasionVal * 100:##.##}% --> {player.TotalEvasion * 100:##.##}%"); // 0 represents always-appearing digit; # is optional
            }
            
            LevelUpdate();
            StatUpdate();
        }
        public static void ProcessBuffs(Player player, Enemy enemy)
        {
            //Process player buffs
            if (player.Buffs.Count > 0)
            {
                Buff[] currentPlayerBuffs = player.Buffs.ToArray();
                foreach (var buff in currentPlayerBuffs)
                {
                    buff.MinusOneTurn();
                    
                    if (buff.TurnsRemaining == 0)
                    {
                        buff.Remove(player); //needs to be able to remove any buff, not just actives
                        buff.Applied = false;
                        Utils.Separator('-');
                        Console.WriteLine($"{buff.Name} has expired!");
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

        public static void DisplayBuffs(this Character player)
        {
            string buffs = "";
            foreach (var buff in player.Buffs)
            {
                buffs += $"{buff}: Turns Remaining {buff.TurnsRemaining}" + Environment.NewLine;
            }

            Console.Write(buffs);
        }

        public static bool AttackChance(this Player player)
        {
            var rand = new Random();
            double playerRoll = Math.Round(rand.NextDouble(), 2);
            //Console.WriteLine($"player roll: {playerRoll}");
            if (playerRoll <= player.Accuracy) return true;
            else
            {
                Console.WriteLine("Your attack missed the enemy!");
                return false;
            }
        }

        public static bool DefenseChance(this Player player, Enemy enemy)
        {
            var rand = new Random();
            double evade = Math.Round(rand.NextDouble(), 2);
            double enemyRoll = Math.Round(rand.NextDouble(), 2);
            //Console.WriteLine($"enemy roll: {enemyRoll}");
            if (evade <= player.TotalEvasion)
            {
                Console.WriteLine("You evaded the enemies attack!"); 
                return false;
            }
            else if (enemyRoll <= enemy.Accuracy) return true;
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