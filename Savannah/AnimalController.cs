namespace Savannah
{
    using System;
    using System.Collections.Generic;
    using Savannah.Models;

    public class AnimalController
    {
        private List<Antelope> antelopes;
        private List<Lion> lions;

        public AnimalController()
        {
            antelopes = new List<Antelope>();
            lions = new List<Lion>();
        }

        public bool AnimalExists(int coordinateX, int coordinateY)
        {
            var antilopeExist = antelopes.Find(u => u.CoordinateX == coordinateX && u.CoordinateY == coordinateY) != null;
            bool lionExist = lions.Find(u => u.CoordinateX == coordinateX && u.CoordinateY == coordinateY) != null;

            bool animalExist = antilopeExist || lionExist;

            return animalExist;
        }

        public bool AnimalOutOfField(int coordinateX, int coordinateY, Field field)
        {
            var outOfField = coordinateX > field.Height || coordinateY > field.Width;

            return outOfField;
        }

        public Antelope CreateAntelope(Field field)
        {
            Random rnd = new Random();
            var coordX = rnd.Next(15);
            var coordY = rnd.Next(15);

            if (AnimalExists(coordX, coordY))
            {
                CreateAntelope(field);
            }
            else if(coordX > field.Height || coordY > field.Width)
            {
                CreateAntelope(field);
            }

            var newAntelope = new Antelope();
            newAntelope.Alive = true;
            newAntelope.CoordinateX = coordX;
            newAntelope.CoordinateY = coordY;

            field.Animals.Add(newAntelope);
            return newAntelope;
        }

        public Lion CreateLion(Field field)
        {
            Random rnd = new Random();
            var coordX = rnd.Next(15);
            var coordY = rnd.Next(15);

            if (AnimalExists(coordX, coordY))
            {
                CreateLion(field);
            }
            else if (coordX > field.Height || coordY > field.Width)
            {
                CreateLion(field);
            }

            var newLion = new Lion();
            newLion.Alive = true;
            newLion.CoordinateX = coordX;
            newLion.CoordinateY = coordY;

            field.Animals.Add(newLion);
            return newLion;
        }

    }
}
