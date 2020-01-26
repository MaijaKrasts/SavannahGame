namespace Savannah.Interfaces
{
    using System.Collections.Generic;
    using Savannah.Models;

    public interface ICarnivoreManager : IAnimalManager
    {
        Animal EatVictim(Animal carnivore, List<Animal> additionalField);
    }
}
