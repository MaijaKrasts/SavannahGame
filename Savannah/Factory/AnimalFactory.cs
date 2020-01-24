namespace Savannah
{
    using Savannah.Interfaces;
    using Savannah.Models;
    using Savannah.Static;

    public class AnimalFactory : IAnimalFactory
    {
        private IAnimalValidator _generalActions;
        private IConsoleFacade _facade;

        public AnimalFactory(IAnimalValidator generalAction, IConsoleFacade facade)
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
            newLion.Symbol = TextParameters.Lion;

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
            newAntelope.Symbol = TextParameters.Antelope;

            field.Animals.Add(newAntelope);
            return newAntelope;
        }
    }
}
