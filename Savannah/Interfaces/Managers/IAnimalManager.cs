namespace Savannah.Interfaces
{
    using System.Collections.Generic;
    using AnimalLibrary;
    using Savannah.Models;

    public interface IAnimalManager
    { 
        List<Animal> ChooseTheMove(List<Animal> searchList, Field field);

        List<Animal> MoveWithoutEnemies(Animal animal, List<Animal> searchList, Field field);

        List<Animal> MoveWithEnemies(Animal animal, List<Animal> searchList, Field field);
    }
}
