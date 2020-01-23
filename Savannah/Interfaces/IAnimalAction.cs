namespace Savannah.Interfaces
{
    using System.Collections.Generic;
    using Savannah.Models;

    public interface IAnimalAction
    { 
        //IAnimal Create(Field field);

        void Locate(Field field);

        List<IAnimal> Move(List<IAnimal> additionalField, Field field);

        List<IAnimal> MoveWithoutEnemies(IAnimal animal, List<IAnimal> additionalField, Field field);

        List<IAnimal> MoveWithEnemies(IAnimal animal, List<IAnimal> additionalField, Field field);
    }
}
