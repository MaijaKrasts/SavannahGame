namespace Savannah
{
    using System;

    public interface IFacade
    {
        void Write(string text);

        void WriteLine(string text);

        void WriteLine();

        string ReadLine();

        void Clear();

        Random GetRandom();
    }
}