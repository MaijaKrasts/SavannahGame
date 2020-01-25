namespace Savannah
{
    using System.Collections.Generic;
    using System.Linq;
    using Savannah.Interfaces;
    using Savannah.Models;
    using Savannah.Static;

    public class GenericAnimalManager : IGenericAnimalManager
    {
        private int mateScore = 0;

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
            List<Animal> additionalAnimals = field.Animals;
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
            double ultimateLocation = _math.Vector(0, field.Width, 0, field.Height);

            foreach (var animal in field.Animals)
            {
                for (int coordX = -1; coordX < 2; coordX++)
                {
                    for (int coordY = -1; coordY < 2; coordY++)
                    {

                        int nextStepX = animal.CoordinateX + coordX;
                        int nextStepY = animal.CoordinateY + coordY;

                        var isThereAnimal = _validator.AnimalExists(nextStepX, nextStepY, field);
                        Animal closestAnimal = field.Animals.Find(a => a.CoordinateX == nextStepX && a.CoordinateY == nextStepY);

                        if (closestAnimal.Herbivore == animal.Herbivore)
                        {
                            if (animal.ClosestMate != null)
                            {
                                if (animal.ClosestMate == closestAnimal)
                                {
                                    mateScore++;
                                    if (mateScore == 3)
                                    {
                                        Breed(animal);
                                    }
                                }
                            }
                            else
                            {
                                animal.ClosestMate = closestAnimal;
                                mateScore = 1;
                            }
                        }
                    }
                }
            }
        }

        public void Breed(Animal animal)
        {
            IncreaseHealth(animal);
            IncreaseHealth(animal.ClosestMate);

            //if(animal.Herbivore = true)
            //{
            //    _animalFactory.
            //}


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
