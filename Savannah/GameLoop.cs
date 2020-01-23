namespace Savannah
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Savannah.Interfaces;
    using Savannah.Models;

    public class GameLoop : IGameLoop
    {
        private Field field;
        private IDisplay _display;
        private IAnimalAction _herbivore;
        private IAnimalAction _carnivore;
        private IGeneralAnimalAction _generalAction;
        private IAnimalFactory _factory;

        public GameLoop(IDisplay display, AntelopeAction herbivore, CarnivoreAction carnivore, IGeneralAnimalAction generalAction,IAnimalFactory factory)
        {
            field = new Field();
            field.Animals = new List<IAnimal>();
            _display = display;
            _herbivore = herbivore;
            _carnivore = carnivore;
            _generalAction = generalAction;
            _factory = factory;
        }

        public void Loop()
        {
            var additionalAnimals = _generalAction.AdditionalAnimalField(field);
            bool animalsInField = true;

            while (true)
            {
                Console.SetCursorPosition(0, 0);
                _display.DrawAnimals(field, additionalAnimals);

                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.A)
                {
                    _factory.CreateAntelope(field);
                }
                else if (key.Key == ConsoleKey.L)
                {
                   _factory.CreateLion(field);
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
            }

            while (animalsInField)
            {
                _carnivore.Locate(field);
                _carnivore.MoveWithoutEnemies(field, additionalAnimals);
                _herbivore.MoveWithoutEnemies(field, additionalAnimals);
                Console.SetCursorPosition(0, 0);
                _display.DrawAnimals(field, additionalAnimals);
                additionalAnimals = _generalAction.AdditionalAnimalField(field);
                Thread.Sleep(500);
            }
        }
    }
}
