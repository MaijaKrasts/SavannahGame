namespace Savannah
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Savannah.Interfaces;
    using Savannah.Models;

    public class GameLoop
    {
        private Field field;
        private Display display;
        private AntelopeAction antelope;
        private LionAction lion;
        private GeneralAnimalAction generalAnimal;

        public GameLoop()
        {
            field = new Field();
            field.Animals = new List<IAnimal>();
            display = new Display();
            antelope = new AntelopeAction();
            lion = new LionAction();
            generalAnimal = new GeneralAnimalAction();
        }

        public void Loop()
        {
            var additionalAnimals = generalAnimal.AdditionalAnimalField(field);

            while (true)
            {
                Console.SetCursorPosition(0, 0);
                display.DrawAnimals(field, additionalAnimals);

                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.A)
                {
                    antelope.Create(field);
                }
                else if (key.Key == ConsoleKey.L)
                {
                    lion.Create(field);
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
            }

            while (true)
            {
                lion.Locate(field);
                lion.MoveWithoutEnemies(field, additionalAnimals);
                antelope.MoveWithoutEnemies(field, additionalAnimals);
                Console.SetCursorPosition(0, 0);
                display.DrawAnimals(field, additionalAnimals);
                additionalAnimals = generalAnimal.AdditionalAnimalField(field);
                Thread.Sleep(500);
            }
        }
    }
}
