using System;
using ProjectLegend.CharacterClasses;
using ProjectLegend.GameUtilities;

namespace ProjectLegend.GameUtilities.BuffUtilities
{
    public class Buff
    {
        public Guid Id { get; }
        public string Name { get; }
        public int Duration { get; }
        public int TurnsRemaining { get; set; }
        
        public bool HasTurnEffect { get; set; }
        
        public bool Applied { get; set; }
        
        public Action<Character> ApplyEffect { get; set; }
        public Action<Character> RemoveEffect { get; set; }


        public Buff(string name, int duration)
        {
            Name = name;
            Duration = duration;
            TurnsRemaining = Duration;
            Id = Guid.NewGuid();
        }

        public void Apply(Character character, int energyConsumption = -1)
        {
            try
            {
                if (character is Player player)
                {
                    if (player.CurrentEnergy >= energyConsumption)
                    {
                        player.CurrentEnergy -= energyConsumption;
                        if (HasBuff(character))
                        {
                            RefreshBuff();
                            Console.WriteLine($"{this} has been refreshed!");
                        }
                        else
                        {
                            character.Buffs.Add(this);
                            if(HasTurnEffect is false) //Differentiates between static and dynamic buff effects
                                ApplyEffect(player);
                            Console.WriteLine($"{this} has been applied!");
                            Applied = true;
                            Utils.Separator('*');
                        }
                    }
                    else
                    {
                        Console.WriteLine("You do not have enough energy!");
                    }
                }
                else if (character is Enemy enemy)
                {
                    enemy.Buffs.Add(this);
                    ApplyEffect(enemy);
                    Applied = true;
                }
            }
            catch (NullReferenceException)
            {
                Console.WriteLine($"Please set an application effect for {Name}!");
            }
        }

        public void Remove(Character character)
        {  
            character.Buffs.Remove(this);

            try
            {
                RemoveEffect(character);
            }
            catch (NullReferenceException)
            {
                Console.WriteLine($"Please create a remove effect for {Name}");
            }
        }

        public bool HasBuff(Character player)
        {
            if (player.Buffs.Contains(this) && Applied)
                return true;
            else
                return false;
        }
        
        public void MinusOneTurn()
        {
            TurnsRemaining -= 1;
        }

        private void RefreshBuff()
        {
            TurnsRemaining = Duration;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}