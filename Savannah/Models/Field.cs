namespace Savannah.Models
{
    using AnimalLibrary;
    using System.Collections.Generic;

    public class Field
    {
        public int Height { get; set; }

        public int Width { get; set; }

        public List<Animal> Animals { get; set; }
    }
}
