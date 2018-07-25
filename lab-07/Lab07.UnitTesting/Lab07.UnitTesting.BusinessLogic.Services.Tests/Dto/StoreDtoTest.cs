using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Lab07.UnitTesting.DTO;
using NUnit.Framework;

namespace Lab07.UnitTesting.BusinessLogic.Services.Tests.Dto
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class StoreDtoTest
    {
        [Test]
        public void StoreDto_Test_Property()
        {
            //Arrange
            var fixture = new StoreDto();

            //Act
            fixture.Id = 1;
            fixture.Name = "test";
            fixture.Promocode = "test promocode";
            fixture.StoreTypeId = 1;

            //Assert
            fixture.Id.Should().Be(1);
            fixture.Name.Should().BeEquivalentTo("test");
            fixture.Promocode.Should().BeEquivalentTo("test promocode");
            fixture.StoreTypeId.Should().Be(1);
        }
    }
}