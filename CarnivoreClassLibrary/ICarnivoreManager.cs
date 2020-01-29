namespace CarnivoreClassLibrary.Interfaces
{
    using SavannahClassLibrary.Interfaces;
    using SavannahClassLibrary.Models;

    public interface ICarnivoreManager : IAnimalManager
    {
        void EatVictim(Animal carnivore, Field field);
    }
}
