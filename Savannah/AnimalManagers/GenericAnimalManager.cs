namespace Savannah
{
    using System.Collections.Generic;
    using System.Linq;
    using AnimalLibrary;
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

        public List<Animal> CopyList(Field field)
        {
            List<Animal> searchList = new List<Animal>();

            searchList = DeepCopy.DeepClone(field.Animals);
            return searchList;
        }

        public Animal FindInField(Field field, int coordX, int coordY)
        {
            var animal = field.Animals.Find(a => a.CoordinateX == coordX && a.CoordinateY == coordY);
            return animal;
        }

        public Animal FindInList(List<Animal> searchList, int coordX, int coordY)
        {
            var animal = searchList.Find(u => u.CoordinateX == coordX && u.CoordinateY == coordY);
            return animal;
        }

        public void LocateEnemy(Field field, List<Animal> searchList)
        {
            double ultimateLocation = _math.Vector(0, field.Width, 0, field.Height);

            var carnivoreList = searchList.FindAll(a => a.IsHerbivore == false);
            var herbivoreList = searchList.FindAll(a => a.IsHerbivore == true);

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
                            SetEnemies(carnivore, herbivore, field);
                        }
                        else
                        {
                            ResetEnemies(carnivore, herbivore, field);
                        }
                    }
                }

                ultimateLocation = _math.Vector(0, field.Width, 0, field.Height);
            }
        }

        public void SetEnemies(Animal firstAnimal, Animal secondAnimal, Field field)
        {
            var savedFirstAnimal = FindInField(field, firstAnimal.CoordinateX, firstAnimal.CoordinateY);
            var savedSecondAnimal = FindInField(field, secondAnimal.CoordinateX, secondAnimal.CoordinateY);

            savedFirstAnimal.ClosestEnemy = savedSecondAnimal;
        }

        public void ResetEnemies(Animal firstAnimal, Animal secondAnimal, Field field)
        {

            var savedFirstAnimal = FindInField(field, firstAnimal.CoordinateX, firstAnimal.CoordinateY);

            savedFirstAnimal.ClosestEnemy = null;
        }

        public void LocateFriend(Field field, List<Animal> searchList)
        {
            foreach (var animal in searchList)
            {
                for (int coordX = NumParameters.BreedingNegative; coordX < NumParameters.BreedingPositive; coordX++)
                {
                    for (int coordY = NumParameters.BreedingNegative; coordY < NumParameters.BreedingPositive; coordY++)
                    {
                        int nextStepX = animal.CoordinateX + coordX;
                        int nextStepY = animal.CoordinateY + coordY;

                        bool exactAnimal = coordX == 0 && coordY == 0;

                        if (!exactAnimal)
                        {
                            Animal closestAnimal = FindInField(field, nextStepX, nextStepY);
                            var validBreeder = _validator.AnimalExists(nextStepX, nextStepY, field)
                                && closestAnimal.IsHerbivore == animal.IsHerbivore;

                            if (validBreeder)
                            {
                                var currentAnimal = FindInField(field, animal.CoordinateX, animal.CoordinateY);
                                BreedingValidator(currentAnimal, closestAnimal, field);
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

        public void ResetMatingValues(Animal animal)
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

        public Animal TakeAStep(int nextStepX, int nextStepY, Animal animal, Field field)
        {
            var savedAnimal = FindInField(field, animal.CoordinateY, animal.CoordinateX);

            if (savedAnimal == null)
            {
                savedAnimal = FindInField(field, animal.CoordinateX, animal.CoordinateY);
            }

            savedAnimal.CoordinateX = nextStepX;
            savedAnimal.CoordinateY = nextStepY;

            return savedAnimal;
        }
    }
}