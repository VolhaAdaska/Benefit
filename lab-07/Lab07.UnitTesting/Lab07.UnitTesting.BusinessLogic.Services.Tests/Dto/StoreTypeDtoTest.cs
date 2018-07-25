using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Lab07.UnitTesting.DTO;
using NUnit.Framework;

namespace Lab07.UnitTesting.BusinessLogic.Services.Tests.Dto
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class StoreTypeDtoTest
    {
        [Test]
        public void StoreTypeDto_Test_Property()
        {
            //Arrange
            var fixture = new StoreTypeDto();

            //Act
            fixture.Id = 1;
            fixture.Name = "test";

            //Assert
            fixture.Id.Should().Be(1);
            fixture.Name.Should().BeEquivalentTo("test");
        }
    }
}