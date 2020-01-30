using System;

namespace AnimalClassLibrary
{
    public interface IAnimal
    {
        bool Alive { get; set; }

        int CoordinateX { get; set; }

        int CoordinateY { get; set; }

        string Symbol { get; set; }

        bool IsHerbivore { get; set; }

        ConsoleKey Key { get; set; }

        Animal ClosestEnemy { get; set; }

        Animal ClosestMate { get; set; }

        int MatingCount { get; set; }

        int Health { get; set; }

    }
}
