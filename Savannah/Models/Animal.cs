using Savannah.Interfaces;

namespace Savannah.Models
{
    public class Animal : IAnimal
    {
        public bool Alive { get; set; }

        public int CoordinateX { get; set; }

        public int CoordinateY { get; set; }

        public string Symbol { get; set; }

        public bool Herbivore { get; set; }

        public Animal ClosestEnemy { get; set; }
    }
}
