using Moq;
using NUnit.Framework;
using Savannah.Interfaces;
using Savannah.Models;
using System.Collections.Generic;


namespace Savannah
{
    [TestFixture]
    public class CarnivorActionTests
    {
        private Mock<IGeneralAnimalAction> generalAnimalActionMock;

        private Mock<ICalculations> calculationsMock;

        private Mock<IFacade> facadeMock;

        private CarnivoreAction _sut;

        public CarnivorActionTests()
        {
            generalAnimalActionMock = new Mock<IGeneralAnimalAction>();
            calculationsMock = new Mock<ICalculations>();
            facadeMock = new Mock<IFacade>();

            _sut = new CarnivoreAction(generalAnimalActionMock.Object, calculationsMock.Object, facadeMock.Object);
        }

        [Test]
        public void Locate_ShouldSucceed()
        {
            // ARRANGE
            var field = new Field { Animals = new List<Animal>(), Height = 20, Width = 20 };
            var ultimateLocation = 28.2842712474619;
            var location = 2.23606797749979;
            var animal = new Animal();
            animal.Herbivore = true;
            animal.CoordinateX = 1;
            animal.CoordinateY = 2;
            field.Animals.Add(animal);

            var secondAnimal = new Animal();
            secondAnimal.Herbivore = false;
            secondAnimal.CoordinateX = 3;
            secondAnimal.CoordinateY = 3;
            field.Animals.Add(secondAnimal);

            calculationsMock.Setup(cm => cm.Vector(0, field.Height, 0, field.Width)).Returns(ultimateLocation);
            calculationsMock.Setup(cm => cm.Vector(animal.CoordinateX, secondAnimal.CoordinateX, animal.CoordinateY, secondAnimal.CoordinateY)).Returns(location);

            // ACT

            _sut.Locate(field);

            // ASSERT

            secondAnimal.ClosestEnemy = animal;
            animal.ClosestEnemy = secondAnimal;
        }

    }
}
