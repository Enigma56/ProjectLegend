using System;
using System.Collections.Generic;

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
        
        public CharacterStats()
        {
            stats = new Dictionary<string, Stat>()
            {
                ["str"] = new Strength(),
                ["vit"]  = new Vitality(),
                ["fatk"] = new FlatAttIncrease(),
                ["fhp"] = new FlatHpIncrease(),
                ["patk"] = new AttackPercentIncrease(),
                ["php"] = new HealthPercentIncrease()
            };
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

        public static void RollStat(Stat stat, dynamic min, dynamic max)
        {
            if (min is int minInt && max is int maxInt)
            {
                stat.StatRoll = StatRoller.Next(minInt, maxInt);
            }
            else if (min is double minDouble && max is double maxDouble)
            {
                stat.StatRoll = StatRoller.RandomDouble(minDouble, maxDouble);
            }
            else
            {
                throw new ArgumentException("No Valid numbers to roll stats from!");
            }
        }

        public string RollsToString()
        {
            string roll;
            if (StatRoll is int)
                roll = $": {StatRoll}";
            else
            {
                roll = $": {StatRoll * 100:#0.0#}";
            }
            return Name + roll;
        }

        public override string ToString()
        {
            string total;
            if (StatTotal is int)
                total = $": {StatTotal}";
            else
            {
                total = $": {StatTotal * 100:#0.0#}";
            }
            return Name + total;
        }
    }

     public sealed class Strength : Stat
     {
         public Strength() : base("Strength", "str", "add")
         {
             Range = new NumRange(5, 20);
         }
     }

     public sealed class Vitality : Stat
     {

         public Vitality() : base("Vitality","vit", "add")
         {
             Range = new(5, 20);
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
}