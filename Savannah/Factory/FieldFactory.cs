﻿namespace Savannah
{
    using System.Collections.Generic;
    using AnimalClassLibrary;
    using Savannah.Interfaces;
    using Savannah.Models;

    public class FieldFactory : IFieldFactory
    {
        public Field CreateField()
        {
            Field field = new Field();
            field.Animals = new List<Animal>();
            field.Width = 20;
            field.Height = 20;

            return field;
        }
    }
}
