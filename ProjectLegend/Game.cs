using System;
//using System.Data;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;


namespace ProjectLegend
{
    internal class Game
    {
        private readonly Utils _u = new Utils();
        private readonly GameFuncs _game = new GameFuncs();
        private static void Main(string[] args)
        {
            Console.WriteLine("This is the main gameloop!");
            var game1 = new Game();
            game1.Run(args[0]);
        }

        public void Run(string option)
        {
            if (option.Equals("game"))
            {
                Player p = _game.ChooseCharacter();
                //FightEnemy();
                GameLoop(p);
            }
        }

        public void GameLoop(Player p) //URGENT: Find way to take inputs once
        {
            Console.WriteLine("Entering main loop!");
            
            // Example Enemy
            Enemy e = new Enemy();
            string input;
            string[] args;
            bool playing = true;
            //Entering GameLoop
            while (playing)
            {
                // retrieves and converts args
                input = Console.ReadLine();
                args = input.Split(" ");
                args = (from str in args
                    select str.ToLower()).ToArray();
                Console.WriteLine($"You entered {args[0]}");
                
                
                _game.ParseCommand(args[0],p, e);
                
                
                //exit sequence
                if (args[0].Equals("exit"))
                {
                    playing = false;
                }

            }
        }
    }
}