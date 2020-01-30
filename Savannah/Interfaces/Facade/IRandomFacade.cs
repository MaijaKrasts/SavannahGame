namespace Savannah.Interfaces.Facade
{
    public interface IRandomFacade
    {
        int GetRandomMinMax(int minValue, int maxValue);

        int GetRandom();
    }
}
