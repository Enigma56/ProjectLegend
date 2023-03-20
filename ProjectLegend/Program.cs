// See https://aka.ms/new-console-template for more information


using ProjectLegend.GameUtilities;
using ProjectLegend.CharacterClasses;
using ProjectLegend.GameWorld;
//Does not need to be used because
//1) It is not a static class


namespace ProjectLegend
{
    public class Game 
    {
        private GameManager Manager { get; }
        private World World { get; }

        private Game()
        {
            //TODO: Fix Gamemanager
            Manager = new GameManager();
            World = new World();
        }
        

        private static void Main(string[] args)
        {
            var game = new Game();
            game.Run(); // 0; for debugging and running in IDE. 1; for running in CLI
        }

        private void Run() //responsible for running the game
        {
            WorldInitialization();
            GameManager.GameFuncs.ChooseCharacter();
            GameLoop();
        }

        private void WorldInitialization()
        {
            World.Initialize();
            foreach (var entry in World.MapDict)
            {
                entry.Value.Initialize();
            }
        }

        private void GameLoop() //currently just parses commands in the infinite loop
        {
            void Intro(){
                Console.WriteLine();
                Utils.Separator('#');
                Console.WriteLine("Welcome to ProjectLegend!");
                Utils.Separator('#');
                Console.WriteLine(Environment.NewLine + "When typing commands, format for commands is: command arg1 arg2 ..." +
                                  Environment.NewLine + "Separate each command by a space");
                Utils.Separator('-');
            }

            void MainLoop() //This loop only handles general command choices
            {
                while (Manager.GameRunning)
                {
                    string[] args = Utils.ReadInput(GameManager.GameFuncs.GenCommands());
                    GameManager.GameFuncs.ParseGeneralChoice(World, args);
                }
            }

            //Set head of LinkedList BackPointers - will never be removed as head of the linked list
            Action PrimaryGameLoop = MainLoop;
            var Header = new LinkedListNode<Action>(PrimaryGameLoop);
            GameManager.BackPointers.AddFirst(Header);
            
            Intro();
            MainLoop();
        }
    }
}