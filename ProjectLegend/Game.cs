using System;
//using System.Data;
using System.Linq;


namespace ProjectLegend
{
    internal class Game
    {
        private static readonly Utils U = new Utils();
        private static void Main(string[] args)
        {
            Console.WriteLine("This is the main gameloop!");
            var game1 = new Game();
            U.ArrayToString(args);
            game1.Run(args[1]);
        }

        public void Run(string option)
        {
            if (option.Equals("game"))
            {
                GameLoop();
            }
        }

        public void GameLoop()
        {
            Console.WriteLine("Please provide inputs!");
            string input = Console.ReadLine();
            string[] args = input.Split(" ");
            args = (from str in args
                select str.ToLower()).ToArray();
            Console.WriteLine("Entering main loop!");
            U.ArrayToString(args);
            while (!args[0].Equals("exit"))
            {
                    
                Console.WriteLine($"You entered {args[0]}");
                input = Console.ReadLine();
                args = input.Split(" ");
                U.ArrayToString(args);

            }
        }
    }
}