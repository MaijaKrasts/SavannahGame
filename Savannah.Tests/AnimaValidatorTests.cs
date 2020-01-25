using NUnit.Framework;
using Savannah.Models;
using System;
using System.Collections.Generic;

namespace Savannah.Tests
{
    public class AnimalValidatorTests
    {
        private AnimalValidator animalValidator;

        [SetUp]
        public void Setup()
        {
            animalValidator = new AnimalValidator();
        }

        [Test]
        public void AnimalExists_WhenAllInputIsCorrect_ShouldSucceed()
        {
            // ARRANGE

            int coordX = 3;
            int coordY = 4;

            Field field = new Field()
            {
                Animals = new List<Animal>
                {
                     new Animal()
                     {
                         CoordinateX = coordX,
                         CoordinateY = coordY
                     }
                }
            };


            // ACT
            var result = animalValidator.AnimalExists(coordX, coordY, field);

            // ASSERT
            Assert.IsTrue(result);
        }

        [Test]
        public void AnimalExists_WhenFieldIsNull_ShouldReturnNullReferenceException()
        {
            // ARRANGE

            int coordX = 3;
            int coordY = 4;

            Field field = null;

            // ACT && ASSERT
            Assert.Throws<NullReferenceException>(() => animalValidator.AnimalExists(coordX, coordY, field));
        }
    }
}