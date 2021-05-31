﻿using System;
using System.Linq;
using ProjectLegend.PlayerClasses;

namespace ProjectLegend
{
    public static class Utils
    {
        public static void ArrayToString<T>(this T[] arr)
        {
            string stringArray = "[";
            for (int i = 0; i < arr.Length - 1; i++)
            {
                stringArray += $"{arr[i]}, "; // creates string representation of array
            }

            stringArray += $"{arr[^1]}]";
            Console.WriteLine(stringArray);
        }

       public static string[] ReadInput()
       {
           string input = Console.ReadLine();
           string[] args = input.Split();
           args = (from str in args select str.ToLower()).ToArray();
           return args;
       }

       public static void ExitSequence(Player p)
       {
           Console.WriteLine($"You finished at level {p.Level}");
           Environment.Exit(0);
       }
       
       public static void Separator()
       {
           string separator = "--------------------------";
           Console.WriteLine(separator);
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