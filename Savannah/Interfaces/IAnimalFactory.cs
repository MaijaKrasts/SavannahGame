namespace Savannah.Interfaces
{
    using Savannah.Models;

    public interface IAnimalFactory
    {

        Animal CreateLion(Field field);
        Animal CreateAntelope(Field field);
    }
}
