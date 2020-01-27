namespace Savannah
{
    using System.Reflection;
    using Ninject;

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