namespace Savannah
{
    using Ninject;
    using System.Reflection;

    public class Program
    {
        public static void Main(string[] args)
        {

            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            var play = kernel.Get<IGameEngine>();
            play.CreateGamefield();
        }
    }
}