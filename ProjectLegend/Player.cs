namespace ProjectLegend
{
    public class Player
    {

        public Player(int health, int attack)
        {
            Health = health;
            Attack = attack;
        }

        public int Health { get; set; }

        public int Attack { get; set; }
        public override string ToString()
        {
            return $"Player Stats:\nHealth = {Health}, Attack = {Attack}";
        }
    }
}