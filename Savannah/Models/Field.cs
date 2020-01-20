namespace Savannah.Models
{ 
    using System.Collections.Generic;
    using Savannah.Interfaces;

    public class Field
    {
        public int Height { get; set; }

        public int Width { get; set; }

        public List<IAnimal> Animals { get; set; }

        public List<Antelope> Antelopes { get; set; }
    }
}
