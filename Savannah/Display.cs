namespace Savannah
{
    using Savannah.Interfaces;
    using Savannah.Models;
    using Savannah.Static;

    public class Display : IDisplay
    {
        private IConsoleFacade _facade;
        private IGenericAnimalManager _genericAnimal;

        public Display(IConsoleFacade facade, IGenericAnimalManager genericAnimal)
        {
            _facade = facade;
            _genericAnimal = genericAnimal;
        }

        public void DrawAnimals(Field field)
        {
            for (int currentRow = 0; currentRow < field.Height; currentRow++)
            {
                for (int currentColumn = 0; currentColumn < field.Width; currentColumn++)
                {
                    var currentAnimal = _genericAnimal.FindInField (field, currentColumn, currentRow);

                    if (currentAnimal == null)
                    {
                        _facade.Write(TextParameters.Empty);
                    }
                    else if (currentAnimal.Alive)
                    {
                        _facade.Write(currentAnimal.Symbol);
                    }
                    if (currentColumn == field.Width - 1)
                    {
                        _facade.WriteLine(TextParameters.Return);
                    }
                }
            }
        }
    }
}
