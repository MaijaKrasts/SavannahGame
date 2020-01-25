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

        Random GetRandom();

        bool KeyAvailable();
       
        void SetCursorPosition();

        void Sleep();
    }
}