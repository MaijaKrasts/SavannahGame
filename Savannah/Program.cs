namespace Savannah
{
    using Savannah.Interfaces;

    public class Program
    {
        public static void Main(string[] args)
        {
            Setup config = new Setup();
            config.GameSetup();
        }
    }
}