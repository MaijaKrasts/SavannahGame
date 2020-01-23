namespace Savannah
{
    using Savannah.Interfaces;
    using Savannah.Models;

    public class AnimalFactory : IAnimalFactory
    {
        private IGeneralAnimalAction _generalActions;
        private IFacade _facade;

        public AnimalFactory(IGeneralAnimalAction generalAction, IFacade facade)
        {
            _generalActions = generalAction;
            _facade = facade;
        }

        public Animal CreateLion(Field field)
        {
            var rnd = _facade.GetRandom();
            var coordY = rnd.Next(field.Height);
            var coordX = rnd.Next(field.Width);

            if (_generalActions.AnimalExists(coordY, coordX, field))
            {
                CreateLion(field);
            }

            var newLion = new Lion();
            newLion.Alive = true;
            newLion.CoordinateX = coordY;
            newLion.CoordinateY = coordX;
            newLion.Herbivore = false;
            newLion.Symbol = "L";

            field.Animals.Add(newLion);
            return newLion;
        }

        public Animal CreateAntelope(Field field)
        {
            var rnd = _facade.GetRandom();
            var coordX = rnd.Next(field.Height);
            var coordY = rnd.Next(field.Width);

            if (_generalActions.AnimalExists(coordX, coordY, field))
            {
                CreateAntelope(field);
            }

            var newAntelope = new Antelope();
            newAntelope.Alive = true;
            newAntelope.CoordinateX = coordX;
            newAntelope.CoordinateY = coordY;
            newAntelope.Herbivore = true;
            newAntelope.Symbol = "A";

            field.Animals.Add(newAntelope);
            return newAntelope;
        }
    }
}
