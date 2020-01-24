namespace Savannah
{
    using System.Collections.Generic;
    using Savannah.Interfaces;
    using Savannah.Models;
    using Savannah.Static;

    public class Display : IDisplay
    {
        private IConsoleFacade _facade;

        public Display(IConsoleFacade facade)
        {
            _facade = facade;
        }

        public void DrawAnimals(Field field, List<Animal> additionalAnimal)
        {
            for (int currentRow = 0; currentRow < field.Height; currentRow++)
            {
                for (int currentColumn = 0; currentColumn < field.Width; currentColumn++)
                {
                    var currentAnimal = additionalAnimal.Find(u => u.CoordinateX == currentRow && u.CoordinateY == currentColumn);

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

            field.Animals = additionalAnimal;
        }
    }
}
