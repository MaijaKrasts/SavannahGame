namespace Savannah
{
    using System.Collections.Generic;
    using System.Linq;
    using AnimalClassLibrary;
    using Savannah.Interfaces;
    using Savannah.Models;
    using Savannah.Static;

    public class CarnivoreManager : ICarnivoreManager
    {
        private IAnimalValidator _validator;
        private ICalculations _math;
        private IConsoleFacade _facade;
        private IGenericAnimalManager _genericAnimal;

        public CarnivoreManager(IAnimalValidator validator, ICalculations math, IConsoleFacade facade, IGenericAnimalManager genericAnimal)
        {
            _validator = validator;
            _math = math;
            _facade = facade;
            _genericAnimal = genericAnimal;
        }

        public List<Animal> ChooseTheMove(List<Animal> seachList, Field field)
        {
            var carnivoreList = seachList.FindAll(a => a.IsHerbivore == false).ToList();

            foreach (var carnivore in carnivoreList)
            {
                if (carnivore.ClosestEnemy == null)
                {
                    seachList = MoveWithoutEnemies(carnivore, seachList, field);
                }
                else if (carnivore.ClosestEnemy != null)
                {
                    seachList = MoveWithEnemies(carnivore, seachList, field);
                }

                _genericAnimal.DecreaseHealth(carnivore);
            }

            return seachList;
        }

        public List<Animal> MoveWithoutEnemies(Animal carnivore, List<Animal> searchList, Field field)
        {
            bool foundMove = false;
            int bestStepX = carnivore.CoordinateX;
            int bestStepY = carnivore.CoordinateY;

            while (!foundMove)
            {
                int moveX = _facade.GetRandomMinMax(NumParameters.MovingNegative, NumParameters.MovingPositive);
                int moveY = _facade.GetRandomMinMax(NumParameters.MovingNegative, NumParameters.MovingPositive);

                int nextStepX = carnivore.CoordinateX + moveX;
                int nextStepY = carnivore.CoordinateY + moveY;

                var validMove = _validator.ValidateMove(nextStepX, nextStepY, field)
                    && !_validator.AnimalExists(nextStepX, nextStepY, field);

                if (validMove)
                {
                    foundMove = true;
                    bestStepX = nextStepX;
                    bestStepY = nextStepY;
                }
            }

            _genericAnimal.TakeAStep(bestStepX, bestStepY, carnivore, field);

            return searchList;
        }

        public List<Animal> MoveWithEnemies(Animal carnivore, List<Animal> searchList, Field field)
        {
            var initialLocation = _math.Vector(carnivore.CoordinateX, carnivore.CoordinateY, carnivore.ClosestEnemy.CoordinateX, carnivore.ClosestEnemy.CoordinateY);
            int bestStepX = carnivore.CoordinateX;
            int bestStepY = carnivore.CoordinateY;

            for (int coordX = NumParameters.MovingNegative; coordX < NumParameters.MovingPositive; coordX++)
            {
                for (int coordY = NumParameters.MovingNegative; coordY < NumParameters.MovingPositive; coordY++)
                {
                    int nextStepX = carnivore.CoordinateX + coordX;
                    int nextStepY = carnivore.CoordinateY + coordY;

                    var validMove = _validator.ValidateMove(nextStepX, nextStepY, field)
                        && !_validator.CarnivoreExists(nextStepX, nextStepY, field)
                        && (coordX != 0 && coordY != 0);

                    if (validMove)
                    {
                        double betterLocation = _math.Vector(nextStepX, nextStepY, carnivore.ClosestEnemy.CoordinateX, carnivore.ClosestEnemy.CoordinateY);
                        if (betterLocation <= initialLocation)
                        {
                            initialLocation = betterLocation;

                            bestStepX = carnivore.CoordinateX + coordX;
                            bestStepY = carnivore.CoordinateY + coordY;

                            if (_validator.HerbivoreExists(bestStepX, bestStepY, field))
                            {
                                EatVictim(carnivore, field);
                                break;
                            }
                        }
                    }
                }
            }

            _genericAnimal.TakeAStep(bestStepX, bestStepY, carnivore, field);

            return searchList;
        }

        public void EatVictim(Animal carnivore, Field field)
        {
            var savedAnimal = _genericAnimal.FindInField(field, carnivore.CoordinateX, carnivore.CoordinateY);
            savedAnimal.ClosestEnemy.Alive = false;
            savedAnimal.ClosestEnemy = null;
        }
    }
}
