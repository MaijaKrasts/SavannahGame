namespace Savannah
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Savannah.Interfaces;
    using Savannah.Models;
    using Savannah.Static;

    public class AntelopeAction : IAnimalAction
    {
        private IGeneralAnimalAction _generalActions;
        private ICalculations _math;
        private IFacade _facade;
        private Random rnd;

        public AntelopeAction(IGeneralAnimalAction generalAction, ICalculations math, IFacade facade)
        {
            _generalActions = generalAction;
            _math = math;
            _facade = facade;
            rnd = _facade.GetRandom();
        }

        public void Locate(Field field)
        {
            double ultimateLocation = _math.Vector(0, field.Width, 0, field.Height);

            var herbivoreList = field.Animals.FindAll(a => a.Herbivore == true).ToList();
            var carnivoreList = field.Animals.FindAll(a => a.Herbivore == false).ToList();

            foreach (var herbivore in herbivoreList)
            {
                foreach (var carnivore in carnivoreList)
                {
                    var location = _math.Vector(herbivore.CoordinateX, carnivore.CoordinateX, herbivore.CoordinateY, carnivore.CoordinateY);

                    if (location < ultimateLocation)
                    {
                        if (location < Numbers.VisionRange)
                        {
                            ultimateLocation = location;
                            herbivore.ClosestEnemy = carnivore;
                        }
                    }
                }
            }
        }

        public Field MoveWithoutEnemies(Field field, List<IAnimal> additionalField)
        {
            var herbivoreList = field.Animals.FindAll(a => a.Herbivore == true).ToList();

            int moveX = 0;
            int moveY = 0;
            bool foundMove = false;

            foreach (var herbivore in herbivoreList)
            {
                if (herbivore.ClosestEnemy == null)
                {
                    while (!foundMove)
                    {
                        moveX = rnd.Next(-1, 2);
                        moveY = rnd.Next(-1, 2);

                        var validMove = (herbivore.CoordinateX + moveX < field.Width)
                            && (herbivore.CoordinateY + moveY < field.Height)
                            && (herbivore.CoordinateX + moveX > 0)
                            && (herbivore.CoordinateY + moveY > 0);

                        if (validMove)
                        {
                            foundMove = true;
                        }
                    }

                    additionalField.Find(c => c.CoordinateY == herbivore.CoordinateY && c.CoordinateX == herbivore.CoordinateX).CoordinateX += moveX;
                    additionalField.Find(c => c.CoordinateY == herbivore.CoordinateY && c.CoordinateX == herbivore.CoordinateX).CoordinateY += moveY;
                }
                else if (herbivore.ClosestEnemy != null)
                {
                    MoveWithEnemies(herbivore, additionalField, field);
                }
            }

            return field;
        }

        public IAnimal MoveWithEnemies(IAnimal herbivore, List<IAnimal> additionalField, Field field)
        {
            var initialLocation = _math.Vector(herbivore.CoordinateX, herbivore.CoordinateY, herbivore.ClosestEnemy.CoordinateX, herbivore.ClosestEnemy.CoordinateY);

            for (int coordX = -1; coordX < 2; coordX++)
            {
                for (int coordY = -1; coordY < 2; coordY++)
                {
                    int nextStepX = herbivore.CoordinateX + coordX;
                    int nextStepY = herbivore.CoordinateY + coordY;

                    double betterLocation = _math.Vector(nextStepX, nextStepY, herbivore.ClosestEnemy.CoordinateX, herbivore.ClosestEnemy.CoordinateY);
                    if (!_generalActions.AnimalExists(nextStepX, nextStepY, field))
                    {
                        if (betterLocation > initialLocation)
                        {
                            initialLocation = betterLocation;

                            additionalField.Find(c => c.CoordinateY == herbivore.CoordinateY && c.CoordinateX == herbivore.CoordinateX).CoordinateX += coordX;
                            additionalField.Find(c => c.CoordinateY == herbivore.CoordinateY && c.CoordinateX == herbivore.CoordinateX).CoordinateY += coordY;
                        }
                    }
                }
            }
            return herbivore;
        }
    }
}
