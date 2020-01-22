namespace Savannah
{
    using System;
    using System.Collections.Generic;
    using Savannah.Interfaces;
    using Savannah.Models;

    public class GeneralAnimalAction : IGeneralAnimalAction
    {
        private List<IAnimal> animals;

        public GeneralAnimalAction()
        {
            animals = new List<IAnimal>();
        }

        public List<IAnimal> AdditionalAnimalField(Field field)
        {
            List<IAnimal> additionalAnimals = new List<IAnimal>();
            additionalAnimals = field.Animals;
            return additionalAnimals;
        }

        public bool AnimalExists(int coordinateX, int coordinateY)
        {
            var animalExist = animals.Find(u => u.CoordinateX == coordinateX && u.CoordinateY == coordinateY) != null;
            return animalExist;
        }

        public bool LionExists(int coordinateX, int coordinateY)
        {
            var animalExist = animals.Find(u => u.CoordinateX == coordinateX && u.CoordinateY == coordinateY);

            if (animalExist == null)
            {
                return true;
            }
            else if (animalExist.Symbol == "L")
            {
                return true;
            }

            return false;
        }

        public bool AntelopeExists(int coordinateX, int coordinateY)
        {
            var animalExist = animals.Find(u => u.CoordinateX == coordinateX && u.CoordinateY == coordinateY);

            if (animalExist == null)
            {
                return true;
            }
            else if (animalExist.Symbol == "A")
            {
                return true;
            }

            return false;
        }

        public bool AnimalOutOfField(int coordinateX, int coordinateY, Field field)
        {
            var outOfField = coordinateX > field.Height || coordinateY > field.Width;
            return outOfField;
        }

        public double LocateSingle(int animalX, int animalY, int enemyX, int enemyY)
        {
            var xLocation = (enemyX - animalX) * (enemyX - animalX);
            var yLocation = (enemyY - animalY) * (enemyY - animalY);

            var location = Math.Sqrt(xLocation + yLocation);

            return location;
        }
    }
}
