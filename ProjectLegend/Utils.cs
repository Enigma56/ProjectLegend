using System;
using System.Security.Cryptography;

namespace ProjectLegend
{
    public class Utils
    {
       public void ArrayToString<T>(T[] arr)
        {
            string stringArray = "[";
            for (int i = 0; i < arr.Length - 1; i++)
            {
                stringArray += $"{arr[i]}, "; // creates string representation of array
                
            }

            stringArray += $"{arr[arr.Length - 1]}]";
            Console.WriteLine(stringArray);
        }

       public void ParseCommand(string cmd)
       {
           
           switch(cmd)

           {
               case "attack":
                   Console.WriteLine("attacked!");
                   break;
               case "exit":
                   Console.WriteLine("exiting!");
                   break;
               default:
                   Console.WriteLine(cmd);
                   break;
           }
       }
       
    }
}