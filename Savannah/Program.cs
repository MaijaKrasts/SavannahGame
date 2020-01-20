using Savannah.Models;
using System;
using System.Reflection;

namespace Savannah
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Board b = new Board();
            Field field = b.CreateField();

            Display display = new Display();
            AnimalController animal = new AnimalController();

            while(true)
            {
                Console.SetCursorPosition(0, 0);
                display.DrawAnimals(field);

                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.A)
                {
                    animal.CreateAntelope(field);
                }
                else if (key.Key == ConsoleKey.L)
                {
                    animal.CreateLion(field);
                }

            }
        }

    }
}