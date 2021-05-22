using System;
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
            game1.Run(args[0]); // 0; for debugging and running in IDE. 1; for running in CLI
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
            _game.Separator();
            Console.WriteLine("Welcome to ProjectLegend!");
            _game.Separator();
            Console.Write("Your current options are (casing does not matter): "); //try to read this from an array//
            _game.PrintCommands();
            Console.WriteLine("Format for commands is: command arg1 arg2 ... ; each input separated by a space");
            _game.Separator();
            
            bool playing = true;
            //Entering GameLoop
            while (playing)
            {
                // retrieves and converts args
                string[] args = Utils.ReadInput();

                //exit sequence
                if (args[0].Equals("exit"))
                {
                    Console.WriteLine("Exiting game now!");
                    playing = false;
                }
                else
                {
                    _game.ParseGeneralCommand(this, args, p);
                }
            }
        }
    }
}