using System;

namespace ProjectLegend.PlayerClasses
{
    public static class PlayerUtilities
    {
        public static void AddLevel(this Player player)
        {
            void LevelUpdate()
            { 
                int xp =player.Exp;
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
        public static void DisplayBuffs(this Player player)
        {
            Console.WriteLine(Utils.ToString(player.Buffs));
        }
    }
}