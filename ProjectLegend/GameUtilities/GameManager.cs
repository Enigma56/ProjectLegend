using System;
using System.Collections.Generic;
using ProjectLegend.CharacterClasses;

using ProjectLegend.GameUtilities.FuncUtils;

namespace ProjectLegend.GameUtilities
{
    public class GameManager //Should this class be static or not?
    {
        public bool GameRunning { get; }

        public static string CurrentMap { get; set; }
        public static string CurrentLocation { get; set; }

        public static GameFuncs GameFuncs = new();
        public static LinkedList<Action<Player>> BackPointers = new();
        
        public GameManager()
        {
            GameRunning = true;
        }

    }
}