﻿using System.Globalization;
using System.Transactions;

namespace ProjectLegend.PlayerClasses
{
    public class Bloodhound : Player
    {
        public Bloodhound()
        {
            Health = 100;
            Attack = 15;
            Exp = 0;
            ExpThresh = 30;
        }
        
    }
}