//using Moq;
//namespace Savannah
//{
//    using NUnit.Framework;
//    using Savannah.Interfaces;
//    using Savannah.Models;
//    using System.Collections.Generic;

//    [TestFixture]
//    public class CarnivorManagerTests
//    {
//        private Mock<IAnimalValidator> generalAnimalActionMock;

//        private Mock<ICalculations> calculationsMock;

//        private Mock<IConsoleFacade> facadeMock;

//        private CarnivoreManager _sut;

//        public CarnivorManagerTests()
//        {
//            generalAnimalActionMock = new Mock<IAnimalValidator>();
//            calculationsMock = new Mock<ICalculations>();
//            facadeMock = new Mock<IConsoleFacade>();

//            _sut = new CarnivoreManager(generalAnimalActionMock.Object, calculationsMock.Object, facadeMock.Object);
//        }

//        [Test]
//        public void Locate_ShouldSucceed()
//        {
//            // ARRANGE
//            var field = new Field { Animals = new List<Animal>(), Height = 20, Width = 20 };
//            var ultimateLocation = 28.2842712474619;
//            var location = 2.23606797749979;
//            var herbivore = new Animal();
//            herbivore.Herbivore = true;
//            herbivore.CoordinateX = 1;
//            herbivore.CoordinateY = 2;
//            field.Animals.Add(herbivore);

//            var carnivore = new Animal();
//            carnivore.Herbivore = false;
//            carnivore.CoordinateX = 3;
//            carnivore.CoordinateY = 3;
//            field.Animals.Add(carnivore);

//            calculationsMock.Setup(cm => cm.Vector(0, field.Height, 0, field.Width)).Returns(ultimateLocation);
//            calculationsMock.Setup(cm => cm.Vector(herbivore.CoordinateX, carnivore.CoordinateX, herbivore.CoordinateY, carnivore.CoordinateY)).Returns(location);

//            // ACT

//            _sut.Locate(field);

//            // ASSERT

//            carnivore.ClosestEnemy = herbivore;
//        }
//    }
//}
