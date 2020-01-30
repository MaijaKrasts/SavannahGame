namespace Savannah
{
    using System;
    using AnimalLibrary;
    using Savannah.Interfaces;
    using Savannah.Models;
    using Savannah.Static;

    public class AnimalFactory : IAnimalFactory
    {
        private IAnimalValidator _validator;
        private IConsoleFacade _facade;

        public AnimalFactory(IAnimalValidator validator, IConsoleFacade facade)
        {
            _validator = validator;
            _facade = facade;
        }

        public Animal CreateAnimal(ConsoleKey key, Field field)
        {
            var coordX = _facade.GetRandomMinMax(0, field.Width);
            var coordY = _facade.GetRandomMinMax(0, field.Height);

            if (_validator.AnimalExists(coordX, coordY, field))
            {
                CreateAnimal(key, field);
            }

            var newAnimal = new Animal();

            if (key == TextParameters.AntelopeKey)
            {
                newAnimal = new AntelopeLibrary.Antelope();
            }
            else if (key == TextParameters.LionKey)
            {
                newAnimal = new LionLibrary.Lion();
            }

            newAnimal.Alive = true;
            newAnimal.CoordinateX = coordX;
            newAnimal.CoordinateY = coordY;
            newAnimal.MatingCount = 0;
            newAnimal.Health = NumParameters.MaxHealth;

            field.Animals.Add(newAnimal);
            return newAnimal;
        }
    }
}
