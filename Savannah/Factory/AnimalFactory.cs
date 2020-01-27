namespace Savannah
{
    using Savannah.Interfaces;
    using Savannah.Models;
    using Savannah.Static;
    using System;

    public class AnimalFactory : IAnimalFactory
    {
        private IAnimalValidator _validator;
        private IConsoleFacade _facade;

        public AnimalFactory(IAnimalValidator generalAction, IConsoleFacade facade)
        {
            _validator = generalAction;
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
                    newAnimal = new Antelope()
                    {
                        Alive = true,
                        CoordinateX = coordX,
                        CoordinateY = coordY,
                        Herbivore = true,
                        Symbol = TextParameters.Antelope,
                        MatingCount = 0,
                        Health = NumberParameters.MaxHealth,
                    };
                }
                else if (key == TextParameters.LionKey)
                {
                    newAnimal = new Lion()
                    {
                        Alive = true,
                        CoordinateX = coordX,
                        CoordinateY = coordY,
                        Herbivore = false,
                        Symbol = TextParameters.Lion,
                        MatingCount = 0,
                        Health = NumberParameters.MaxHealth,
                    };
                }

            field.Animals.Add(newAnimal);
            return newAnimal;
        }
    }
}
