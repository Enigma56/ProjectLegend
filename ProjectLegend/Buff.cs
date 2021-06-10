using System;
using ProjectLegend.PlayerClasses;

namespace ProjectLegend
{
    public class Buff
    {
        public Guid Id { get; }
        public string Name { get; init; }
        public int Duration { get; init; }
        
        public int TurnsRemaining { get; set; }
        
        
        public Buff(string name, int duration)
        {
            Name = name;
            Duration = duration;
            Id = Guid.NewGuid();
        }

        public void MinusTick()
        {
            TurnsRemaining -= 1;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}