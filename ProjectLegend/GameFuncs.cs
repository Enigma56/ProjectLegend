using System;

namespace ProjectLegend
{
    public class GameFuncs
    {
         public void ParseCommand(string cmd, Player p, Enemy e)
               {
                   
                   switch(cmd)
        
                   {
                       case "attack":
                           Console.WriteLine("attacked!");
                           p.Health -= e.Attack;
                           Console.WriteLine(p.Health);
                           e.Health -= p.Attack;
                           Console.WriteLine(e.Health);
                           break;
                       case "exit":
                           Console.WriteLine("exiting!");
                           break;
                       default:
                           Console.WriteLine(cmd);
                           break;
                   }
               }
         /// <summary>
         /// Choose your character from a predetermined List
         /// !!!CURRENTLY IN TESTING!!!
         /// </summary>
         /// <returns>chosen player</returns>
         public Player ChooseCharacter()
         {
             Player p = new Player(100, 10);
             //Console.WriteLine(p.ToString());
             return p;
         }
         
         /**
          * Enters loop to fight an enemy
          */
         public void FightEnemy() //Fight an enemy and spawn another when alive one dies
         {
             Enemy e = new Enemy();
             Console.WriteLine(e.ToString());
         }
    }
}