namespace Savannah
{
    using Savannah.Interfaces;

    public class Program
    {
        public static void Main(string[] args)
        {
            IFacade facade = new Config.Facade();
            ICalculations math = new Calculations();
            IGeneralAnimalAction generalAction = new GeneralAnimalAction();
            IDisplay display = new Display(facade);
            AntelopeAction herbivore = new AntelopeAction(generalAction, math, facade);
            CarnivoreAction carnivore = new CarnivoreAction(generalAction, math, facade);
            IAnimalFactory factory = new AnimalFactory(generalAction, facade);
            IFieldFactory fieldFactory = new FieldFactory();

            GameLoop play = new GameLoop(display, herbivore, carnivore, generalAction, factory, fieldFactory);
            play.Loop();
        }
    }
}