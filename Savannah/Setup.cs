namespace Savannah
{
    using Savannah.Interfaces;

    public class Setup
    {
        public void GameSetup()
        {
            IConsoleFacade facade = new Config.ConsoleFacade();
            ICalculations math = new Calculations();
            IGeneralAnimalAction generalAction = new GeneralAnimalAction();
            IDisplay display = new Display(facade);
            HerbivoreManager herbivore = new HerbivoreManager(generalAction, math, facade);
            CarnivoreManager carnivore = new CarnivoreManager(generalAction, math, facade);
            IAnimalFactory animalFactory = new AnimalFactory(generalAction, facade);
            IFieldFactory fieldFactory = new FieldFactory();

            GameEngine play = new GameEngine(display, herbivore, carnivore, generalAction, animalFactory, fieldFactory);
            play.CreateGamefield();
        }
    }
}
