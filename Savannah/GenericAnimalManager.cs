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
            List<Animal> additionalAnimals = new List<Animal>();
            additionalAnimals = field.Animals;
            return additionalAnimals;
        }

        public void LocateEnemy(Field field)
        {
            double ultimateLocation = _math.Vector(0, field.Width, 0, field.Height);

            var herbivoreList = field.Animals.FindAll(a => a.Herbivore == true).ToList();
            var carnivoreList = field.Animals.FindAll(a => a.Herbivore == false).ToList();

            foreach (var herbivore in herbivoreList)
            {
                foreach (var carnivore in carnivoreList)
                {
                    var location = _math.Vector(herbivore.CoordinateX, carnivore.CoordinateX, herbivore.CoordinateY, carnivore.CoordinateY);

                    if (location <= ultimateLocation)
                    {
                        if (location < NumberParameters.VisionRange)
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
            }
        }

        public void LocateFriend(Field field)
        {
            var animalList = field.Animals.ToList();

            foreach (var animal in animalList)
            {
                for (int coordX = -2; coordX < 3; coordX++)
                {
                    for (int coordY = -2; coordY < 3; coordY++)
                    {
                        int nextStepX = animal.CoordinateX + coordX;
                        int nextStepY = animal.CoordinateY + coordY;

                        if(coordX != 0 && coordY != 0)
                        {
                            Animal closestAnimal = field.Animals.Find(a => a.CoordinateX == nextStepX && a.CoordinateY == nextStepY);

                            if (_validator.AnimalExists(nextStepX, nextStepY, field) && closestAnimal.Herbivore == animal.Herbivore)
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
                    animal.MatingCount = 1;
                }
            else if (animal.ClosestMate != null)
                {
                    if (animal.ClosestMate == closestAnimal)
                    {
                        animal.MatingCount++;
                        if (animal.MatingCount == 3)
                        {
                            isAnimalBreedable = true;
                            Breed(animal, field);
                        animal.MatingCount = 0;
                        }
                    }
                    else
                    {
                        animal.ClosestMate = closestAnimal;
                        animal.MatingCount = 1;
                    }
                }

            return isAnimalBreedable;
        }

        public Animal Breed(Animal animal, Field field)
        {
            var newAnimal = new Animal();

            if (animal.Herbivore == true)
            {
                newAnimal = _animalFactory.CreateAnimal(TextParameters.AntelopeKey, field);
            }
            else if (animal.Herbivore == false)
            {
                newAnimal = _animalFactory.CreateAnimal(TextParameters.LionKey, field);
            }

            return newAnimal;
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

        public void TakeAStep(int nextStepX, int nextStepY, Animal animal)
        {
            animal.CoordinateX = nextStepX;
            animal.CoordinateY = nextStepY;
        }

        public void Breed()
        {
            throw new System.NotImplementedException();
        }
    }
}
