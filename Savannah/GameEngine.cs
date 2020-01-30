namespace Savannah
{

    using Savannah.Interfaces;
    using Savannah.Models;
    using Savannah.Static;

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
            bool fieldCreated = false;

            while (!fieldCreated)
            {
                var additionalField = _genericAnimal.CopyList(field);
                _facade.SetCursorPosition();
                _display.DrawAnimals(field);

                var key = _facade.ConsoleKey();

                var animalKey = key == TextParameters.AntelopeKey
                             || key == TextParameters.LionKey;


                if (key == TextParameters.EnterKey)
                {
                    fieldCreated = true;
                    LifeCycle(field);
                }
                else if (animalKey)
                {
                    _animalFactory.CreateAnimal(key, field);
                }
            }
        }

        public void LifeCycle(Field field)
        { 
            bool keyAvailabe = false;

            while (!keyAvailabe)
            {
                var searchList = _genericAnimal.CopyList(field);
                _genericAnimal.LocateEnemy(field, searchList);
                _genericAnimal.LocateFriend(field, searchList);
                _herbivore.ChooseTheMove(searchList, field);
                _carnivore.ChooseTheMove(searchList, field);
                _facade.SetCursorPosition();
                _display.DrawAnimals(field);
                _facade.Sleep();
                keyAvailabe = _facade.KeyAvailable();
            }
        }
    }
}
