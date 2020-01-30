using AnimalLibrary;
using LionLibrary;
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

            facadeMock.Setup(f => f.GetRandomMinMax(0, 20)).Returns(0);
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

            int coordX = 3;
            int coordY = 3;

            facadeMock.Setup(f => f.GetRandomMinMax(0, 20)).Returns(3);

            ConsoleKey key = TextParameters.LionKey;

            Lion lion = new Lion();
            lion.Alive = true;
            lion.ClosestEnemy = null;
            lion.ClosestMate = null;
            lion.CoordinateX = coordX;
            lion.CoordinateY = coordY;
            lion.Health = NumParameters.MaxHealth;
            lion.IsHerbivore = false;
            lion.MatingCount = NumParameters.InitialMatingCount;
            lion.Symbol = "L";
            lion.Key = ConsoleKey.L;
            
            //ACT
            var result = animalFactory.CreateAnimal(key, field);

            // ASSERT
            Assert.AreEqual(lion.IsHerbivore, result.IsHerbivore);
            Assert.AreEqual(lion.Symbol, result.Symbol);
            Assert.AreEqual(lion.Key, result.Key);
        }
    }
}
