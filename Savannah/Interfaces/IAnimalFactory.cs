namespace Savannah.Interfaces
{
    using Savannah.Models;

    public interface IAnimalFactory
    {

        IAnimal CreateLion(Field field);
        IAnimal CreateAntelope(Field field);
    }
}
