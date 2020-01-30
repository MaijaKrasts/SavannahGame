﻿namespace Savannah.Models
{
    using AnimalClassLibrary;
    using System.Collections.Generic;

    public class Field
    {
        public int Height { get; set; }

        public int Width { get; set; }

        public List<Animal> Animals { get; set; }
    }
}
