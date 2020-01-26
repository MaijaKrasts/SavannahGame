namespace Savannah.Interfaces
{
    using System;
    using Savannah.Models;

    public interface IAnimalFactory
    {
        Animal CreateAnimal(ConsoleKey key, Field field);
    }
}
