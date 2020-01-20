namespace Savannah.Interfaces
{
    public interface IAnimal
    {
        bool Alive { get; set; }

        int CoordinateX { get; set; }

        int CoordinateY { get; set; }
        
        string Symbol { get;}
    }
}
