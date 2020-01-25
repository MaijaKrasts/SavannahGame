namespace Savannah.DI
{
    using Ninject.Modules;
    using Savannah.Interfaces;
    using Savannah.Config;

    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IAnimalValidator>().To<AnimalValidator>();
            Bind<ICalculations>().To<Calculations>();
            Bind<IConsoleFacade>().To<ConsoleFacade>();
            Bind<IDisplay>().To<Display>();
            Bind<IGameEngine>().To<GameEngine>();
            Bind<IGenericAnimalManager>().To<GenericAnimalManager>();
            Bind<IAnimalManager>().To<HerbivoreManager>();
            Bind<IAnimalManager>().To<CarnivoreManager>();
            Bind<IAnimalFactory>().To<AnimalFactory>();
            Bind<IFieldFactory>().To<FieldFactory>();
            Bind<ISetup>().To<Setup>();
        }
    }
}
