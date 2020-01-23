namespace Savannah.Interfaces
{
    using System.Collections.Generic;
    using Savannah.Models;

    public interface IDisplay
    {
        void DrawAnimals(Field field, List<IAnimal> additionalAnimal);
    }
}
