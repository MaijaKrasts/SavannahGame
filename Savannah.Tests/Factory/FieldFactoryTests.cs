using NUnit.Framework;
using Savannah.Models;
using System.Collections.Generic;

namespace Savannah.Tests.Factory
{
    [TestFixture]
    public class FieldFactoryTests
    {
        private FieldFactory fieldFactory;

        [SetUp]
        public void Setup()
        {
            fieldFactory = new FieldFactory();
        }

        [Test]
        public void CreateField_WhenNoInputIsPassedDown_ShouldCreateInstanceOfField()
        {
            // ARRANGE

            // ACT

            var result = fieldFactory.CreateField();

            //ASSERT

            Assert.IsInstanceOf<Field>(result);

        }

        [Test]
        public void CreateField_WhenNoInputIsPassedDown_ShouldCreateField()
        {
            // ARRANGE

            Field field = new Field()
            {
                Width = 20,
                Height = 20,
                Animals = new List<Animal>()
            };

            // ACT

            var result = fieldFactory.CreateField();

            //ASSERT
            Assert.AreEqual(field.Width, result.Width);
        }
    }
}
