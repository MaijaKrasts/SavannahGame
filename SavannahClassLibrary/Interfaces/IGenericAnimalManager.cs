﻿namespace SavannahClassLibrary
{
    using System.Collections.Generic;
    using SavannahClassLibrary.Models;

    public interface IGenericAnimalManager
    {
        List<Animal> AdditionalAnimalList(Field field);

        Animal FindInField(Field field, int coordX, int coordY);

        Animal FindInList(List<Animal> additionalAnimal, int coordX, int coordY);
        
        void SetEnemies(Animal firstAnimal, Animal secondAnimal, Field field);

        void ResetEnemies(Animal firstAnimal, Animal secondAnimal, Field field);

        void LocateEnemy(Field field, List<Animal> additionslField);

        void LocateFriend(Field field, List<Animal> additionslField);

        bool BreedingValidator(Animal animal, Animal closestAnimal, Field field);

        Animal Breed(Animal animal, Field field);

        void ResetMatingValues(Animal animal);

        void IncreaseHealth(Animal animal);

        void DecreaseHealth(Animal animal);

        Animal TakeAStep(int nextStepX, int nextStepY, Animal animal, Field field);

    }
}