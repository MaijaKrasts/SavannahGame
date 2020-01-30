using AnimalClassLibrary;
using System;

namespace AntelopeClassLibrary
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
