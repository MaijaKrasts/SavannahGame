namespace Savannah.Models
{
    using Savannah.Interfaces;

    public class Lion : IAnimal
    {
        public bool Alive { get; set; }

        public int CoordinateX { get; set; }

        public int CoordinateY { get; set; }

        public string Symbol { get => "L"; }

        public IAnimal ClosestEnemy { get; set; }
    }
}
