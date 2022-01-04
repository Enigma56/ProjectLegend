using System;
using System.Collections.Generic;

using ProjectLegend.ItemClasses;
using ProjectLegend.GameUtilities.BuffUtilities;
using ProjectLegend.ItemClasses.GearClasses;


namespace ProjectLegend.CharacterClasses
{
    public abstract class Player : Character
    {
        public CharacterStats PlayerStats { get; }
        public Gear[] GearInventory { get; }
        public Item[] Inventory { get; }
        public Item Hand { get; set; }

        public int Level { get; set; }
        public int Exp { get; set; }
        public int ExpThresh { get; set; }
        
        public int ActiveCost { get; protected set; }
        public int UltimateCost { get; protected set; }
        
        public Energy Energy { get; set; }
        public Evasion Evasion { get; set; }
        
        private int BaseVitality { get; set; }
        private int MaxVitality { get; set; }
        private int BaseStrength { get; set; }
        private int MaxStrength { get; set; }

        protected bool CanUpdatePassive { get; init; }
        
        public int DeathCount { get; set; }
        

        protected Player()
        {
            GearInventory = new Gear[4];
            Inventory = new Item[10];

            PlayerStats = new CharacterStats();
            Buffs = new List<Buff>();

            Energy = new Energy();
            Energy.Current = 0;
            Evasion = new Evasion();

            Accuracy = 1;
            
            BaseStrength = 0;
            BaseVitality = 0;
            MaxStrength = 500;
            MaxVitality = 500;

            Level = 1;
            Exp = 0;
            ExpThresh = 10;
        }
        
       //If the ability is a buff, energy checking is done when applying the buff 
        public abstract void Passive(); //No flag; always active
        public abstract void UpdatePassive();
        public abstract void Active(Enemy enemy); //Flag -a
        public abstract void Ultimate(Enemy enemy); //Flag: -u

        public void AddLevel()
        {
            LevelUpdate();
            StatLevelUpdate();
        }
        
        private void LevelUpdate()
        { 
            int xp = Exp;
            int oldLevel = Level;
                
            Exp = 0;
            Exp += (xp % ExpThresh);
            Level++;
            int oldExpThresh =ExpThresh;
            int thresholdIncrease = (int) Math.Ceiling(Math.Pow(Level, 2) * Math.Log10(Level));
            ExpThresh += thresholdIncrease;

            Console.WriteLine("You Leveled Up!" + Environment.NewLine + 
                              $"Current level: {oldLevel} --> {Level}" + Environment.NewLine + 
                              $"XP Required {oldExpThresh} --> {ExpThresh}");
        }
        
        private void StatLevelUpdate()
        {
            //HP Stats
            int oldMaxHealthVal = Health.Max;
            BaseVitality += (int) Math.Ceiling(Math.Pow(Level, 2) / 5);
            Health.Max += (int) Math.Ceiling(Math.Pow(2, (Level + 100) / 20.0) - 32);
            Health.Current = Health.Max; //Fully heal on every level up
            
            //Atk Stats
            int oldAttackVal = Attack.Max;
            BaseStrength += (int) Math.Ceiling(Math.Pow(Level, 2) / 20);
            Attack.Max += (int) Math.Ceiling(Math.Pow(2, (Level + 100) / 25.0) - 16);
            Attack.Current = Attack.Max;

            double oldEvasionVal = Evasion.Total;
            if (Evasion.UnbuffedTotal < Evasion.UnbuffedCap)
            {
                Evasion.UnbuffedTotal += Evasion.PercentPerLevel;
                Evasion.Total += Evasion.PercentPerLevel;
            }

            if (CanUpdatePassive)
            {
                UpdatePassive();
            }
                

            Console.WriteLine(Environment.NewLine + $"Max Health Up! {oldMaxHealthVal} --> {Health.Current}"
                                                  + Environment.NewLine + $"Attack Up! {oldAttackVal} --> {Attack.Max}");
                
            Console.WriteLine(Evasion.UnbuffedTotal < Evasion.UnbuffedCap 
                ? $"Evasion Up! {oldEvasionVal * 100:#0.0#}% --> {Evasion.Total * 100:##.##}%" + Environment.NewLine 
                : $"Max Evasion from levels hit! {oldEvasionVal * 100:##.##}% --> {Evasion.Total * 100:##.##}%"); // 0 represents always-appearing digit; # is optional
        }

        public void UpdatePlayerStats(Gear newGear, Gear oldGear, string action)
        {
            //Removes old bonus(if set)
            Attack.Max -= Attack.Bonus; //Where does the Bonus stat come from?
            Attack.Current -= Attack.Bonus;
            Health.Max -= Health.Bonus;
            Health.Current -= Health.Bonus;

            UpdateStatsFromGear(newGear, oldGear, action); //Changes bonus in /CalculateBonuses/
            
            //Adds new bonus
            Attack.Max += Attack.Bonus;
            Attack.Current += Attack.Bonus;
            Health.Max += Health.Bonus;
            Health.Current += Health.Bonus;
        }

        private void UpdateStatsFromGear(Gear newGear, Gear oldGear, string action) //add and remove addition stats from base
        {
            if (action.Equals("new/replace"))
            {
                if (oldGear != null) //remove oldGear stats
                {
                    foreach (var stat in oldGear.GearStats)
                    {
                        PlayerStats[stat.Id].StatTotal -= stat.StatRoll;
                    }
                }

                //add newGear stats
                foreach (var stat in newGear.GearStats)
                {
                    PlayerStats[stat.Id].StatTotal += stat.StatRoll;
                }
            }
            else
            {
                foreach (var stat in newGear.GearStats)
                {
                    PlayerStats[stat.Id].StatTotal -= stat.StatRoll;
                }
            }
            CalculateBonuses();
        }

        private void CalculateBonuses()
        {
            int playerStrTotal = BaseStrength + PlayerStats["str"].StatTotal;
            int factoredStr =playerStrTotal / 12; //equation: Math.Pow(2, (Level + 100)/ 25.0) - 16
            int bonusFromAtkPercent = (int) (Attack.Max * PlayerStats["patk"].StatTotal);
            int flatAtkBonus = (int) PlayerStats["fatk"].StatTotal;
            int calculatedBonusAtk = factoredStr + flatAtkBonus + bonusFromAtkPercent;
            Attack.Bonus = calculatedBonusAtk;

            int playerVitTotal = BaseVitality + PlayerStats["vit"].StatTotal; 
            int factoredVit = playerVitTotal / 4;
            int bonusFromHpPercent = (int) (Health.Max * PlayerStats["php"].StatTotal);
            int calculatedBonusHp = factoredVit + PlayerStats["fhp"].StatTotal + bonusFromHpPercent;
            Health.Bonus = calculatedBonusHp;
        }

        public override string ToString()
        {
            return "Stats: " + Environment.NewLine + 
                   $"Health = {Health.Current}" + Environment.NewLine + 
                   $"Attack = {Attack.Current}" + Environment.NewLine +
                   $"Current Energy = {Energy.Current} && Max Energy = {Energy.Max}" + Environment.NewLine +
                   $"Vitality = {PlayerStats["vit"].StatTotal}" + Environment.NewLine +
                   $"Strength = {PlayerStats["str"].StatTotal}" + Environment.NewLine +
                   $"Evasion = {Evasion.Total * 100:#0.0#}%";
                    
        }
    }
}