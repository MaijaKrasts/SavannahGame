namespace Savannah
{
    using System;

    public class Calculations : ICalculations
    {
        public double Vector(int x1, int x2, int y1, int y2)
        {
            double vector = Math.Sqrt(((x1 - x2) * (x1 - x2)) + ((y1 - y2) * (y1 - y2)));
            return vector;
        }
    }
}
