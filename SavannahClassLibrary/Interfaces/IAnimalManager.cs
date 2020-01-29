namespace SavannahClassLibrary.Interfaces
{
    using System.Collections.Generic;
    using SavannahClassLibrary.Models;

    public interface IAnimalManager
    { 
        List<Animal> ChooseTheMove(List<Animal> additionalField, Field field);

        List<Animal> MoveWithoutEnemies(Animal animal, List<Animal> additionalField, Field field);

        List<Animal> MoveWithEnemies(Animal animal, List<Animal> additionalField, Field field);
    }
}
