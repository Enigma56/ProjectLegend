using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ProjectLegend.CharacterClasses;

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

        public static void ExitSequence(Player p)
       {
           Separator('-');
           Console.WriteLine($"You finished at level {p.Level}");
           Separator('-');
           Environment.Exit(0);
       }
       
       public static void Separator(char sep)
       {
           var separator = new string(sep, 30);
           Console.WriteLine(separator);
       }
       public static string ToString<T>(T[] arr)
       {
           string stringArray = "[";
           for (int i = 0; i < arr.Length - 1; i++)
           {
               stringArray += $"{arr[i].ToString()}, "; // creates string representation of array
           }

           stringArray += $"{arr[^1]}]";
           return stringArray;
       }

       public static string ToString(List<Buff> arr)
       {
           if (arr.Count > 0)
           {
               string stringArray = "[";
               for (int i = 0; i < arr.Count - 1; i++)
               {
                   stringArray += $"{arr[i].ToString()}, "; // creates string representation of array
               }

               stringArray += $"{arr[^1]}]";
               return stringArray;
           }
           else
           {
               return "You do not have any active buffs!";
           }
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