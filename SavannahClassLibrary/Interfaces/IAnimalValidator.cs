namespace SavannahClassLibrary.Interfaces
{
    using SavannahClassLibrary.Models;

    public interface IAnimalValidator
    {
        bool AnimalExists(int coordinateX, int coordinateY, Field field);

        bool CarnivoreExists(int coordinateX, int coordinateY, Field field);

        bool HerbivoreExists(int coordinateX, int coordinateY, Field field);

        bool AnimalOutOfField(int coordinateX, int coordinateY, Field field);

        bool ValidateMove(int nextStepX, int nextStepY, Field field);
    }
}
