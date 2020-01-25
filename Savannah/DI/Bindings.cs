namespace Savannah.DI
{
    using Ninject.Modules;
    using Savannah.Config;
    using Savannah.Interfaces;

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
            Bind<IHerbivoreManager>().To<HerbivoreManager>();
            Bind<ICarnivoreManager>().To<CarnivoreManager>();
            Bind<IAnimalFactory>().To<AnimalFactory>();
            Bind<IFieldFactory>().To<FieldFactory>();
        }
    }
}
