namespace Savannah
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Savannah.Interfaces;
    using Savannah.Models;
    using Savannah.Static;

    public class CarnivoreManager : IAnimalManager
    {
        private IAnimalValidator _validator;
        private ICalculations _math;
        private IConsoleFacade _facade;
        private Random rnd;
        private IGenericAnimalManager _genericAnimal;

        public CarnivoreManager(IAnimalValidator validator, ICalculations math, IConsoleFacade facade, IGenericAnimalManager genericAnimal)
        {
            _validator = validator;
            _math = math;
            _facade = facade;
            rnd = _facade.GetRandom();
            _genericAnimal = genericAnimal;
        }

        public void Locate(Field field)
        {
            double ultimateLocation = _math.Vector(0, field.Width, 0, field.Height);

            var herbivoreList = field.Animals.FindAll(a => a.Herbivore == true).ToList();
            var carnivoreList = field.Animals.FindAll(a => a.Herbivore == false).ToList();

            foreach (var carnivore in carnivoreList)
            {
                foreach (var herbivore in herbivoreList)
                {
                    var location = _math.Vector(herbivore.CoordinateX, carnivore.CoordinateX, herbivore.CoordinateY, carnivore.CoordinateY);

                    if (location <= ultimateLocation)
                    {
                        if (location < NumberParameters.VisionRange)
                        {
                            ultimateLocation = location;
                            carnivore.ClosestEnemy = herbivore;
                        }
                        else
                        {
                            carnivore.ClosestEnemy = null;
                        }
                    }
                }
            }
        }

        public List<Animal> ChooseTheMove(List<Animal> additionalField, Field field)
        {
            var carnivoreList = field.Animals.FindAll(a => a.Herbivore == false).ToList();

            foreach (var carnivore in carnivoreList)
            {
                if (carnivore.ClosestEnemy == null)
                {
                    additionalField = MoveWithoutEnemies(carnivore, additionalField, field);
                }
                else if (carnivore.ClosestEnemy != null)
                {
                    additionalField = MoveWithEnemies(carnivore, additionalField, field);
                }

                _genericAnimal.DecreaseHealth(carnivore);
            }

            return additionalField;
        }

        public List<Animal> MoveWithoutEnemies(Animal carnivore, List<Animal> additionalField, Field field)
        {
            bool foundMove = false;

            while (!foundMove)
            {
                int moveX = rnd.Next(-1, 2);
                int moveY = rnd.Next(-1, 2);

                int nextStepX = carnivore.CoordinateX + moveX;
                int nextStepY = carnivore.CoordinateY + moveY;

                var validMove = _validator.ValidateMove(nextStepX, nextStepY, field)
                    && !_validator.AnimalExists(nextStepX, nextStepY, field);

                if (validMove)
                {
                    foundMove = true;
                }

                carnivore.CoordinateX = nextStepX;
                carnivore.CoordinateY = nextStepY;
            }

            return additionalField;
        }

        public List<Animal> MoveWithEnemies(Animal carnivore, List<Animal> additionalField, Field field)
        {
            var initialLocation = _math.Vector(carnivore.CoordinateX, carnivore.CoordinateY, carnivore.ClosestEnemy.CoordinateX, carnivore.ClosestEnemy.CoordinateY);

            for (int coordX = -1; coordX < 2; coordX++)
            {
                for (int coordY = -1; coordY < 2; coordY++)
                {
                    int nextStepX = carnivore.CoordinateX + coordX;
                    int nextStepY = carnivore.CoordinateY + coordY;

                    var validMove = _validator.ValidateMove(nextStepX, nextStepY, field)
                        && !_validator.CarnivoreExists(nextStepX, nextStepY, field);

                    if (validMove)
                    {
                        double betterLocation = _math.Vector(nextStepX, nextStepY, carnivore.ClosestEnemy.CoordinateX, carnivore.ClosestEnemy.CoordinateY);
                        if (betterLocation < initialLocation)
                        {
                            initialLocation = betterLocation;
                            carnivore.CoordinateX = nextStepX;
                            carnivore.CoordinateY = nextStepY;

                            if (_validator.HerbivoreExists(carnivore.CoordinateY, carnivore.CoordinateY, field))
                            {
                                EatVictim(carnivore, additionalField);
                                carnivore.CoordinateX = nextStepX;
                                carnivore.CoordinateY = nextStepY;
                                break;
                            }
                        }
                    }
                }
            }

            return additionalField;
        }

        public void EatVictim(Animal carnivore, List<Animal> additionalField)
        {
            additionalField.Remove(carnivore.ClosestEnemy);
            carnivore.ClosestEnemy.Alive = false;
        }
    }
}
