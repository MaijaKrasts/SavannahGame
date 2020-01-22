namespace Savannah.Models
{ 
    using System.Collections.Generic;
    using Savannah.Interfaces;

    public class Field
    {
        public int Height { get => 20; }

        public int Width { get => 20; }

        public List<IAnimal> Animals { get; set; }
    }
}
