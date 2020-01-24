﻿namespace Savannah
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Savannah.Interfaces;
    using Savannah.Models;
    using Savannah.Static;

    public class HerbivoreManager : IAnimalManager
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
            rnd = _facade.GetRandom();
            _genericAnimal = genericAnimal;
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
                        if (location < NumberParameters.VisionRange)
                        {
                            ultimateLocation = location;
                            herbivore.ClosestEnemy = carnivore;
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
                int moveX = rnd.Next(-1, 2);
                int moveY = rnd.Next(-1, 2);

                int nextStepX = herbivore.CoordinateX + moveX;
                int nextStepY = herbivore.CoordinateY + moveY;

                var validMove = _validator.ValidateMove(nextStepX, nextStepY, field)
                    && !_validator.AnimalExists(nextStepX, nextStepY, field);

                if (validMove)
                {
                    foundMove = true;
                }

                herbivore.CoordinateX = nextStepX;
                herbivore.CoordinateY = nextStepY;
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
                            herbivore.CoordinateX = nextStepX;
                            herbivore.CoordinateY = nextStepY;
                        }
                    }
                }
            }
            return additionalField;
        }
    }
}
