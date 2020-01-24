namespace Savannah.Config
{
    using System;

    public class ConsoleFacade : IConsoleFacade
    {
        public void Write(string text)
        {
            Console.Write(text);
        }

        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }

        public string ReadLine()
        {
            var text = Console.ReadLine();
            return text;
        }

        public void Clear()
        {
            Console.Clear();
        }

        public Random GetRandom()
        {
            Random rnd = new Random();
            return rnd;
        }
    }
}
