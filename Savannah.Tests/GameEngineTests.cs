using AnimalLibrary;
using Moq;
using NUnit.Framework;
using Savannah.Interfaces;
using Savannah.Models;
using System.Collections.Generic;

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
            var searchList = new List<Animal>{
                new Animal()
            };

            bool keyAvailable = true;

            genericAnimalMock.Setup(ga => ga.CopyList(field)).Returns(searchList);
            genericAnimalMock.Setup(ga => ga.LocateEnemy(field, searchList));
            genericAnimalMock.Setup(ga => ga.LocateFriend(field, searchList));
            carnivoreMock.Setup(c => c.ChooseTheMove(searchList, field)).Returns(searchList);
            herbivoreMock.Setup(h => h.ChooseTheMove(searchList, field)).Returns(searchList);
            facadeMock.Setup(f => f.SetCursorPosition());
            displayMock.Setup(d => d.DrawAnimals(field));
            facadeMock.Setup(f => f.Sleep());
            facadeMock.Setup(f => f.KeyAvailable()).Returns(keyAvailable);
            
            // ACT
            gameEngine.LifeCycle(field);

            // ASSERT
            genericAnimalMock.Verify(ga => ga.CopyList(field), Times.Once);
            genericAnimalMock.Verify(ga => ga.LocateEnemy(field, searchList), Times.Once);
            genericAnimalMock.Verify(ga => ga.LocateFriend(field, searchList), Times.Once);
            carnivoreMock.Verify(c => c.ChooseTheMove(searchList, field), Times.Once);
            herbivoreMock.Verify(h => h.ChooseTheMove(searchList, field), Times.Once);
            displayMock.Verify(d => d.DrawAnimals(field), Times.Once);
            facadeMock.Verify(f => f.KeyAvailable(), Times.Once);
        }
    }
}
