using System;
using System.Collections.Generic;
using ProjectLegend.CharacterClasses;

using ProjectLegend.GameUtilities.FuncUtils;
using ProjectLegend.ItemClasses.GearClasses;

namespace ProjectLegend.GameUtilities
{
    public class GameManager //Should this class be static or not?
    {
        public bool GameRunning { get; }

        public static string CurrentMap { get; set; }
        public static string CurrentLocation { get; set; }
        public static GameFuncs GameFuncs { get; }
        public static LinkedList<Action<Player>> BackPointers { get; }

        public static GearPool CommonGear { get; set; }

        static GameManager()
        {
            GameFuncs = new GameFuncs();
            BackPointers = new LinkedList<Action<Player>>();

            CommonGear = new CommonGear();
        }

        public GameManager()
        {
            GameRunning = true;
        }

    }
}