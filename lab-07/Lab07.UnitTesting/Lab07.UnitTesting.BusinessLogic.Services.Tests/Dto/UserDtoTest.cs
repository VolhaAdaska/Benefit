using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Lab07.UnitTesting.DTO;
using NUnit.Framework;

namespace Lab07.UnitTesting.BusinessLogic.Services.Tests.Dto
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class UserDtoTest
    {
        [Test]
        public void UserDto_Test_Property()
        {
            //Arrange
            var fixture = new UserDto();

            //Act
            fixture.Email = "test@godeltech.com";
            fixture.Password = "test";
            fixture.UserName = "test name";
            fixture.Role = "test role";

            //Assert
            fixture.Email.Should().BeEquivalentTo("test@godeltech.com");
            fixture.Password.Should().BeEquivalentTo("test");
            fixture.UserName.Should().BeEquivalentTo("test name");
            fixture.Role.Should().BeEquivalentTo("test role");
        }
    }
}
