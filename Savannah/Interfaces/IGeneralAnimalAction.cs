namespace Savannah.Interfaces
{
    using System.Collections.Generic;
    using Savannah.Models;

    public interface IGeneralAnimalAction
    {
        List<IAnimal> AdditionalAnimalField(Field field);

        bool AnimalExists(int coordinateX, int coordinateY);

        bool LionExists(int coordinateX, int coordinateY);

        bool AntelopeExists(int coordinateX, int coordinateY);

        bool AnimalOutOfField(int coordinateX, int coordinateY, Field field);

        double LocateSingle(int animalX, int animalY, int enemyX, int enemyY);
    }
}
