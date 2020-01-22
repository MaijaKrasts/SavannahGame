namespace Savannah.Interfaces
{
    using System.Collections.Generic;
    using Savannah.Models;

    public interface IAnimalAction
    { 
        IAnimal Create(Field field);

        void Locate(Field field);

        Field MoveWithoutEnemies(Field field, List<IAnimal> additionalField);

        IAnimal MoveWithEnemies(IAnimal animal, List<IAnimal> additionalField);
    }
}
