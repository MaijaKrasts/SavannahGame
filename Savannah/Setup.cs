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
            HerbivoreManager herbivore = new HerbivoreManager(validator, math, facade);
            CarnivoreManager carnivore = new CarnivoreManager(validator, math, facade);
            IAnimalFactory animalFactory = new AnimalFactory(validator, facade);
            IFieldFactory fieldFactory = new FieldFactory();

            GameEngine play = new GameEngine(display, herbivore, carnivore, validator, animalFactory, fieldFactory);
            play.CreateGamefield();
        }
    }
}
