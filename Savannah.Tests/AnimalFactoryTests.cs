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

        private AnimalFactory animalFactory;

        [SetUp]
        public void Setup()
        {
            animalValidatorMock = new Mock<IAnimalValidator>();
            facadeMock = new Mock<IConsoleFacade>(MockBehavior.Strict);

            animalFactory = new AnimalFactory(animalValidatorMock.Object, facadeMock.Object);
        }

        [Test]
        public void CreateAnimal_WhenLionKeyisPassedDown_ShouldCreateInstanceOfAnimal()
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

            //ACT
            var result = animalFactory.CreateAnimal(key, field);

            // ASSERT
            Assert.IsInstanceOf<Animal>(result);
        }

        [Test]
        public void CreateAnimal_WhenLionKeyisPassedDown_ShouldCreateALion()
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


            //ACT
            var result = animalFactory.CreateAnimal(key, field);

            // ASSERT
            Assert.IsInstanceOf<Animal>(result);
        }
    }
}
