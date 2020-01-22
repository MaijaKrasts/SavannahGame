namespace Savannah.Models
{
    using Savannah.Interfaces;

    public class Antelope : IAnimal
    {
        public bool Alive { get; set ; }

        public int CoordinateX { get; set; }

        public int CoordinateY { get; set; }

        public string Symbol { get => "A";  }

        public IAnimal ClosestEnemy { get; set; }
    }
}
