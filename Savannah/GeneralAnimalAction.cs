namespace Savannah
{
    using System;
    using System.Collections.Generic;
    using Savannah.Interfaces;
    using Savannah.Models;

    public class GeneralAnimalAction : IGeneralAnimalAction
    {

        public List<IAnimal> AdditionalAnimalField(Field field)
        {
            List<IAnimal> additionalAnimals = field.Animals;
            return additionalAnimals;
        }

        public bool AnimalExists(int coordinateX, int coordinateY, Field field)
        {
            var animalExist = false;
            animalExist = field.Animals.Find(u => u.CoordinateX == coordinateX && u.CoordinateY == coordinateY) != null == true;
            return animalExist;
        }

        public bool CarnivoreExists(int coordinateX, int coordinateY, Field field)
        {
            var animalExist = field.Animals.Find(u => u.CoordinateX == coordinateX && u.CoordinateY == coordinateY);

            if (animalExist == null)
            {
                return false;
            }
            else if (animalExist.Herbivore == false)
            {
                return false;
            }

            return true;
        }

        public bool HerbivoreExists(int coordinateX, int coordinateY, Field field)
        {
            var animalExist = field.Animals.Find(u => u.CoordinateX == coordinateX && u.CoordinateY == coordinateY);

            if (animalExist == null)
            {
                return true;
            }
            else if (animalExist.Herbivore == true)
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

    }
}
