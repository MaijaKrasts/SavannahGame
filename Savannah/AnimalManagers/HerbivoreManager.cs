namespace Savannah
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Savannah.Interfaces;
    using Savannah.Models;
    using Savannah.Static;

    public class HerbivoreManager : IHerbivoreManager
    {
        private IAnimalValidator _validator;
        private ICalculations _math;
        private IConsoleFacade _facade;
        private IGenericAnimalManager _genericAnimal;

        public HerbivoreManager(IAnimalValidator generalAction, ICalculations math, IConsoleFacade facade, IGenericAnimalManager genericAnimal)
        {
            _validator = generalAction;
            _math = math;
            _facade = facade;
            _genericAnimal = genericAnimal;
        }

        public List<Animal> ChooseTheMove(List<Animal> searchList, Field field)
        {
            var herbivoreList = searchList.FindAll(a => a.Herbivore == true).ToList();

            foreach (var herbivore in herbivoreList)
            {
                if (herbivore.ClosestEnemy == null)
                {
                    searchList = MoveWithoutEnemies(herbivore, searchList, field);
                }
                else if (herbivore.ClosestEnemy != null)
                {
                    searchList = MoveWithEnemies(herbivore, searchList, field);
                }

                _genericAnimal.DecreaseHealth(herbivore);
            }

            return searchList;
        }

        public List<Animal> MoveWithoutEnemies(Animal herbivore, List<Animal> searchList, Field field)
        {
            bool foundMove = false;
            int bestStepX = herbivore.CoordinateX;
            int bestStepY = herbivore.CoordinateY;

            while (!foundMove)
            {
                int moveX = _facade.GetRandomMinMax(NumParameters.MovingNegative, NumParameters.MovingPositive);
                int moveY = _facade.GetRandomMinMax(NumParameters.MovingNegative, NumParameters.MovingPositive);

                int nextStepX = herbivore.CoordinateX + moveX;
                int nextStepY = herbivore.CoordinateY + moveY;

                var validMove = _validator.ValidateMove(nextStepX, nextStepY, field)
                    && !_validator.AnimalExists(nextStepX, nextStepY, field);

                if (validMove)
                {
                    foundMove = true;
                    bestStepX = nextStepX;
                    bestStepY = nextStepY;
                }
            }

            _genericAnimal.TakeAStep(bestStepX, bestStepY, herbivore, field);

            return searchList;
        }

        public List<Animal> MoveWithEnemies(Animal herbivore, List<Animal> searchList, Field field)
        {
            double closestLocation = 0;
            int bestStepX = herbivore.CoordinateX;
            int bestStepY = herbivore.CoordinateY;

            for (int coordX = NumParameters.MovingNegative; coordX < NumParameters.MovingPositive; coordX++)
            {
                for (int coordY = NumParameters.MovingNegative; coordY < NumParameters.MovingPositive; coordY++)
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

                            bestStepX = nextStepX;
                            bestStepY = nextStepY;
                        }
                    }
                }
            }

            _genericAnimal.TakeAStep(bestStepX, bestStepY, herbivore, field);
            return searchList;
        }
    }
}
