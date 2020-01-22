namespace Savannah
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Savannah.Interfaces;
    using Savannah.Models;


    public class AntelopeAction : IAnimalAction
    {
        private GeneralAnimalAction generiActions;
        private Random rnd;

        public AntelopeAction()
        {
            generiActions = new GeneralAnimalAction();
            rnd = new Random();
        }

        public IAnimal Create(Field field)
        {
            var coordX = rnd.Next(field.Height);
            var coordY = rnd.Next(field.Width);

            if (generiActions.AnimalExists(coordX, coordY))
            {
                Create(field);
            }

            var newAntelope = new Antelope();
            newAntelope.Alive = true;
            newAntelope.CoordinateX = coordX;
            newAntelope.CoordinateY = coordY;

            field.Animals.Add(newAntelope);
            return newAntelope;
        }

        public void Locate(Field field)
        {
            double ultimateLocation = Math.Sqrt(((0 - field.Width) * (0 - field.Width)) + ((0 - field.Height) * (0 - field.Height)));

            var antelopeList = field.Animals.FindAll(a => a.Symbol == "A").ToList();
            var lionList = field.Animals.FindAll(a => a.Symbol == "L").ToList();

            foreach (var antelope in antelopeList)
            {
                foreach (var lion in lionList)
                {
                    var xLocation = (antelope.CoordinateX - lion.CoordinateX) * (antelope.CoordinateX - lion.CoordinateX);
                    var yLocation = (antelope.CoordinateY - lion.CoordinateY) * (antelope.CoordinateY - lion.CoordinateY);

                    var location = Math.Sqrt(xLocation + yLocation);

                    if (location < ultimateLocation)
                    {
                        if (location < 3.1)
                        {
                            ultimateLocation = location;
                            antelope.ClosestEnemy = lion;
                        }
                    }
                }
                ultimateLocation = Math.Sqrt(((0 - field.Width) * (0 - field.Width)) + ((0 - field.Height) * (0 - field.Height)));
            }
        }

        public IAnimal MoveWithEnemies(IAnimal antelope, List<IAnimal> additionalField)
        {
            var initialLocation = generiActions.LocateSingle(antelope.CoordinateX, antelope.CoordinateY, antelope.ClosestEnemy.CoordinateX, antelope.ClosestEnemy.CoordinateY);

            for (int coordX = -1; coordX < 2; coordX++)
            {
                for (int coordY = -1; coordY < 2; coordY++)
                {
                    double betterLocation = generiActions.LocateSingle(antelope.CoordinateX + coordX, antelope.CoordinateY + coordY, antelope.ClosestEnemy.CoordinateX, antelope.ClosestEnemy.CoordinateY);
                    if (!generiActions.AnimalExists(antelope.CoordinateX + coordX, antelope.CoordinateY + coordY))
                    {
                        if (betterLocation > initialLocation)
                        {
                            initialLocation = betterLocation;

                            additionalField.Find(c => c.CoordinateY == antelope.CoordinateY && c.CoordinateX == antelope.CoordinateX).CoordinateX += coordX;
                            additionalField.Find(c => c.CoordinateY == antelope.CoordinateY && c.CoordinateX == antelope.CoordinateX).CoordinateY += coordY;
                        }
                    }
                }
            }

            return antelope;
        }

        public Field MoveWithoutEnemies(Field field, List<IAnimal> additionalField)
        {
            var antelopeList = field.Animals.FindAll(a => a.Symbol == "A").ToList();

            int moveX = rnd.Next(-1, 2);
            int moveY = rnd.Next(-1, 2);

            foreach (var antelope in antelopeList)
            {
                if (antelope.ClosestEnemy == null)
                {
                    while (true)
                    {
                        moveX = rnd.Next(-1, 2);
                        moveY = rnd.Next(-1, 2);

                        var validMove = (antelope.CoordinateX + moveX < field.Width)
                            && (antelope.CoordinateY + moveY < field.Height)
                            && (antelope.CoordinateX + moveX > 0)
                            && (antelope.CoordinateY + moveY > 0);

                        if (validMove)
                        {
                            break;
                        }
                    }

                    additionalField.Find(c => c.CoordinateY == antelope.CoordinateY && c.CoordinateX == antelope.CoordinateX).CoordinateX += moveX;
                    additionalField.Find(c => c.CoordinateY == antelope.CoordinateY && c.CoordinateX == antelope.CoordinateX).CoordinateY += moveY;
                }
                else if (antelope.ClosestEnemy != null)
                {
                    MoveWithEnemies(antelope, additionalField);
                }
            }

            return field;
        }

    }
}
