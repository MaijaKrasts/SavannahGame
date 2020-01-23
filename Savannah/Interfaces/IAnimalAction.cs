﻿namespace Savannah.Interfaces
{
    using System.Collections.Generic;
    using Savannah.Models;

    public interface IAnimalAction
    { 
        void Locate(Field field);

        List<Animal> Move(List<Animal> additionalField, Field field);

        List<Animal> MoveWithoutEnemies(Animal animal, List<Animal> additionalField, Field field);

        List<Animal> MoveWithEnemies(Animal animal, List<Animal> additionalField, Field field);
    }
}
