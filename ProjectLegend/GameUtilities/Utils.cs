﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using ProjectLegend.CharacterClasses;
using ProjectLegend.GameUtilities.BuffUtilities;

namespace ProjectLegend.GameUtilities
{
    public static class Utils
    {

        public static string[] ReadInput(params string[] options)
        {
            string input;
            string[] args;
            do
            {
                Console.WriteLine("Your current options are: " + ToString(options));
                input = Console.ReadLine();
                args = input.Split(' ', '\t').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            } while (Equals(input, ""));
            
            args = (from str in args select str.ToLower()).ToArray();
            return args;
       }

        public static void ExitSequence(Player p, string reason)
       {
           Separator('-');
           if (reason.Equals("death"))
           {
               Console.WriteLine($"You died at level {p.Level}");
               p.CurrentHealth = p.MaxHealth;
           }
           else if (reason.Equals("finish"))
           {
               Console.WriteLine($"You exited at level {p.Level}");
           }
           Separator('-');
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
               stringArray += $"{PrintArrayElement(arr[i])}, "; // creates string representation of array
           }

           stringArray += $"{PrintArrayElement(arr[^1])}]";
           return stringArray;
       }

        public static string PrintArrayElement<T>(T arrayItem)
       {
           if (arrayItem is null)
               return "empty";
           else
           {
               return arrayItem.ToString();
           }
       }

        public static (NumberFormatInfo culture, NumberStyles numberStyles) IntegerCultureAndFormat()
        { 
            var culture = CultureInfo.CreateSpecificCulture("en-US").NumberFormat;
            var numberStyles = NumberStyles.None | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingWhite;
            return (culture, numberStyles);
        }
        
        //UNUSED
        public static List<int> EmptyIndeces<T>( this T[] array)
        {
            var emptyIndeces = new List<int>();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] is null)
                {
                    emptyIndeces.Add(i);
                }
            }

            return emptyIndeces;
        }

        public static string ToStringWithIndices<T>(T[] arr) 
        {
            string stringArray = "[";
            for (int i = 0; i < arr.Length - 1; i++)
            {
                stringArray += $"{i + 1}: {PrintArrayElement(arr[i])}, "; // creates string representation of array
            }

            stringArray += $"{arr.Length}: {PrintArrayElement(arr[^1])}]";
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