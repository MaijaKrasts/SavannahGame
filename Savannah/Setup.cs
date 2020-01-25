namespace Savannah
{
    using Savannah.Interfaces;

    public class Setup : ISetup
    {
        public void GameSetup()
        {
            IConsoleFacade facade = new Config.ConsoleFacade();
            ICalculations math = new Calculations();
            IAnimalValidator validator = new AnimalValidator();
            IDisplay display = new Display(facade);
            IAnimalFactory animalFactory = new AnimalFactory(validator, facade);
            IGenericAnimalManager genericAnimal = new GenericAnimalManager(math, validator, animalFactory);
            HerbivoreManager herbivore = new HerbivoreManager(validator, math, facade, genericAnimal);
            CarnivoreManager carnivore = new CarnivoreManager(validator, math, facade, genericAnimal);
            IFieldFactory fieldFactory = new FieldFactory();

            GameEngine play = new GameEngine(display, herbivore, carnivore, animalFactory, fieldFactory, genericAnimal);
            play.CreateGamefield();
        }
    }
}
