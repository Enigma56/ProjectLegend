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
                ChooseCharacter();
                FightEnemy();
                GameLoop();
            }
        }

        public void GameLoop()
        {
            //Initial Inputs
            Console.WriteLine("Please provide inputs!");
            string input = Console.ReadLine();
            string[] args = input.Split(" ");
            args = (from str in args
                select str.ToLower()).ToArray();
            Console.WriteLine("Entering main loop!");
            
            //Entering GameLoop
            while (!args[0].Equals("exit"))
            {
                    
                Console.WriteLine($"You entered {args[0]}");
                input = Console.ReadLine();
                args = input.Split(" ");
                U.ParseCommand(args[0]);

            }
        }

        public void ChooseCharacter()
        {
            Player p = new Player(100, 10);
            Console.WriteLine(p.ToString());
        }

        public void FightEnemy()
        {
            Enemy e = new Enemy();
            Console.WriteLine(e.ToString());
        }
    }
}