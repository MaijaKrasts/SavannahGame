namespace Savannah
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Savannah.Interfaces;
    using Savannah.Models;

    public class LionAction : IAnimalAction
    {
        private GeneralAnimalAction generiActions;
        private Random rnd;

        public LionAction()
        {
            generiActions = new GeneralAnimalAction();
            rnd = new Random();
        }

        public IAnimal Create(Field field)
        {
            var coordY = rnd.Next(field.Height);
            var coordX = rnd.Next(field.Width);

            if (generiActions.AnimalExists(coordY, coordX))
            {
                Create(field);
            }

            var newLion = new Lion();
            newLion.Alive = true;
            newLion.CoordinateX = coordY;
            newLion.CoordinateY = coordX;

            field.Animals.Add(newLion);
            return newLion;
        }

        public void Locate(Field field)
        {
            double ultimateLocation = Math.Sqrt(((0 - field.Width) * (0 - field.Width)) + ((0 - field.Height) * (0 - field.Height)));

            var antelopeList = field.Animals.FindAll(a => a.Symbol == "A").ToList();
            var lionList = field.Animals.FindAll(a => a.Symbol == "L").ToList();

            foreach (var lion in lionList)
            {
                foreach (var antelope in antelopeList)
                {
                    var xLocation = (antelope.CoordinateX - lion.CoordinateX) * (antelope.CoordinateX - lion.CoordinateX);
                    var yLocation = (antelope.CoordinateY - lion.CoordinateY) * (antelope.CoordinateY - lion.CoordinateY);
                    var location = Math.Sqrt(xLocation + yLocation);

                    if (location < ultimateLocation)
                    {
                        if (!(location > 3))
                        {
                            ultimateLocation = location;
                            lion.ClosestEnemy = antelope;
                        }
                    }
                }
                ultimateLocation = Math.Sqrt(((0 - field.Width) * (0 - field.Width)) + ((0 - field.Height) * (0 - field.Height)));
            }

        }

        public Field MoveWithoutEnemies(Field field, List<IAnimal> additionalField)
        {
            var lionList = field.Animals.FindAll(a => a.Symbol == "L").ToList();
            int moveX = rnd.Next(-1, 2);
            int moveY = rnd.Next(-1, 2);

            foreach (var lion in lionList)
            {
                if (lion.ClosestEnemy == null)
                {
                    while (true)
                    {
                        moveX = rnd.Next(-1, 2);
                        moveY = rnd.Next(-1, 2);

                        var validMove = (lion.CoordinateX + moveX < field.Width)
                            && (lion.CoordinateY + moveY < field.Height)
                            && (lion.CoordinateX + moveX > 0)
                            && (lion.CoordinateY + moveY > 0);

                        if (validMove)
                        {
                            break;
                        }
                    }

                    additionalField.Find(c => c.CoordinateY == lion.CoordinateY && c.CoordinateX == lion.CoordinateX).CoordinateX += moveX;
                    additionalField.Find(c => c.CoordinateY == lion.CoordinateY && c.CoordinateX == lion.CoordinateX).CoordinateY += moveY;
                }
                else if (lion.ClosestEnemy != null)
                {
                    MoveWithEnemies(lion, additionalField);
                }
            }

            return field;
        }

        public IAnimal MoveWithEnemies(IAnimal lion, List<IAnimal> additionalField)
        {
            var initialLocation = generiActions.LocateSingle(lion.CoordinateX, lion.CoordinateY, lion.ClosestEnemy.CoordinateX, lion.ClosestEnemy.CoordinateY);

            for (int coordX = -1; coordX < 2; coordX++)
            {
                for (int coordY = -1; coordY < 2; coordY++)
                {
                    double betterLocation = generiActions.LocateSingle(lion.CoordinateX + coordX, lion.CoordinateY + coordY, lion.ClosestEnemy.CoordinateX, lion.ClosestEnemy.CoordinateY);

                    if (!generiActions.LionExists(lion.CoordinateX + coordX, lion.CoordinateY + coordY))
                    {
                        if (betterLocation < initialLocation)
                        {
                            initialLocation = betterLocation;

                            additionalField.Find(c => c.CoordinateY == lion.CoordinateY && c.CoordinateX == lion.CoordinateX).CoordinateX += coordX;
                            additionalField.Find(c => c.CoordinateY == lion.CoordinateY && c.CoordinateX == lion.CoordinateX).CoordinateY += coordY;
                        }
                    }
                    else if (generiActions.AntelopeExists(lion.CoordinateX + coordX, lion.CoordinateY + coordY))
                    {
                        EatAntelope(lion);
                    }
                }
            }
            return lion;
        }

        public void EatAntelope(IAnimal lion)
        {
            IAnimal victimAnimal = lion.ClosestEnemy;
            victimAnimal.Alive = false;
        }


    }
}
