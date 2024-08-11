namespace DepthChart.Domain.Entities
{
    public class Player
    {
        public int Number { get; }
        public string Name { get; }
        public string Position { get; }

        public Player(int number, string name, string position)
        {
            Number = number;
            Name = name;
            Position = position;
        }
    }
}
