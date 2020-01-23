namespace Savannah
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Savannah.Interfaces;
    using Savannah.Models;
    using Savannah.Static;

    public class CarnivoreAction : IAnimalAction
    {
        private IGeneralAnimalAction _generalActions;
        private ICalculations _math;
        private IFacade _facade;
        private Random rnd;

        public CarnivoreAction(IGeneralAnimalAction generalAction, ICalculations math, IFacade facade)
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

            foreach (var carnivore in carnivoreList)
            {
                foreach (var herbivore in herbivoreList)
                {
                    var location = _math.Vector(herbivore.CoordinateX, carnivore.CoordinateX, herbivore.CoordinateY, carnivore.CoordinateY);

                    if (location < ultimateLocation)
                    {
                        if (location < Numbers.VisionRange)
                        {
                            ultimateLocation = location;
                            carnivore.ClosestEnemy = herbivore;
                        }
                    }
                }
            }
        }

        public Field MoveWithoutEnemies(Field field, List<IAnimal> additionalField)
        {
            var carnivoreList = field.Animals.FindAll(a => a.Herbivore == false).ToList();

            int moveX = 0;
            int moveY = 0;
            bool foundMove = false;

            foreach (var carnivore in carnivoreList)
            {
                if (carnivore.ClosestEnemy == null)
                {
                    while (!foundMove)
                    {
                        moveX = rnd.Next(-1, 2);
                        moveY = rnd.Next(-1, 2);

                        var validMove = (carnivore.CoordinateX + moveX < field.Width)
                            && (carnivore.CoordinateY + moveY < field.Height)
                            && (carnivore.CoordinateX + moveX > 0)
                            && (carnivore.CoordinateY + moveY > 0);

                        if (validMove)
                        {
                            foundMove = true;
                        }
                    }

                    additionalField.Find(c => c.CoordinateY == carnivore.CoordinateY && c.CoordinateX == carnivore.CoordinateX).CoordinateX += moveX;
                    additionalField.Find(c => c.CoordinateY == carnivore.CoordinateY && c.CoordinateX == carnivore.CoordinateX).CoordinateY += moveY;
                }
                else if (carnivore.ClosestEnemy != null)
                {
                    MoveWithEnemies(carnivore, additionalField, field);
                }
            }

            return field;
        }

        public IAnimal MoveWithEnemies(IAnimal carnivore, List<IAnimal> additionalField, Field field)
        {
            var initialLocation = _math.Vector(carnivore.CoordinateX, carnivore.CoordinateY, carnivore.ClosestEnemy.CoordinateX, carnivore.ClosestEnemy.CoordinateY);

            for (int coordX = -1; coordX < 2; coordX++)
            {
                for (int coordY = -1; coordY < 2; coordY++)
                {
                    int nextStepX = carnivore.CoordinateX + coordX;
                    int nextStepY = carnivore.CoordinateY + coordY;

                    double betterLocation = _math.Vector(nextStepX, nextStepY, carnivore.ClosestEnemy.CoordinateX, carnivore.ClosestEnemy.CoordinateY);

                    if (!_generalActions.CarnivoreExists(nextStepX, nextStepY, field))
                    {
                        if (betterLocation < initialLocation)
                        {
                            initialLocation = betterLocation;

                            additionalField.Find(c => c.CoordinateY == carnivore.CoordinateY && c.CoordinateX == carnivore.CoordinateX).CoordinateX += coordX;
                            additionalField.Find(c => c.CoordinateY == carnivore.CoordinateY && c.CoordinateX == carnivore.CoordinateX).CoordinateY += coordY;
                        }
                    }
                    else if (_generalActions.HerbivoreExists(nextStepX, nextStepY, field))
                    {
                        EatVictim(carnivore, additionalField);
                    }
                }
            }
            return carnivore;
        }

        public void EatVictim(IAnimal carnivore, List<IAnimal> additionalField)
        {
            carnivore.ClosestEnemy.Alive = false;
            additionalField.Remove(carnivore.ClosestEnemy);
        }
    }
}
