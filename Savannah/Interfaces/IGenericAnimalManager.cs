namespace Savannah
{
    using System.Collections.Generic;
    using Savannah.Models;

    public interface IGenericAnimalManager
    {
        List<Animal> CopyList(Field field);

        Animal FindInField(Field field, int coordX, int coordY);

        Animal FindInList(List<Animal> searchList, int coordX, int coordY);
        
        void SetEnemies(Animal firstAnimal, Animal secondAnimal, Field field);

        void ResetEnemies(Animal firstAnimal, Animal secondAnimal, Field field);

        void LocateEnemy(Field field, List<Animal> searchList);

        void LocateFriend(Field field, List<Animal> searchList);

        bool BreedingValidator(Animal animal, Animal closestAnimal, Field field);

        Animal Breed(Animal animal, Field field);

        void ResetMatingValues(Animal animal);

        void IncreaseHealth(Animal animal);

        void DecreaseHealth(Animal animal);

        Animal TakeAStep(int nextStepX, int nextStepY, Animal animal, Field field);

    }
}