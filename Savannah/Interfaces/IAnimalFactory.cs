namespace Savannah.Interfaces
{
    using System;
    using AnimalClassLibrary;
    using Savannah.Models;

    public interface IAnimalFactory
    {
        Animal CreateAnimal(ConsoleKey key, Field field);
    }
}
