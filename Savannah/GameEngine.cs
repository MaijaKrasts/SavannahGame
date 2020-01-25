﻿namespace Savannah
{
    using System;
    using System.Threading;
    using Savannah.Interfaces;
    using Savannah.Models;

    public class GameEngine : IGameEngine
    {
        private Field field;
        private IDisplay _display;
        private IConsoleFacade _facade;
        private IHerbivoreManager _herbivore;
        private ICarnivoreManager _carnivore;
        private IAnimalFactory _animalFactory;
        private IFieldFactory _fieldFactory;
        private IGenericAnimalManager _genericAnimal;

        public GameEngine(IDisplay display, IConsoleFacade facade, IHerbivoreManager herbivore, ICarnivoreManager carnivore, IAnimalFactory animalfactory, IFieldFactory fieldFactory, IGenericAnimalManager genericAnimal)
        {
            _display = display;
            _facade = facade;
            _herbivore = herbivore;
            _carnivore = carnivore;
            _animalFactory = animalfactory;
            _fieldFactory = fieldFactory;
            _genericAnimal = genericAnimal;
        }

        public void CreateGamefield()
        {
            field = _fieldFactory.CreateField();
            var additionalField = _genericAnimal.AdditionalAnimalList(field);
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
            var additionalField = _genericAnimal.AdditionalAnimalList(field);
            bool keyAvailabe = false;

            while (!keyAvailabe)
            {
                _genericAnimal.LocateEnemy(field);
                _carnivore.ChooseTheMove(additionalField, field);
                _herbivore.ChooseTheMove(additionalField, field);
                _facade.SetCursorPosition();
                _display.DrawAnimals(field, additionalField);
                _display.ResetValues(field, additionalField);
                _facade.Sleep();

                keyAvailabe = _facade.KeyAvailable();
            }
        }
    }
}
