namespace SavannahClassLibrary
{
    using System;
    using SavannahClassLibrary.Models;
    using SavannahClassLibrary.Static;
    using SavannahClassLibrary.Interfaces;

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
                    newAnimal = new Antelope()
                    {
                        Alive = true,
                        CoordinateX = coordX,
                        CoordinateY = coordY,
                        Herbivore = true,
                        Symbol = TextParameters.Antelope,
                        Key = TextParameters.AntelopeKey,
                        MatingCount = 0,
                        Health = NumParameters.MaxHealth,
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
                        Key = TextParameters.LionKey,
                        MatingCount = 0,
                        Health = NumParameters.MaxHealth,
                    };
                }

            field.Animals.Add(newAnimal);
            return newAnimal;
        }
    }
}
