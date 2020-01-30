using Savannah.Interfaces;
using System;

namespace Savannah.Models
{
    [Serializable]
    public class Animal : IAnimal
    {
        public bool Alive { get; set; }

        public int CoordinateX { get; set; }

        public int CoordinateY { get; set; }

        public string Symbol { get; set; }

        public ConsoleKey Key { get; set; }

        public bool Herbivore { get; set; }

        public Animal ClosestEnemy { get; set; }

        public Animal ClosestMate { get ; set; }

        public int MatingCount { get; set; }

        public int Health {get; set; }
    }
}
