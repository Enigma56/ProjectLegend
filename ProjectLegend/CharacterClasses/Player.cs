using ProjectLegend.ItemClasses;
using ProjectLegend.GameUtilities.BuffUtilities;
using ProjectLegend.ItemClasses.GearClasses;

// REQUIRES INHERENT SEPARATION OF ENEMIES & LEGENDS

//Process to convert to Singleton
// 1. Player has to be static which creates errors that need to be addressed
// 2. Turn field variables to static
//      a. code base will now contain references to this now-static variable
//      b. Go through and change code to refer to these field varaibles statically
// 3. Create delegate field variables for character functions
//      a. allow a specific player to be chosen, assigning their abilities to these delegates
//      b. account for legend-specific abilities - i.e. lifeline

namespace ProjectLegend.CharacterClasses
{
    public sealed class Player : Character
    {
        private Player()
        {
            GearInventory = new Gear[4];
            Inventory = new Item[10];

            PlayerStats = new CharacterStats();
            Buffs = new List<Buff>();

            Energy = new Energy { Current = 0 };
            Evasion = new Evasion();

            Accuracy = 1;
            
            //Health and such are inherited from Character
            Health.Max = 50;
            Attack.Max = 20;
            Health.Current = Health.Max;
            Attack.Current = Attack.Max;
            
            BaseStrength = 0;
            BaseVitality = 0;
            MaxStrength = 500;
            MaxVitality = 500;

            Level = 1;
            Exp = 0;
            ExpThresh = 10;
        }  
        private static Player? _instance;  
        public static Player Instance {  
            get { return _instance ??= new Player(); }  
        }
        
        //Encapsulate Legend such that it is a sub-class of player
        

        //Would store the chosen legend's abilities to be used
        public Character Legend;
        
        //These are accessed via Instance.
        //If the ability is a buff, energy checking is done when applying the buff 
        public Action Passive; //No flag; always active
        public Action UpdatePassive;
        public Action<Enemy> Active; //Flag -a
        public Action<Enemy> Ultimate; //Flag: -u

        
        public CharacterStats PlayerStats { get; }
        public Gear[] GearInventory { get; }
        public Item[] Inventory { get; }
        public Item Hand { get; set; }

        public int Level { get; private set; }
        public int Exp { get; set; }
        public int ExpThresh { get; set; }
        
        public int ActiveCost { get; set; }
        public int UltimateCost { get; set; }
        
        public Energy Energy { get; set; }
        public Evasion Evasion { get; set; }
        
        private int BaseVitality { get; set; }
        private int BaseStrength { get; set; }
        
        //TODO: Max Vit/Str are not checked for when calculating stats
        private int MaxVitality { get; set; }
        private int MaxStrength { get; set; }

        public bool CanUpdatePassive { get; set; }
        
        public int DeathCount { get; set; }
        
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
                Instance.UpdatePassive();
            }
            
            Console.WriteLine(Environment.NewLine + $"Max Health Up! {oldMaxHealthVal} --> {Health.Current}"
                                                  + Environment.NewLine + $"Attack Up! {oldAttackVal} --> {Attack.Max}");
                
            Console.WriteLine(Evasion.UnbuffedTotal < Evasion.UnbuffedCap 
                ? $"Evasion Up! {oldEvasionVal * 100:#0.0#}% --> {Evasion.Total * 100:##.##}%" + Environment.NewLine 
                : $"Max Evasion from levels hit! {oldEvasionVal * 100:##.##}% --> {Evasion.Total * 100:##.##}%"); // 0 represents always-appearing digit; # is optional
        }

        public void UpdatePlayerStats(Gear newGear, Gear oldGear, string action)
        {
            //TODO: There has to be a better way of doing this
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