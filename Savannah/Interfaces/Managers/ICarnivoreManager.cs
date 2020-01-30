namespace Savannah.Interfaces
{
    using AnimalLibrary;
    using Savannah.Models;

    public interface ICarnivoreManager : IAnimalManager
    {
        void EatVictim(Animal carnivore, Field field);
    }
}
