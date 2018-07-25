using System.Diagnostics.CodeAnalysis;
using Lab07.UnitTesting.Controllers;
using NUnit.Framework;
using System.Web.Mvc;

namespace Lab07.UnitTesting.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class ErrorControllerTest
    {
        private ErrorController errorController;

        [SetUp]
        public void SetUp()
        {
            errorController = new ErrorController();
        }

        [Test]
        public void NotFound()
        {
            //act
            var expectedResult = errorController.NotFound() as ViewResult;

            //assert
            Assert.IsNotNull(expectedResult);
        }
    }
}