﻿namespace Savannah
{
    using Savannah.Interfaces;
    using Savannah.Models;
    using System.Linq;

    public class AnimalValidator : IAnimalValidator
    {
        public bool AnimalExists(int coordinateX, int coordinateY, Field field)
        {
            bool animalExist = field.Animals.FirstOrDefault(u => u.CoordinateX == coordinateX && u.CoordinateY == coordinateY) != null;
            return animalExist;
        }

        public bool CarnivoreExists(int coordinateX, int coordinateY, Field field)
        {
            var animalExist = field.Animals.FirstOrDefault(u => u.CoordinateX == coordinateX && u.CoordinateY == coordinateY);

            if (animalExist == null)
            {
                return false;
            }

            return !animalExist.IsHerbivore;
        }

        public bool HerbivoreExists(int coordinateX, int coordinateY, Field field)
        {
            var animalExist = field.Animals.FirstOrDefault(u => u.CoordinateX == coordinateX && u.CoordinateY == coordinateY);

            if (animalExist == null)
            {
                return false;
            }

            return animalExist.IsHerbivore;
        }

        public bool AnimalOutOfField(int coordinateX, int coordinateY, Field field)
        {
            var outOfField = coordinateX > field.Height || coordinateY > field.Width;
            return outOfField;
        }

        public bool ValidateMove(int nextStepX, int nextStepY, Field field)
        {
            var validMove = (nextStepX < field.Width)
                  && (nextStepY < field.Height)
                  && (nextStepX > 0)
                  && (nextStepY > 0);

            return validMove;
        }

    }
}
