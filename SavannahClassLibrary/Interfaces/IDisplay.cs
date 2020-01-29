namespace SavannahClassLibrary.Interfaces
{
    using System.Collections.Generic;
    using SavannahClassLibrary.Models;

    public interface IDisplay
    {
        void DrawAnimals(Field field, List<Animal> additionalAnimal);
        void ResetValues(Field field, List<Animal> additionalAnimal);
    }
}
