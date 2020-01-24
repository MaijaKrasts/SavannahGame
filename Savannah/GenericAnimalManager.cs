namespace Savannah
{
    using System.Collections.Generic;
    using Savannah.Models;

    public class GenericAnimalManager : IGenericAnimalManager
    {
        public List<Animal> AdditionalAnimalList(Field field)
        {
            List<Animal> additionalAnimals = field.Animals;
            return additionalAnimals;
        }

        public void Breed()
        {

        }

        public void IncreaseHealth(Animal animal)
        {
            animal.Health += 3;
        }

        public void DecreaseHealth(Animal animal)
        {
            animal.Health--;

            if (animal.Health == 0)
            {
                animal.Alive = false;
            }
        }
    }
}
