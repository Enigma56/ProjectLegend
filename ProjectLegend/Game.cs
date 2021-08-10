using System;

using ProjectLegend.GameUtilities;
using ProjectLegend.CharacterClasses;

namespace ProjectLegend
{
    public class Game 
    {
        public readonly GameFuncs GameFuncs = new();
        private Player _player;
        public bool Running { get; set; }
        private static void Main(string[] args)
        {
            var game1 = new Game();
            game1.Run("game"); // 0; for debugging and running in IDE. 1; for running in CLI
        }

        public void Run(string option)
        {
            if(option.Equals("game"))
            {
                //Let the player choose their character(future version)
                _player = GameFuncs.ChooseCharacter();
                GameLoop(_player);
            }
        }
        
        public void GameLoop(Player p) 
        {
            Utils.Separator('#');
            Console.WriteLine("Welcome to ProjectLegend!");
            Utils.Separator('#');
            Console.WriteLine(Environment.NewLine + "When typing commands, format for commands is: command arg1 arg2 ..." +
                              Environment.NewLine + "Separate each command by a space");
            Utils.Separator('-');
            
            Running = true;
            while (Running) //this expression needs to be checked before the game ends
            {
                _player.Dead = false;
                string[] args = Utils.ReadInput(GameFuncs.GenCommands());
                GameFuncs.ParseGeneralCommand(this, args, p);
            }
        }
    }
}