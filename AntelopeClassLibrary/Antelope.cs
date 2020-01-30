using AnimalLibrary;
using System;

namespace AntelopeLibrary
{
    [Serializable]
    public class Antelope : Animal
    {
        public Antelope()
        {
            Key = ConsoleKey.A;
            Symbol = "A";
            IsHerbivore = true;
        }
    }
}
