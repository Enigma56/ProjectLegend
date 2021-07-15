﻿using System;
using System.Collections.Generic;
using ProjectLegend.CharacterClasses;

namespace ProjectLegend.ItemClasses.GearClasses
{
    public abstract class Gear : Item
    {
        protected static int NumCommonRolls = 1;
        protected static int NumRareRolls = 2;
        protected static int NumEpicRolls = 3;
        protected static int NumLegendaryRolls = 4;
        protected static int NumHeirloomRolls = 4;
        public List<Stat> gearStats { get; }
        public int Slot { get; set; }

        protected Gear()
        {
            Name = "Gear Not Implemented";

            IsStackable = false;
            gearStats = new List<Stat>();
        }
    }
}