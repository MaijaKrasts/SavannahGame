namespace Savannah
{
    using System;
    using System.Threading;
    using Savannah.Interfaces;
    using Savannah.Models;

    public class GameEngine : IGameEngine
    {
        private Field field;
        private IDisplay _display;
        private IAnimalManager _herbivore;
        private IAnimalManager _carnivore;
        private IGeneralAnimalAction _generalAction;
        private IAnimalFactory _animalFactory;
        private IFieldFactory _fieldFactory;

        public GameEngine(IDisplay display, HerbivoreManager herbivore, CarnivoreManager carnivore, IGeneralAnimalAction generalAction, IAnimalFactory animalfactory, IFieldFactory fieldFactory)
        {
            _display = display;
            _herbivore = herbivore;
            _carnivore = carnivore;
            _generalAction = generalAction;
            _animalFactory = animalfactory;
            _fieldFactory = fieldFactory;
        }

        public void CreateGamefield()
        {
            field = _fieldFactory.CreateField();
            var additionalField = _generalAction.AdditionalAnimalField(field);
            bool fieldCreated = false;

            while (!fieldCreated)
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
                    fieldCreated = true;
                    LifeCycle(field);
                }
            }
        }

        public void LifeCycle(Field field)
        { 
            var additionalField = _generalAction.AdditionalAnimalField(field);
            bool animalsInField = true;

            while (animalsInField)
            {
                _carnivore.Locate(field);
                _herbivore.Locate(field);
                _carnivore.ChooseTheMove(additionalField, field);
                _herbivore.ChooseTheMove(additionalField, field);
                Console.SetCursorPosition(0, 0);
                _display.DrawAnimals(field, additionalField);
                Thread.Sleep(500);
            }
        }
    }
}
