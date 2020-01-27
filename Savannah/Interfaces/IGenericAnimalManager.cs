namespace Savannah
{
    using System.Collections.Generic;
    using Savannah.Models;

    public interface IGenericAnimalManager
    {
        List<Animal> AdditionalAnimalList(Field field);

        Animal FindInField(Field field, int coordX, int coordY);

        Animal FindInList(List<Animal> additionalAnimal, int coordX, int coordY);

        void LocateEnemy(Field field);

        void LocateFriend(Field field);

        bool BreedingValidator(Animal animal, Animal closestAnimal, Field field);

        Animal Breed(Animal animal, Field field);

        void IncreaseHealth(Animal animal);

        void DecreaseHealth(Animal animal);

        void TakeAStep(int nextStepX, int nextStepY, Animal animal);

    }
}