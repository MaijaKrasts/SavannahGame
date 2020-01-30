namespace Savannah.Facade
{
    using Savannah.Interfaces.Facade;
    using System;

    public class RandomFacade : IRandomFacade
    {
        public int GetRandomMinMax(int minValue, int maxValue)
        {
            Random rnd = new Random();
            var num = rnd.Next(minValue, maxValue);
            return num;
        }

        public int GetRandom()
        {
            Random rnd = new Random();
            int num = rnd.Next();
            return num;
        }
    }
}
