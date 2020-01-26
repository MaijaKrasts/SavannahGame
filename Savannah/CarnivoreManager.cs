namespace Savannah
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Savannah.Interfaces;
    using Savannah.Models;

    public class CarnivoreManager : ICarnivoreManager
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
            _genericAnimal = genericAnimal;
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
                int moveX = _facade.GetRandomMinMax(-1, 2);
                int moveY = _facade.GetRandomMinMax(-1, 2);

                int nextStepX = carnivore.CoordinateX + moveX;
                int nextStepY = carnivore.CoordinateY + moveY;

                var validMove = _validator.ValidateMove(nextStepX, nextStepY, field)
                    && !_validator.AnimalExists(nextStepX, nextStepY, field);

                if (validMove)
                {
                    foundMove = true;
                }

                _genericAnimal.TakeAStep(nextStepX, nextStepY, carnivore);
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
                            _genericAnimal.TakeAStep(nextStepX, nextStepY, carnivore);

                            if (_validator.HerbivoreExists(carnivore.CoordinateY, carnivore.CoordinateY, field))
                            {
                                EatVictim(carnivore, additionalField);
                                break;
                            }
                        }
                    }
                }
            }

            return additionalField;
        }

        public Animal EatVictim(Animal carnivore, List<Animal> additionalField)
        {
            additionalField.Remove(carnivore.ClosestEnemy);
            carnivore.ClosestEnemy.Alive = false;

            return carnivore.ClosestEnemy;
        }
    }
}
