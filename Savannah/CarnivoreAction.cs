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

        public List<IAnimal> Move(List<IAnimal> additionalField, Field field)
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
            }

            return additionalField;
        }

        public List<IAnimal> MoveWithoutEnemies(IAnimal carnivore, List<IAnimal> additionalField, Field field)
        {
            bool foundMove = false;

            while (!foundMove)
            {
                int moveX = rnd.Next(-1, 2);
                int moveY = rnd.Next(-1, 2);

                int nextStepX = carnivore.CoordinateX + moveX;
                int nextStepY = carnivore.CoordinateY + moveY;

                var validMove = (nextStepX < field.Width)
                    && (nextStepY < field.Height)
                    && (nextStepX > 0)
                    && (nextStepY > 0)
                    && !_generalActions.AnimalExists(nextStepX, nextStepY, field);

                if (validMove)
                {
                    foundMove = true;
                }

                var findAnimal = additionalField.Find(c => c.CoordinateY == carnivore.CoordinateY && c.CoordinateX == carnivore.CoordinateX);
                findAnimal.CoordinateX += moveX;
                findAnimal.CoordinateY += moveY;
            }

            return additionalField;
        }

        public List<IAnimal> MoveWithEnemies(IAnimal carnivore, List<IAnimal> additionalField, Field field)
        {
            var initialLocation = _math.Vector(carnivore.CoordinateX, carnivore.CoordinateY, carnivore.ClosestEnemy.CoordinateX, carnivore.ClosestEnemy.CoordinateY);

            for (int coordX = -1; coordX < 2; coordX++)
            {
                for (int coordY = -1; coordY < 2; coordY++)
                {
                    int nextStepX = carnivore.CoordinateX + coordX;
                    int nextStepY = carnivore.CoordinateY + coordY;

                    var validMove = (nextStepX < field.Width)
                            && (nextStepY < field.Height)
                            && (nextStepX > 0)
                            && (nextStepY > 0);

                    if (validMove && !_generalActions.CarnivoreExists(nextStepX, nextStepY, field))
                    {
                        double betterLocation = _math.Vector(nextStepX, nextStepY, carnivore.ClosestEnemy.CoordinateX, carnivore.ClosestEnemy.CoordinateY);
                        if (betterLocation > initialLocation)
                        {
                            initialLocation = betterLocation;
                            var findAnimal = additionalField.Find(c => c.CoordinateY == carnivore.CoordinateY && c.CoordinateX == carnivore.CoordinateX);
                            findAnimal.CoordinateX += coordX;
                            findAnimal.CoordinateY += coordY;
                        }

                        if (_generalActions.HerbivoreExists(nextStepX, nextStepY, field))
                        {
                            EatVictim(carnivore, additionalField, field);

                            var findAnimal = additionalField.Find(c => c.CoordinateY == carnivore.CoordinateY && c.CoordinateX == carnivore.CoordinateX);
                            findAnimal.CoordinateX += coordX;
                            findAnimal.CoordinateY += coordY;
                        }
                    }
                }
            }
            return additionalField;
        }

        public void EatVictim(IAnimal carnivore, List<IAnimal> additionalField, Field field)
        {
            carnivore.ClosestEnemy.Alive = false;
            additionalField.Remove(carnivore.ClosestEnemy);
        }



    }
}
