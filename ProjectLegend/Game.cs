using System;
using System.Reflection.Emit;
using ProjectLegend.PlayerClasses;




namespace ProjectLegend
{
    public class Game
    {
        private readonly GameFuncs _game = new GameFuncs();
        private Player _player;
        private static void Main(string[] args)
        {
            var game1 = new Game();
            game1.Run(args[1]); // 0; for debugging and running in IDE. 1; for running in CLI
        }

        public void Run(string option)
        {
            if(option.Equals("game"))
            {
                //Let the player choose their character(future version)
                _player = _game.ChooseCharacter();
                
                //FightEnemy();
                GameLoop(_player);
            }
        }
        
        public void GameLoop(Player p) 
        {
            Utils.Separator();
            Console.WriteLine("Welcome to ProjectLegend!");
            Utils.Separator();
            Console.WriteLine("When typing commands, format for commands is: command arg1 arg2 ..." +
                              "\nSeparate each command by a space");
            Utils.Separator();
            while (p.Health > 0)
            {
                Console.Write("Your current options are (casing does not matter): "); //try to read this from an array//
                _game.PrintCommands();
                // retrieves and converts args
                string[] args = Utils.ReadInput();
                _game.ParseGeneralCommand(args, p);
            }
        }
    }
}