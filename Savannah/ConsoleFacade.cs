﻿namespace Savannah.Config
{
    using System;
    using System.Threading;

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

        public int GetRandom(int minValue, int maxValue)
        {
            Random rnd = new Random();
            var num = rnd.Next(minValue, maxValue);
            return num;
        }

        public int GetRandom()
        {
            Random rnd = new Random();
            int num = rnd.Next();
            return num;
        }

        public bool KeyAvailable()
        {
            return Console.KeyAvailable;
        }

        public ConsoleKey ConsoleKey()
        {
            var key = Console.ReadKey(true);
            return key.Key;
        }

        public void SetCursorPosition()
        {
            Console.SetCursorPosition(0, 0);
        }

        public void Sleep()
        {
            Thread.Sleep(500);
        }
    }
}
