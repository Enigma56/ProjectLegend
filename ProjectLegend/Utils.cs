using System;
using System.Linq;
using ProjectLegend.PlayerClasses;

namespace ProjectLegend
{
    public static class Utils
    {

        public static string[] ReadInput(string[] options)
        {
            string input;
            string[] args;
            do
            {
                Console.WriteLine("Your current options are: " + ToString(options));
                input = Console.ReadLine();
                args = input.Split();
            } while (Equals(input, ""));
            
            args = (from str in args select str.ToLower()).ToArray();
            return args;
       }
       public static bool AttackChance(Player player)
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

       public static bool DefenseChance(Player player, Enemy enemy)
       {
           var rand = new Random();
           double evade = Math.Round(rand.NextDouble(), 2);
           double enemyRoll = Math.Round(rand.NextDouble(), 2);
           //Console.WriteLine($"enemy roll: {enemyRoll}");
           if (evade <= player.Evasion)
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

       public static void ExitSequence(Player p)
       {
           Separator();
           Console.WriteLine($"You finished at level {p.Level}");
           Separator();
           Environment.Exit(0);
       }
       
       public static void Separator()
       {
           string separator = "--------------------------";
           Console.WriteLine(separator);
       }
       public static string ToString<T>(T[] arr)
       {
           string stringArray = "[";
           for (int i = 0; i < arr.Length - 1; i++)
           {
               stringArray += $"{arr[i]}, "; // creates string representation of array
           }

           stringArray += $"{arr[^1]}]";
           return stringArray;
       }

       //NOT IN USE
       public static string GetItem(this string[] source, string target)
       {
           foreach (string item in source)
           {
               if (!item.Equals(target)) continue;
               
               try
               {
                   return item;
               }
               catch (Exception e) //get type of exception so that it does not catch generic exceptions
               {
                   Console.WriteLine($"{e}: Item not found in array!");
               }
           }

           return null;
       }

    }
}