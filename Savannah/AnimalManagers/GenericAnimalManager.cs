namespace Savannah
{
    using System.Collections.Generic;
    using System.Linq;
    using Savannah.Interfaces;
    using Savannah.Models;
    using Savannah.Static;

    public class GenericAnimalManager : IGenericAnimalManager
    {
        private IAnimalValidator _validator;
        private ICalculations _math;
        private IAnimalFactory _animalFactory;

        public GenericAnimalManager(ICalculations math, IAnimalValidator validator, IAnimalFactory animalFactory)
        {
            _math = math;
            _validator = validator;
            _animalFactory = animalFactory;
        }

        public List<Animal> AdditionalAnimalList(Field field)
        {
            List<Animal> additionalAnimal = new List<Animal>();

            foreach (var animal in field.Animals)
            {
                additionalAnimal.Add(animal);
            }

            return additionalAnimal;
        }

        public Animal FindInField(Field field, int coordX, int coordY)
        {
            var animal = field.Animals.Find(a => a.CoordinateX == coordX && a.CoordinateY == coordY);
            return animal;
        }

        public Animal FindInList(List<Animal> additionalAnimal, int coordX, int coordY)
        {
            var animal = additionalAnimal.Find(u => u.CoordinateX == coordX && u.CoordinateY == coordY);
            return animal;
        }

        public void LocateEnemy(Field field)
        {
            double ultimateLocation = _math.Vector(0, field.Width, 0, field.Height);

            var carnivoreList = field.Animals.FindAll(a => a.Herbivore == false).ToList();
            var herbivoreList = field.Animals.FindAll(a => a.Herbivore == true).ToList();

            foreach (var carnivore in carnivoreList)
            {
                foreach (var herbivore in herbivoreList)
                {
                    var location = _math.Vector(herbivore.CoordinateX, carnivore.CoordinateX, herbivore.CoordinateY, carnivore.CoordinateY);

                    if (location < ultimateLocation)
                    {
                        if (location < NumParameters.VisionRange)
                        {
                            ultimateLocation = location;
                            herbivore.ClosestEnemy = carnivore;
                            carnivore.ClosestEnemy = herbivore;
                        }
                        else
                        {
                            carnivore.ClosestEnemy = null;
                            herbivore.ClosestEnemy = null;
                        }
                    }
                }

               ultimateLocation = _math.Vector(0, field.Width, 0, field.Height);
            }
        }

        public void LocateFriend(Field field)
        {
            var animalList = field.Animals.ToList();

            foreach (var animal in animalList)
            {
                for (int coordX = NumParameters.BreedingNegative; coordX < NumParameters.BreedingPositive; coordX++)
                {
                    for (int coordY = NumParameters.BreedingNegative; coordY < NumParameters.BreedingPositive; coordY++)
                    {
                        int nextStepX = animal.CoordinateX + coordX;
                        int nextStepY = animal.CoordinateY + coordY;

                        bool exactAnimal = coordX == 0 && coordY == 0;

                        if(!exactAnimal)
                        {
                            Animal closestAnimal = FindInField(field, nextStepX, nextStepY);
                            var validBreeder = _validator.AnimalExists(nextStepX, nextStepY, field)
                                && closestAnimal.Herbivore == animal.Herbivore;

                            if (validBreeder)
                            {
                                BreedingValidator(animal, closestAnimal, field);
                            }
                        }
                    }
                }
            }
        }

        public bool BreedingValidator(Animal animal, Animal closestAnimal, Field field)
        {
            bool isAnimalBreedable = false;

            if (animal.ClosestMate == null)
                {
                    animal.ClosestMate = closestAnimal;
                    animal.MatingCount = NumParameters.ActiveMatingCount;
                }
            else if (animal.ClosestMate != null)
                {
                if (animal.ClosestMate == closestAnimal)
                {
                    animal.MatingCount++;
                    if (animal.MatingCount == NumParameters.MaxMatingCount)
                    {
                        isAnimalBreedable = true;
                        Breed(animal, field);
                    }
                }
                else
                {
                    animal.ClosestMate = closestAnimal;
                    animal.MatingCount = NumParameters.ActiveMatingCount;
                }
            }

            return isAnimalBreedable;
        }

        public Animal Breed(Animal animal, Field field)
        {
            ResetMatingValues(animal);

            var newAnimal = _animalFactory.CreateAnimal(animal.Key, field);
            return newAnimal;
        }

        private void ResetMatingValues(Animal animal)
        {
            animal.ClosestMate.ClosestMate = null;
            animal.ClosestMate.MatingCount = NumParameters.InitialMatingCount;
            animal.ClosestMate = null;
            animal.MatingCount = NumParameters.InitialMatingCount;
        }

        public void IncreaseHealth(Animal animal)
        {
            animal.Health += NumParameters.ExtendLife;
        }

        public void DecreaseHealth(Animal animal)
        {
            animal.Health--;

            if (animal.Health == NumParameters.Die)
            {
                animal.Alive = false;
            }
        }

        public List<Animal> TakeAStep(int nextStepX, int nextStepY, Animal animal, List<Animal> additionalAnimal)
        {
            var savedAnimal = FindInList(additionalAnimal, animal.CoordinateX, animal.CoordinateY);
            savedAnimal.CoordinateX = nextStepX;
            savedAnimal.CoordinateY = nextStepY;

            return additionalAnimal;
        }
    }
}
