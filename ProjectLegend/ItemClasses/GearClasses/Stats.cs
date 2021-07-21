using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using ProjectLegend.GameUtilities;


namespace ProjectLegend.ItemClasses.GearClasses
{
    public class CharacterStats
    {
        public static List<Stat> AllStats { get; }
        public static List<Stat> StandardStats { get; }
        public Dictionary<string, Stat> stats { get; }

        static CharacterStats()
        {
            AllStats = new List<Stat>()
            {
                new Strength(),
                new Vitality(),
                new FlatAttIncrease(),
                new FlatHpIncrease(),
                new AttackPercentIncrease(),
                new HealthPercentIncrease()
            };
            
            StandardStats = new List<Stat>()
            {
                new Strength(),
                new Vitality(),
                new FlatAttIncrease(),
                new FlatHpIncrease()
            };
        }

        public CharacterStats() //TODO: HP/ATK/Ev% need to be reworked into core stats
        {
            stats = new Dictionary<string, Stat>() //
            {
                
                //["hp"] = new HealthPoints(),
                //["atk"] =  new AttackPoints(),
                ["str"] = new Strength(),
                ["vit"] = new Vitality(),
                ["fatk"] = new FlatAttIncrease(),
                ["fhp"] = new FlatHpIncrease(),
                ["patk"] = new AttackPercentIncrease(),
                ["php"] = new HealthPercentIncrease(),
                //E%
            };
        }
        public Stat this[string key]
        {
            get => stats[key];
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

    public sealed class HealthPoints //Core
    {
        public int Current { get; set; }
        public int Max { get; set; }
        public int Bonus { get; set; }

        public HealthPoints()
        {
            Current = 0;
            Max = 0;

            Bonus = 0;
        }
    }

    public sealed class AttackPoints //Core
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

     public sealed class Strength : Stat
     {
         public int Base { get; set; }
         public int Max { get; set; }
         
         public Strength() : base("Strength", "str", "add")
         {
             Range = new NumRange(5, 20);
             Base = 0;
             Max = 500;
         }
     }

     public sealed class Vitality : Stat
     {
         public int Base { get; set; }
         public int Max { get; set; }
         
         public Vitality() : base("Vitality","vit", "add")
         {
             Range = new(5, 20);
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
             Range = new NumRange(.05, .1);
         }
         
     }

     public sealed class HealthPercentIncrease : Stat
     {
         public HealthPercentIncrease() : base("HP % Increase","php", "mult")
         {
             Range = new NumRange(.05, .1);
         }
     }

     public sealed class Evasion //Core
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
}