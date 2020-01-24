namespace Savannah
{
    using System.Collections.Generic;
    using Savannah.Models;

    public interface IGenericAnimalManager
    {
        List<Animal> AdditionalAnimalList(Field field);

        void Breed();

        void IncreaseHealth(Animal animal);

        void DecreaseHealth(Animal animal);

    }
}