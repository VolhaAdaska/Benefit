using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Lab07.UnitTesting.Models;
using NUnit.Framework;

namespace Lab07.UnitTesting.Tests.Models
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class LoginModelTest
    {
        [Test]
        public void LoginModel_Test_Property()
        {
            //arrange
            var fixture = new LoginModel();

            //act
            fixture.Email = "test@godeltech.com";
            fixture.Password = "test";

            //assert
            fixture.Email.Should().BeEquivalentTo("test@godeltech.com");
            fixture.Password.Should().BeEquivalentTo("test");
        }
    }
}