using System.Diagnostics.CodeAnalysis;
using Lab07.UnitTesting.Controllers;
using NUnit.Framework;
using System.Web.Mvc;

namespace Lab07.UnitTesting.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class HomeControllerTest
    {
        private HomeController homeController;

        [SetUp]
        public void SetUp()
        {
            homeController = new HomeController();
        }

        [Test]
        public void Index()
        {
            //act
            var expectedResult = homeController.Index() as ViewResult;

            //assert
            Assert.IsNotNull(expectedResult);
        }
    }
}