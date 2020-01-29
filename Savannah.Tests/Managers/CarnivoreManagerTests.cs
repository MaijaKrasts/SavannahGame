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

            List<Animal> additionalField = new List<Animal>();

            Field field = new Field()
            {
                Width = 20,
                Height = 20,
                Animals = new List<Animal>()
            };

            // ACT

            var result = carnivoreManager.ChooseTheMove(additionalField, field);

            //ASSERT

            Assert.IsInstanceOf<List<Animal>>(result);

        }

        [Test]
        public void ChooseTheMove_WhenFieldIsNull_ShouldReturnNullReferenceException()
        {
            // ARRANGE

            List<Animal> additionalField = new List<Animal>();

            Field field = null;

            // ACT

            //ASSERT
            Assert.Throws<NullReferenceException>(() => carnivoreManager.ChooseTheMove(additionalField, field));
        }

        [Test]
        public void ChooseTheMove_WhenAnimalListIsNull_ShouldReturnNull()
        {
            // ARRANGE

            List<Animal> additionalField = null;

            Field field = new Field()
            {
                Width = 20,
                Height = 20,
                Animals = new List<Animal>()
            };

            // ACT
            var result = carnivoreManager.ChooseTheMove(additionalField, field);

            //ASSERT
            Assert.IsNull(result);
        }
    }
}
