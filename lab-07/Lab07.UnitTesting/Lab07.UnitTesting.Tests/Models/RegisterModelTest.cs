using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Lab07.UnitTesting.Models;
using NUnit.Framework;

namespace Lab07.UnitTesting.Tests.Models
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class RegisterModelTest
    {
        [Test]
        public void RegisterModel_Test_Property()
        {
            //arrange
            var fixture = new RegisterModel();

            //act
            fixture.Email = "test@godeltech.com";
            fixture.Password = "test";
            fixture.ConfirmPassword = "test";
            fixture.UserName = "test name";

            //assert
            fixture.Email.Should().BeEquivalentTo("test@godeltech.com");
            fixture.Password.Should().BeEquivalentTo("test");
            fixture.ConfirmPassword.Should().BeEquivalentTo("test");
            fixture.UserName.Should().BeEquivalentTo("test name");
        }
    }
}