namespace Savannah
{
    using System;
    using System.Collections.Generic;
    using Savannah.Interfaces;
    using Savannah.Models;
    using Savannah.Static;

    public class Board
    {
        private static int origRow;
        private static int origCol;

        private Texts texts;
        private AnimalController animalcontrol;
        private Display display;

        public Board()
        {
            texts = new Texts();
            animalcontrol = new AnimalController();
            display = new Display();
        }

        public static void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        public void DrawBorders()
        {
            Console.Clear();
            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;

            // Draw the left side of a 10x10 rectangle, from top to bottom.
            for (var num = 0; num < 10; num++)
            {
                WriteAt(Texts.BoardVer, 0, num);
            }

            // Draw the bottom side, from left to right.
            WriteAt(Texts.BoardHor, 1, 9);

            // Draw the right side, from bottom to top.
            for (var num = 9; num >= 0; num--)
            {
                WriteAt(Texts.BoardVer, 19, num);
            }

            // Draw the top side, from right to left.
            WriteAt(Texts.BoardHor, 1, 0);

            WriteAt("All done! Usable field between [1,1] and [19,9]", 0, 12);
            Console.WriteLine();
        }

        public Field CreateField()
        {
            Field field = new Field();
            field.Animals = new List<IAnimal>();
            field.Height = 15;
            field.Width = 15;

            return field;
        }
    }
}
