namespace Savannah.Interfaces
{
    using System.Collections.Generic;
    using Savannah.Models;

    public interface IAnimalValidator
    {
        List<Animal> AdditionalAnimalField(Field field);

        bool AnimalExists(int coordinateX, int coordinateY, Field field);

        bool CarnivoreExists(int coordinateX, int coordinateY, Field field);

        bool HerbivoreExists(int coordinateX, int coordinateY, Field field);

        bool AnimalOutOfField(int coordinateX, int coordinateY, Field field);
    }
}
