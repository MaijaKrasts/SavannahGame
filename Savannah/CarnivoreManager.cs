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

        public CarnivoreManager(IAnimalValidator generalAction, ICalculations math, IConsoleFacade facade)
        {
            _validator = generalAction;
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

                    if (location <= ultimateLocation)
                    {
                        if (location < Numbers.VisionRange)
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

                var validMove = (nextStepX < field.Width)
                    && (nextStepY < field.Height)
                    && (nextStepX > 0)
                    && (nextStepY > 0)
                    && !_validator.AnimalExists(nextStepX, nextStepY, field);

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

        public List<Animal> MoveWithEnemies(Animal carnivore, List<Animal> additionalField, Field field)
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

                    if (validMove && !_validator.CarnivoreExists(nextStepX, nextStepY, field))
                    {
                        double betterLocation = _math.Vector(nextStepX, nextStepY, carnivore.ClosestEnemy.CoordinateX, carnivore.ClosestEnemy.CoordinateY);
                        if (betterLocation <= initialLocation)
                        {
                            initialLocation = betterLocation;
                            var findAnimal = additionalField.Find(c => c.CoordinateY == carnivore.CoordinateY && c.CoordinateX == carnivore.CoordinateX);
                            findAnimal.CoordinateX = nextStepX;
                            findAnimal.CoordinateY = nextStepY;

                            if (_validator.HerbivoreExists(findAnimal.CoordinateY, findAnimal.CoordinateY, field))
                            {
                                EatVictim(carnivore, additionalField);
                                findAnimal.CoordinateX = nextStepX;
                                findAnimal.CoordinateY = nextStepY;
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
