namespace Savannah
{
    using System;

    public interface IConsoleFacade
    {
        void Write(string text);

        void WriteLine(string text);

        void WriteLine();

        string ReadLine();

        void Clear();

        bool KeyAvailable();

        void SetCursorPosition();

        void Sleep();

        ConsoleKey ConsoleKey();
    }
}