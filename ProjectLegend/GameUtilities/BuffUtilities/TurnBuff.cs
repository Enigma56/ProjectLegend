﻿using System;
using ProjectLegend.CharacterClasses;

namespace ProjectLegend.GameUtilities.BuffUtilities
{
    public class TurnBuff : Buff
    {
        //public bool MaintainEffect { get; set; }
        public Action<Character> TurnEffect { get; set; }

        public TurnBuff(string name, int duration) : base(name, duration) { } //Constructor

        public void ProcessTurnEffect(Character character)
        {
           
            TurnEffect(character); 
            MinusOneTurn();
          
        }
    }
}