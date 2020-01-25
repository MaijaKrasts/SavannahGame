using Moq;
using NUnit.Framework;
using Savannah.Interfaces;
using Savannah.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Savannah.Tests
{
    [TestFixture]
   public class GameEngineTests
    {
        private Mock<IDisplay> displayMock;
        private Mock<IConsoleFacade> facadeMock;
        private Mock<IHerbivoreManager> herbivoreMock;
        private Mock<ICarnivoreManager> carnivoreMock;
        private Mock<IAnimalFactory> animalFactoryMock;
        private Mock<IFieldFactory> fieldFactoryMock;
        private Mock<IGenericAnimalManager> genericAnimalMock;


        private GameEngine gameEngine;

        [SetUp]
        public void Setup()
        {
            displayMock = new Mock<IDisplay>(MockBehavior.Strict);
            facadeMock = new Mock<IConsoleFacade>(MockBehavior.Strict);
            herbivoreMock = new Mock<IHerbivoreManager>(MockBehavior.Strict);
            carnivoreMock = new Mock<ICarnivoreManager>(MockBehavior.Strict);
            animalFactoryMock = new Mock<IAnimalFactory>(MockBehavior.Strict);
            fieldFactoryMock = new Mock<IFieldFactory>(MockBehavior.Strict);
            genericAnimalMock = new Mock<IGenericAnimalManager>(MockBehavior.Strict);
            gameEngine = new GameEngine(displayMock.Object, facadeMock.Object, herbivoreMock.Object, carnivoreMock.Object, animalFactoryMock.Object, fieldFactoryMock.Object, genericAnimalMock.Object);
        }

        [Test]
        public void LifeCycle_WhenAllInputIsCorrect_ShouldSuceed()
        {
            // ARRANGE
            var field = new Field();
            var animalList = new List<Animal>{
                new Animal()
            };

            bool keyAvailable = true;

            genericAnimalMock.Setup(ga => ga.AdditionalAnimalList(field)).Returns(animalList);
            genericAnimalMock.Setup(ga => ga.LocateEnemy(field));
            carnivoreMock.Setup(c => c.ChooseTheMove(animalList, field)).Returns(animalList);
            herbivoreMock.Setup(h => h.ChooseTheMove(animalList, field)).Returns(animalList);
            facadeMock.Setup(f => f.SetCursorPosition());
            displayMock.Setup(d => d.DrawAnimals(field, animalList));
            displayMock.Setup(d => d.ResetValues(field, animalList));
            facadeMock.Setup(f => f.Sleep());
            facadeMock.Setup(f => f.KeyAvailable()).Returns(keyAvailable);
            
            // ACT
            gameEngine.LifeCycle(field);

            // ASSERT
            genericAnimalMock.Verify(ga => ga.AdditionalAnimalList(field), Times.Once);
            genericAnimalMock.Verify(ga => ga.LocateEnemy(field), Times.Once);
            carnivoreMock.Verify(c => c.ChooseTheMove(animalList, field), Times.Once);
            herbivoreMock.Verify(h => h.ChooseTheMove(animalList, field), Times.Once);
            displayMock.Verify(d => d.DrawAnimals(field, animalList), Times.Once);
            displayMock.Verify(d => d.ResetValues(field, animalList), Times.Once);
            facadeMock.Verify(f => f.KeyAvailable(), Times.Once);
        }
    }
}
