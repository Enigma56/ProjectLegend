using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using ProjectLegend.CharacterClasses;
using ProjectLegend.GameUtilities.BuffUtilities;
using ProjectLegend.ItemClasses;
using ProjectLegend.ItemClasses.GearClasses;

namespace ProjectLegend.GameUtilities
{
    public static class Utils
    {

        //returns user input as a sliced up lower-case string array
        public static string[] ReadInput(params string[] options) //Can add an override method to return one input instead of an array
        {
            string input;
            string[] args;
            do
            {
                Console.WriteLine("Your current options are: " + ToString(options));
                input = Console.ReadLine();
            } while (Equals(input, "") | input == null);
            //lowercase methods
            args = input.Split(' ', '\t').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            args = (from str in args select str.ToLower()).ToArray();
            Console.WriteLine(args);
            return args;
        }

        public static bool YesOrNo()
        { 
            Response:
                string query = ReadInput("yes", "no")[0];
                if (query.Equals("yes"))
                {
                    return true;
                }
                else if (!Equals(query, "yes") && !Equals(query, "no"))
                {
                    Console.WriteLine("Please type yes or no!");
                    goto Response;
                }
                else
                {
                    return false;
                }
        }

        public static void ExitSequence(Player p, string reason)
       {
           Separator('-');
           if (reason.Equals("death"))
           {
               Console.WriteLine($"You died at level {p.Level}");
               p.Health.Current = p.Health.Max;
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
        
        /**
         * Similar to Index of except it compares types rather than searching for something in the array 
         */
        public static int GetItemIndex(Item[] source, Item item)
        {
            for (int slot = 0; slot < source.Length; slot++)
            {
                if (source[slot] != null && source[slot].GetType() == item.GetType())
                {
                        return slot;
                }
            }
            return -1;
        }

        public static List<Stat> GetRandomStats(List<Stat> source, int numberOfStats)
        {
            if (source == null)
                throw new NullReferenceException();
            
            Random rand = new();
            List<Stat> chosenStats = new();

            for (int i = 0; i < numberOfStats;)
            {
                int index = rand.Next(source.Count);
                if (source[index].Chosen is false)
                {
                    var stat = source.ElementAt(index);
                    chosenStats.Add(stat);
                    stat.Chosen = true;
                    stat.RollStat();
                    i++;
                }
            }

            foreach (var stat in chosenStats)
                stat.Chosen = false;

            return chosenStats;
        }

        public static double RandomDouble(this Random random, double min, double max)
        {
            return Math.Round(random.NextDouble() * (max - min) + min, 2);
        }

        public static List<Item> DropItem(GearPool pool) //TODO: Generalize to drop items from any source
        {
            var droppedItems = new List<Item>();
            /*Random gearGenerator = new();
            //iterate through loot pool
            Gear droppedItem = null;
            
            bool itemChosen = false;
            while (itemChosen is false)
            {
                double itemChance = gearGenerator.NextDouble();
                int itemIndex = gearGenerator.Next(pool.LootPool.Count);
                
                if (itemChance <= pool.LootPool[itemIndex].DropRate) //Sets have an element but is not accessed like this
                {
                    droppedItem = pool.LootPool[itemIndex]; //.ElementAt for hashset indexing
                    itemChosen = true;
                }
            }
            return droppedItem;*/

            return droppedItems;
        }

        public static bool IsEmpty(this ICollection collection)
        {
            if (collection.Count == 0)
                return true;
            else
            {
                return false;
            }
        }

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
    }
}