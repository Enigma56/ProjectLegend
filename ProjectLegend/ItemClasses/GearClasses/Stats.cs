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
        //public Dictionary<string, Dictionary<string, Stat>> pstats { get; }

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

            /*pstats = new Dictionary<string, Dictionary<string, Stat>>()
            {
                ["add"] = new()
                {
                    ["str"] = new Strength(),
                    ["vit"]  = new Vitality(),
                    ["fatk"] = new FlatAttIncrease(),
                    ["fhp"] = new FlatHpIncrease(),
                },
                    
                ["mult"] = new()
                {
                    ["patk"] = new AttackPercentIncrease(),
                    ["php"] = new HealthPercentIncrease()
                }

            };*/
        }
    }

    public abstract class Stat
    {
        public class Value
        {
            private object _value;
            
            public Value(int value) { _value = value; }
            public Value(double value) { _value = value; }
            
            public object GetValue() { return _value; }

            public override string ToString()
            {
                if(GetValue() is int)
                    return $"{GetValue()}";
                else
                {
                    return $"{(double) GetValue() * 100:#0.0#}";
                }
            }
        }

        public class NumRange
        {
            private object _min;
            private object _max;

            public NumRange(int min, int max)
            {
                _min = min;
                _max = max;
            }
            public NumRange(double min, double max)
            {
                _min = min;
                _max = max;
            }

            public object GetMin()
            {
                return _min;
            }

            public object GetMax()
            {
                return _max;
            }
        }
        
        
        private static readonly Random StatRoller = new();
        public string Name { get;}
        public string Id { get; }
        public string Type { get; }
        public bool Chosen { get; set; } //flag when choosing random stat

        public Value StatValue { get; set; } = new(0);
        public NumRange Range { get; set; }
        public double Multiplier { get; }

        protected Stat(string name, string id, string type)
        {
            Name = name;
            Id = id;
            Type = type;
            
            Multiplier = 1.0;
        }
        public void RollStatValues(int min, int max)
        {
            StatValue = new Value(StatRoller.Next(min, max));
        }

        public void RollStatValues(double min, double max)
        {
            StatValue = new Value(StatRoller.RandomDouble(min, max));
        }
    }

     public sealed class Strength : Stat
     {
         public Strength() : base("Strength", "str", "add")
         {
             Range = new NumRange(5, 20);
         }

         public int CurrentValue()
         {
             return (int) Math.Ceiling((int) StatValue.GetValue() * Multiplier);
         }
         
     }

     public sealed class Vitality : Stat
     {

         public Vitality() : base("Vitality","vit", "add")
         {
             Range = new(5, 20);
         }
         
         public int CurrentValue()
         {
             return (int) Math.Ceiling((int)StatValue.GetValue() * Multiplier);
         }
     }
     
     public sealed class FlatAttIncrease : Stat
     {
         public FlatAttIncrease() : base("Flat Attack","fatk", "add")
         {
             Range = new NumRange(1, 5);
         }
         
         public int CurrentValue()
         {
             return (int) Math.Ceiling((int)StatValue.GetValue() * Multiplier);
         }
     }

     public sealed class FlatHpIncrease : Stat
     {
         public FlatHpIncrease() : base("Flat HP", "fhp", "add")
         {
             Range = new NumRange(1, 5);
         }
         
         public int CurrentValue()
         {
             return (int) Math.Ceiling((int)StatValue.GetValue() * Multiplier);
         }
     }

     public sealed class AttackPercentIncrease : Stat
     {
         public AttackPercentIncrease() : base("Atk % Increase", "patk", "mult")
         {
             Range = new NumRange(.05, .1);
         }
         
         public double CurrentValue()
         {
             return Math.Ceiling((double)StatValue.GetValue() * Multiplier);
         }
     }

     public sealed class HealthPercentIncrease : Stat
     {
         public HealthPercentIncrease() : base("HP % Increase","php", "mult")
         {
             Range = new NumRange(.05, .1);
         }
         
         public double CurrentValue()
         {
             return Math.Ceiling((double)StatValue.GetValue() * Multiplier);
         }
     }
}