﻿using System;
using System.Collections.Generic;

namespace ProjectLegend.GameWorld
{
    public abstract class Map
    {
        public Dictionary<string, Location> 
            LocationDict { get; protected init; } //perhaps use a Location ID to hash into the instance

        public string[] Locations { get; protected init; }
        public bool Complete { get; set; }
        
        public abstract void Initialize();
    }
}