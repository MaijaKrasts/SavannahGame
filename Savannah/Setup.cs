namespace Savannah
{
    using Savannah.Interfaces;

    public class Setup
    {
        public void GameSetup()
        {
            IConsoleFacade facade = new Config.ConsoleFacade();
            ICalculations math = new Calculations();
            IAnimalValidator validator = new AnimalValidator();
            IDisplay display = new Display(facade);
            IGenericAnimalManager genericAnimal = new GenericAnimalManager();
            HerbivoreManager herbivore = new HerbivoreManager(validator, math, facade, genericAnimal);
            CarnivoreManager carnivore = new CarnivoreManager(validator, math, facade, genericAnimal);
            IAnimalFactory animalFactory = new AnimalFactory(validator, facade);
            IFieldFactory fieldFactory = new FieldFactory();
            

            GameEngine play = new GameEngine(display, herbivore, carnivore, validator, animalFactory, fieldFactory, genericAnimal);
            play.CreateGamefield();
        }
    }
}
