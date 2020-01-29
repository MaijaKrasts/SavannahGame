namespace SavannahClassLibrary
{ 
    using System;

    public interface IConsoleFacade
    {
        void Write(string text);

        void WriteLine(string text);

        void WriteLine();

        string ReadLine();

        void Clear();

        int GetRandomMinMax(int minValue, int maxValue);

        int GetRandom();

        bool KeyAvailable();

        void SetCursorPosition();

        void Sleep();

        ConsoleKey ConsoleKey();
    }
}