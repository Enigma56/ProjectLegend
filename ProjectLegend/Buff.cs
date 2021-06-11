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
        
        public bool Applied { get; set; }
        
        public Action<Player> ApplyEffect { get; set; }
        public Action<Player> RemoveEffect { get; set; }
        

        public Buff(string name, int duration)
        {
            Name = name;
            Duration = duration;
            Id = Guid.NewGuid();
        }

        public void MinusOneTurn()
        {
            TurnsRemaining -= 1;
        }

        public void RefreshBuff()
        {
            TurnsRemaining = Duration;
        }
        
        //Make it such that you can Have a buff to be added and removed
        
        public delegate void BuffEffect();
        public void Apply(Player player)
        {
            TurnsRemaining = Duration;
            player.Buffs.Add(this);
            
            try
            {
                ApplyEffect(player);
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Tried to apply an effect but couldn't execute application!");
                //Environment.Exit(1);
            }
        }
        
        public void Remove(Player player)
        {  
            player.Buffs.Remove(this);
            try
            {
                RemoveEffect(player);
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Tried to remove an effect but couldn't execute removal");
                //Environment.Exit(1);
            }
        }
        
        public override string ToString()
        {
            return Name;
        }
    }
}