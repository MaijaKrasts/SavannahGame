namespace LionLibrary
{
    using AnimalLibrary;
    using System;

    [Serializable]
    public class Lion : Animal
    {
        public Lion()
        {
            Key = ConsoleKey.L;
            Symbol = "L";
            IsHerbivore = false;
        }
    }
}
