namespace SavannahClassLibrary
{
    using SavannahClassLibrary.Models;

    public interface IGameEngine
    {
        void CreateGamefield();

        void LifeCycle(Field field);
    }
}