using AnimalLibrary;
using Moq;
using NUnit.Framework;
using Savannah.Interfaces;
using Savannah.Models;
using System;
using System.Collections.Generic;

namespace Savannah.Tests.Managers
{
    [TestFixture]
    public class CarnivoreManagerTests
    {
        private Mock<IAnimalValidator> validatorMock;
        private Mock<ICalculations> mathMock;
        private Mock<IConsoleFacade> facadeMock;
        private Mock<IGenericAnimalManager> genericAnimalMock;

        private CarnivoreManager carnivoreManager;

        [SetUp]
        public void Setup()
        {
            validatorMock = new Mock<IAnimalValidator>(MockBehavior.Strict);
            mathMock = new Mock<ICalculations>(MockBehavior.Strict);
            facadeMock = new Mock<IConsoleFacade>(MockBehavior.Strict);
            genericAnimalMock = new Mock<IGenericAnimalManager>(MockBehavior.Strict);

            carnivoreManager = new CarnivoreManager(validatorMock.Object, mathMock.Object, facadeMock.Object, genericAnimalMock.Object);
        }

        [Test]
        public void ChooseTheMove_WhenAllInputIsCorrect_ShouldCreateInstanceOfAnimalList()
        {
            // ARRANGE

            List<Animal> searchList = new List<Animal>();

            Field field = new Field()
            {
                Width = 20,
                Height = 20,
                Animals = new List<Animal>()
            };

            // ACT

            var result = carnivoreManager.ChooseTheMove(searchList, field);

            //ASSERT

            Assert.IsInstanceOf<List<Animal>>(result);

        }


        //dont know how to finish this..
        /*
        public void ChooseTheMove_WhenAllInputIsCorrect_ReturnNotSame_DueToChangedField()
        {
            // ARRANGE

            List<Animal> searchList = new List<Animal>();

            Field field = new Field()
            {
                Width = 20,
                Height = 20,
                Animals = new List<Animal>()

            };

            Animal carni = new Animal()
            {
                CoordinateX = 1,
                CoordinateY = 2,
                ClosestEnemy = null,
            };

            searchList.Add(carni);


            // ACT

            var result = carnivoreManager.ChooseTheMove(searchList, field);

            //ASSERT

            Assert.IsInstanceOf<List<Animal>>(result);

        }*/

        [Test]
        public void ChooseTheMove_WhenFieldIsNull_ShouldReturnEmpty()
        {
            // ARRANGE

            List<Animal> searchList = new List<Animal>();
            Field field = null;

            // ACT
            var result = carnivoreManager.ChooseTheMove(searchList, field);
            
            //ASSERT
            Assert.IsEmpty(result);
        }

        [Test]
        public void ChooseTheMove_WhenAnimalListIsNull_ShouldReturnNullReferenceException()
        {
            // ARRANGE

            List<Animal> searchList = null;

            Field field = new Field()
            {
                Width = 20,
                Height = 20,
                Animals = new List<Animal>()
            };

            // ACT & ASSERT
            Assert.Throws<NullReferenceException>(() => carnivoreManager.ChooseTheMove(searchList, field));
        }
    }
}
