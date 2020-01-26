using Moq;
using NUnit.Framework;
using Savannah.Interfaces;
using Savannah.Models;
using Savannah.Static;
using System;
using System.Collections.Generic;

namespace Savannah.Tests
{
    [TestFixture]
    public class AnimalFactoryTests
    {
        private Mock<IAnimalValidator> animalValidatorMock;
        private Mock<IConsoleFacade> facadeMock;
        private Mock<TextParameters> texts;

        private AnimalFactory animalFactory;

        [SetUp]
        public void Setup()
        {
            animalValidatorMock = new Mock<IAnimalValidator>(MockBehavior.Strict);
            facadeMock = new Mock<IConsoleFacade>(MockBehavior.Strict);

            animalFactory = new AnimalFactory(animalValidatorMock.Object, facadeMock.Object);
        }

        [Test]
        public void CreateAnimal_WhenAllInputIsCorrect_ShouldSucceed()
        {
            // ARRANGE

            Field field = new Field()
            {
                Width = 20,
                Height = 20,
                Animals = new List<Animal>
                {
                     new Animal()
                }
            };

            ConsoleKey key = TextParameters.LionKey;
            //int coordX = 4;
            //int coordY = 5;

            Animal Lion = new Animal()
            {
                //Alive = true,
                //CoordinateX = coordX,
                //CoordinateY = coordY,
                //Herbivore = false,
                //Symbol = TextParameters.Lion,
                //Health = NumberParameters.MaxHealth,
            };

            // ACT
            
            var result = animalFactory.CreateAnimal(key, field);

            // ASSERT
            Assert.AreSame(Lion, result);
        }
    }
}
