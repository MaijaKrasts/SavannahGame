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
        private IAnimalFactory _animalFactory;
        private IFieldFactory _fieldFactory;

        public GameLoop(IDisplay display, AntelopeAction herbivore, CarnivoreAction carnivore, IGeneralAnimalAction generalAction, IAnimalFactory animalfactory, IFieldFactory fieldFactory)
        {
            _display = display;
            _herbivore = herbivore;
            _carnivore = carnivore;
            _generalAction = generalAction;
            _animalFactory = animalfactory;
            _fieldFactory = fieldFactory;
        }

        public void Loop()
        {
            field = _fieldFactory.CreateField();
            var additionalField = _generalAction.AdditionalAnimalField(field);
            bool animalsInField = true;

            while (true)
            {
                Console.SetCursorPosition(0, 0);
                _display.DrawAnimals(field, additionalField);

                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.A)
                {
                    _animalFactory.CreateAntelope(field);
                }
                else if (key.Key == ConsoleKey.L)
                {
                   _animalFactory.CreateLion(field);
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
            }

            while (animalsInField)
            {
                _carnivore.Locate(field);
                _herbivore.Locate(field);
                _carnivore.Move(additionalField, field);
                _herbivore.Move(additionalField, field);
                Console.SetCursorPosition(0, 0);
                _display.DrawAnimals(field, additionalField);
                Thread.Sleep(500);
            }
        }
    }
}
