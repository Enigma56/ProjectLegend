﻿using ProjectLegend.GameUtilities;


namespace ProjectLegend.ItemClasses.GearClasses
{
    public class CharacterStats
    {
        //General Stat Pools
        public static List<Stat> AllStats { get; }

        //public static List<Stat> LegendaryStats { get; }
        //Gear Type Specific Stat Pools

        //Character stat list
        public static Dictionary<string, Stat> Stats { get; set; }
        
        
        public CharacterStats()
        {
            Stats = new Dictionary<string, Stat>() //
            {
                ["str"] = new Strength(),
                ["vit"] = new Vitality(),
                ["fatk"] = new FlatAttIncrease(),
                ["fhp"] = new FlatHpIncrease(),
                ["patk"] = new AttackPercentIncrease(),
                ["php"] = new HealthPercentIncrease(),
            };
        }
        public Stat this[string key] //index into the dictionary; this may be redundant
        {
            get => Stats[key];
        }
    }

    public abstract class Stat
    {
        public class NumRange
        {
            private dynamic _min;
            private dynamic _max;

            public NumRange(dynamic min, dynamic max)
            {
                _min = min;
                _max = max;
            }

            public dynamic GetMin()
            {
                return _min;
            }

            public dynamic GetMax()
            {
                return _max;
            }
        }
        
        
        private static readonly Random StatRoller = new();
        public string Name { get;}
        public string Id { get; }
        public string Type { get; }
        public bool Chosen { get; set; } //flag when choosing random stat

        public dynamic StatRoll { get; set; } = 0;

        public dynamic StatTotal { get; set; } = 0;
        public NumRange Range { get; set; }
        public double Multiplier { get; }
        
        protected Stat(string name, string id, string type)
        {
            Name = name;
            Id = id;
            Type = type;
            
            Multiplier = 1.0;
        }

        public void RollStat()
        {
            dynamic min = Range.GetMin();
            dynamic max = Range.GetMax();
            
            if (min is int minInt && max is int maxInt)
            {
                StatRoll = StatRoller.Next(minInt, maxInt);
            }
            else if (min is double minDouble && max is double maxDouble)
            {
                StatRoll = StatRoller.RandomDouble(minDouble, maxDouble);
            }
            else
            {
                throw new ArgumentException("No Valid numbers to roll stats from!");
            }
        }

        public override string ToString()
        {
            string total;
            if(StatRoll is int)
                total = $": {StatRoll}";
            else
            {
                total = $": {StatRoll * 100:#0.0#}%";
            }
            return Name + total;
        }
    }

    //Core Stats
    public sealed class HealthPoints 
    {
        public int Max { get; set; } //Refactor to be the maximum value, not current max
        public int Current { get; set; }
        public int Bonus { get; set; }
    }

    public sealed class AttackPoints
    {
        private int _current;
        public int Current
        {
            get => (int) (_current * AtkMultiplier);
            set => _current = value;
        }
        public int Max { get; set; }
        public int Bonus { get; set; }
        
        public double AtkMultiplier { get; set; }

        public AttackPoints()
        {
            AtkMultiplier = 1.0;
        }
    }

    public class Energy : Stat
    {
        public static int EnergyPerTurn = 50;
        
        public int Current { get; set; }
        public int Max { get; set; }

        public Energy() : base("Energy", "nrg", "add")
        {
            Max = 1000;
        }
    }
    
    public sealed class Evasion
    {
        public const double PercentPerLevel = .002;
        public double Total { get; set; }
        public double UnbuffedTotal { get; set; }
        public double UnbuffedCap { get; }

        public Evasion()
        {
            Total = 0.0;
            UnbuffedTotal = 0.0;
            UnbuffedCap = .05;
        }
    }

    // Gear Stats
     public sealed class Strength : Stat
     {
         public int Base { get; set; }
         public int Max { get; set; }
         
         public Strength() : base("Strength", "str", "add")
         {
             Range = new NumRange(5, 15);
             Base = 0;
             Max = 500;
         }
     }

     /// <summary>
     /// Stat for gear. NOT FOR PLAYER
     /// </summary>
     //TODO: Consider renaming this to better reflect its use case, unless it can be used for player too
     public sealed class Vitality : Stat
     {
         public int Base { get; set; }
         public int Max { get; set; }
         
         public Vitality() : base("Vitality","vit", "add")
         {
             Range = new NumRange(5, 15);
             Base = 0;
             Max = 500;
         }
     }
     
     public sealed class FlatAttIncrease : Stat
     {
         public FlatAttIncrease() : base("Flat Attack","fatk", "add")
         {
             Range = new NumRange(1, 5);
         }
     }

     public sealed class FlatHpIncrease : Stat
     {
         public FlatHpIncrease() : base("Flat HP", "fhp", "add")
         {
             Range = new NumRange(1, 5);
         }
     }

     public sealed class AttackPercentIncrease : Stat
     {
         public AttackPercentIncrease() : base("Atk % Increase", "patk", "mult")
         {
             Range = new NumRange(.01, .05);
         }
     }

     public sealed class HealthPercentIncrease : Stat
     {
         public HealthPercentIncrease() : base("HP % Increase","php", "mult")
         {
             Range = new NumRange(.01, .05);
         }
     }
}