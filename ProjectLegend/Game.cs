using System;

using ProjectLegend.GameUtilities;
using ProjectLegend.GameUtilities.FuncUtils;
using ProjectLegend.CharacterClasses;
using ProjectLegend.World; //why doesnt this directive need to be used?

namespace ProjectLegend
{
    public class Game 
    {
        private World.World World { get; }

        public readonly GameFuncs GameFuncs = new();
        public bool Running { get; set; }

        private Game()
        {
            World = new World.World(); //TODO: fix the reason that this needs "World.World"
        }

        private static void Main(string[] args)
        {
            var game = new Game();
            game.Run(); // 0; for debugging and running in IDE. 1; for running in CLI
        }

        private void Run() //responsible for running the game
        {
            var player = GameFuncs.ChooseCharacter();
            WorldInitialization();
            GameLoop(player);
        }

        private void WorldInitialization()
        {
            World.Initialize();
            foreach (var entry in World.MapDict)
            {
                entry.Value.Initialize();
            }
        }

        private void GameLoop(Player p) //currently just parses commands in the infinite loop
        {
            void Intro(){Utils.Separator('#');
                Console.WriteLine("Welcome to ProjectLegend!");
                Utils.Separator('#');
                Console.WriteLine(Environment.NewLine + "When typing commands, format for commands is: command arg1 arg2 ..." +
                                  Environment.NewLine + "Separate each command by a space");
                Utils.Separator('-');
            }
            
            Intro();
            Running = true;
            while (Running) //This loop only handles general command choices
            {
                string[] args = Utils.ReadInput(GameFuncs.GenCommands());
                GameFuncs.ParseGeneralChoice(this, args, p);
            }
        }
    }
}