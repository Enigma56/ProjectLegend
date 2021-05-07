namespace ProjectLegend.PlayerClasses
{
    public abstract class Player
    {
        public int Health { get; set; }
        public int Attack { get; set; }
        public override string ToString()
        {
            return $"Stats:\nHealth = {Health}, Attack = {Attack}";
        }
    }
}