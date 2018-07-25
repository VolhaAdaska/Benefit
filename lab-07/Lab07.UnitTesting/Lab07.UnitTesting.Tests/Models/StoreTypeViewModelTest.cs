using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Lab07.UnitTesting.Models;
using NUnit.Framework;

namespace Lab07.UnitTesting.Tests.Models
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class StoreTypeViewModelTest
    {
        [Test]
        public void StoreTypeViewModel_Test_Property()
        {
            //arrange
            var fixture = new StoreTypeViewModel();

            //act
            fixture.Id = 1;
            fixture.Name = "test";

            //assert
            fixture.Id.Should().Be(1);
            fixture.Name.Should().BeEquivalentTo("test");
        }
    }
}