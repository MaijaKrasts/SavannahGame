namespace SavannahClassLibrary.Interfaces
{
    using System;
    using SavannahClassLibrary.Models;

    public interface IAnimalFactory
    {
        Animal CreateAnimal(ConsoleKey key, Field field);
    }
}
