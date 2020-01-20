﻿using Savannah.Models;
using Savannah.Static;
using System;

namespace Savannah
{
    public class Display
    {
        public void DrawAnimals(Field field)
        {
            for (int currentRow = 0; currentRow < field.Height; currentRow++)
            {
                for (int currentColumn = 0; currentColumn < field.Width; currentColumn++)
                {
                    var currentAnimal = field.Animals.Find(u => u.CoordinateX == currentRow && u.CoordinateY == currentColumn);

                    if (currentAnimal == null)
                    {
                        Console.Write(Texts.Empthy);
                    }
                    else if(currentAnimal.Alive)
                    {
                        Console.Write(currentAnimal.Symbol);
                    }

                    if (currentColumn == field.Width - 1)
                    {
                        Console.WriteLine(Texts.Return);
                    }
                }
            }
        }
    }
}