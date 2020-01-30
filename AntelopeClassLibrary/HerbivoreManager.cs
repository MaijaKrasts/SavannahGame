//namespace AntelopeClassLibrary
//{
//    using System.Collections.Generic;
//    using System.Linq;
//    using AntelopeClassLibrary.Interfaces;
//    using SavannahClassLibrary;
//    using SavannahClassLibrary.Interfaces;
//    using SavannahClassLibrary.Models;
//    using SavannahClassLibrary.Static;

//    public class HerbivoreManager : IHerbivoreManager
//    {
//        private IAnimalValidator _validator;
//        private ICalculations _math;
//        private IConsoleFacade _facade;
//        private IGenericAnimalManager _genericAnimal;

//        public HerbivoreManager(IAnimalValidator generalAction, ICalculations math, IConsoleFacade facade, IGenericAnimalManager genericAnimal)
//        {
//            _validator = generalAction;
//            _math = math;
//            _facade = facade;
//            _genericAnimal = genericAnimal;
//        }

//        public List<Animal> ChooseTheMove(List<Animal> additionalField, Field field)
//        {
//            var herbivoreList = additionalField.FindAll(a => a.Herbivore == true).ToList();

//            foreach (var herbivore in herbivoreList)
//            {
//                if (herbivore.ClosestEnemy == null)
//                {
//                    additionalField = MoveWithoutEnemies(herbivore, additionalField, field);
//                }
//                else if (herbivore.ClosestEnemy != null)
//                {
//                    additionalField = MoveWithEnemies(herbivore, additionalField, field);
//                }

//                _genericAnimal.DecreaseHealth(herbivore);
//            }

//            return additionalField;
//        }

//        public List<Animal> MoveWithoutEnemies(Animal herbivore, List<Animal> additionalAnimal, Field field)
//        {
//            bool foundMove = false;

//            while (!foundMove)
//            {
//                int moveX = _facade.GetRandomMinMax(NumParameters.MovingNegative, NumParameters.MovingPositive);
//                int moveY = _facade.GetRandomMinMax(NumParameters.MovingNegative, NumParameters.MovingPositive);

//                int nextStepX = herbivore.CoordinateX + moveX;
//                int nextStepY = herbivore.CoordinateY + moveY;

//                var validMove = _validator.ValidateMove(nextStepX, nextStepY, field)
//                    && !_validator.AnimalExists(nextStepX, nextStepY, field);

//                if (validMove)
//                {
//                    foundMove = true;
//                }

//                _genericAnimal.TakeAStep(nextStepX, nextStepY, herbivore, field);
//            }

//            return additionalAnimal;
//        }

//        public List<Animal> MoveWithEnemies(Animal herbivore, List<Animal> additionalField, Field field)
//        {
//            double closestLocation = 0;
//            for (int coordX = NumParameters.MovingNegative; coordX < NumParameters.MovingPositive; coordX++)
//            {
//                for (int coordY = NumParameters.MovingNegative; coordY < NumParameters.MovingPositive; coordY++)
//                {
//                    int nextStepX = herbivore.CoordinateX + coordX;
//                    int nextStepY = herbivore.CoordinateY + coordY;

//                    var validMove = _validator.ValidateMove(nextStepX, nextStepY, field)
//                            && !_validator.AnimalExists(nextStepX, nextStepY, field);

//                    if (validMove)
//                    {
//                        double betterLocation = _math.Vector(nextStepX, nextStepY, herbivore.ClosestEnemy.CoordinateX, herbivore.ClosestEnemy.CoordinateY);
//                        if (betterLocation >= closestLocation)
//                        {
//                            closestLocation = betterLocation;

//                            _genericAnimal.TakeAStep(nextStepX, nextStepY, herbivore, field);
//                        }
//                    }
//                }
//            }
//            return additionalField;
//        }
//    }
//}
