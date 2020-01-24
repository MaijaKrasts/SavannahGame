namespace Savannah
{
    using Savannah.Models;

    public interface IGameEngine
    {
        void CreateGamefield();

        void LifeCycle(Field field);
    }
}