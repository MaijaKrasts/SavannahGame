namespace Savannah
{
    using System.Collections.Generic;
    using Savannah.Interfaces;
    using Savannah.Models;
    using Savannah.Static;

    public class Display : IDisplay
    {
        private IFacade _facade;

        public Display(IFacade facade)
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
                        _facade.Write(Texts.Empty);
                    }
                    else if (currentAnimal.Alive)
                    {
                        _facade.Write(currentAnimal.Symbol);
                    }

                    if (currentColumn == field.Width - 1)
                    {
                        _facade.WriteLine(Texts.Return);
                    }
                }
            }

            field.Animals = additionalAnimal;
        }
    }
}
