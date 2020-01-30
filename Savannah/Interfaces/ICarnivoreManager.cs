namespace Savannah.Interfaces
{
    using System.Collections.Generic;
    using AnimalClassLibrary;
    using Savannah.Models;

    public interface ICarnivoreManager : IAnimalManager
    {
        void EatVictim(Animal carnivore, Field field);
    }
}
