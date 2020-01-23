namespace Savannah.Interfaces
{
    using System.Collections.Generic;
    using Savannah.Models;

    public interface IGeneralAnimalAction
    {
        List<IAnimal> AdditionalAnimalField(Field field);

        bool AnimalExists(int coordinateX, int coordinateY, Field field);

        bool CarnivoreExists(int coordinateX, int coordinateY, Field field);

        bool HerbivoreExists(int coordinateX, int coordinateY, Field field);

        bool AnimalOutOfField(int coordinateX, int coordinateY, Field field);
    }
}
