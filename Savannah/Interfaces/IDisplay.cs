namespace Savannah.Interfaces
{
    using System.Collections.Generic;
    using Savannah.Models;

    public interface IDisplay
    {
        void DrawAnimals(Field field, List<Animal> additionalAnimal);
        void ResetValues(Field field, List<Animal> additionalAnimal);
    }
}
