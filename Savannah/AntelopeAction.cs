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

                    if (location <= ultimateLocation)
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

        public List<Animal> Move(List<Animal> additionalField, Field field)
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
            }

            return additionalField;
        }

        public List<Animal> MoveWithoutEnemies(Animal herbivore, List<Animal> additionalField, Field field)
        {
            bool foundMove = false;

            while (!foundMove)
            {
                int moveX = rnd.Next(-1, 2);
                int moveY = rnd.Next(-1, 2);

                int nextStepX = herbivore.CoordinateX + moveX;
                int nextStepY = herbivore.CoordinateY + moveY;

                var validMove = (nextStepX < field.Width)
                    && (nextStepY < field.Height)
                    && (nextStepX > 0)
                    && (nextStepY > 0)
                    && !_generalActions.AnimalExists(nextStepX, nextStepY, field);

                if (validMove)
                {
                    foundMove = true;
                }

                var findAnimal = additionalField.Find(c => c.CoordinateY == herbivore.CoordinateY && c.CoordinateX == herbivore.CoordinateX);
                findAnimal.CoordinateX += moveX;
                findAnimal.CoordinateY += moveY;
            }

            return additionalField;
        }

        public List<Animal> MoveWithEnemies(Animal herbivore, List<Animal> additionalField, Field field)
        {
            var initialLocation = _math.Vector(herbivore.CoordinateX, herbivore.CoordinateY, herbivore.ClosestEnemy.CoordinateX, herbivore.ClosestEnemy.CoordinateY);

            for (int coordX = -1; coordX < 2; coordX++)
            {
                for (int coordY = -1; coordY < 2; coordY++)
                {
                    int nextStepX = herbivore.CoordinateX + coordX;
                    int nextStepY = herbivore.CoordinateY + coordY;

                    var validMove = (nextStepX < field.Width)
                            && (nextStepY < field.Height)
                            && (nextStepX > 0)
                            && (nextStepY > 0)
                            && !_generalActions.AnimalExists(nextStepX, nextStepY, field);

                    if (validMove)
                    {
                        double betterLocation = _math.Vector(nextStepX, nextStepY, herbivore.ClosestEnemy.CoordinateX, herbivore.ClosestEnemy.CoordinateY);
                        if (betterLocation >= initialLocation)
                        {
                            initialLocation = betterLocation;
                            var findAnimal = additionalField.Find(c => c.CoordinateY == herbivore.CoordinateY && c.CoordinateX == herbivore.CoordinateX);
                            findAnimal.CoordinateX += coordX;
                            findAnimal.CoordinateY += coordY;
                        }
                    }
                }
            }
            return additionalField;
        }
    }
}
