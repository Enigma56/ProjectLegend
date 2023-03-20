using System;
using System.Collections.Generic;
using ProjectLegend.CharacterClasses;

using ProjectLegend.GameUtilities.FuncUtils;
using ProjectLegend.ItemClasses.GearClasses;

namespace ProjectLegend.GameUtilities
{
    //TODO: Turn this into a Singleton pattern
    public class GameManager //Should this class be static or not?
    {
        public bool GameRunning { get; }

        public static string CurrentMap { get; set; }
        public static string CurrentLocation { get; set; }
        public static GameFuncs GameFuncs { get; }
        
        //TODO: Turn this into a Stack to better manage user actions
        public static LinkedList<Action> BackPointers { get; set; }

        //TODO: Move Gear drops into a common location
        public static GearPool CommonGear { get; set; }

        static GameManager()
        {
            GameFuncs = new GameFuncs();
            BackPointers = new LinkedList<Action>();

            CommonGear = new CommonGear();
        }

        public GameManager()
        {
            GameRunning = true;
        }

    }
}