namespace Savannah
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Savannah.Interfaces;
    using Savannah.Models;

    public class HerbivoreManager : IHerbivoreManager
    {
        private IAnimalValidator _validator;
        private ICalculations _math;
        private IConsoleFacade _facade;
        private Random rnd;
        private IGenericAnimalManager _genericAnimal;

        public HerbivoreManager(IAnimalValidator generalAction, ICalculations math, IConsoleFacade facade, IGenericAnimalManager genericAnimal)
        {
            _validator = generalAction;
            _math = math;
            _facade = facade;
            _genericAnimal = genericAnimal;
        }

        public List<Animal> ChooseTheMove(List<Animal> additionalField, Field field)
        {
            var herbivoreList = field.Animals.FindAll(a => a.Herbivore == true).ToList();

            foreach (var herbivore in herbivoreList)
            {
                if (herbivore.ClosestEnemy == null)
                {
                    additionalField = MoveWithoutEnemies(herbivore, additionalField, field);
                }
                else if (herbivore.ClosestEnemy != null)
                {
                    additionalField = MoveWithEnemies(herbivore, additionalField, field);
                }

                _genericAnimal.DecreaseHealth(herbivore);
            }

            return additionalField;
        }

        public List<Animal> MoveWithoutEnemies(Animal herbivore, List<Animal> additionalField, Field field)
        {
            bool foundMove = false;

            while (!foundMove)
            {
                int moveX = _facade.GetRandom(-1, 2);
                int moveY = _facade.GetRandom(-1, 2);

                int nextStepX = herbivore.CoordinateX + moveX;
                int nextStepY = herbivore.CoordinateY + moveY;

                var validMove = _validator.ValidateMove(nextStepX, nextStepY, field)
                    && !_validator.AnimalExists(nextStepX, nextStepY, field);

                if (validMove)
                {
                    foundMove = true;
                }

                _genericAnimal.TakeAStep(nextStepX, nextStepY, herbivore);
            }

            return additionalField;
        }

        public List<Animal> MoveWithEnemies(Animal herbivore, List<Animal> additionalField, Field field)
        {
            double closestLocation = 0;

            for (int coordX = -1; coordX < 2; coordX++)
            {
                for (int coordY = -1; coordY < 2; coordY++)
                {
                    int nextStepX = herbivore.CoordinateX + coordX;
                    int nextStepY = herbivore.CoordinateY + coordY;

                    var validMove = _validator.ValidateMove(nextStepX, nextStepY, field)
                            && !_validator.AnimalExists(nextStepX, nextStepY, field);

                    if (validMove)
                    {
                        double betterLocation = _math.Vector(nextStepX, nextStepY, herbivore.ClosestEnemy.CoordinateX, herbivore.ClosestEnemy.CoordinateY);
                        if (betterLocation >= closestLocation)
                        {
                            closestLocation = betterLocation;
                            _genericAnimal.TakeAStep(nextStepX, nextStepY, herbivore);
                        }
                    }
                }
            }
            return additionalField;
        }
    }
}
