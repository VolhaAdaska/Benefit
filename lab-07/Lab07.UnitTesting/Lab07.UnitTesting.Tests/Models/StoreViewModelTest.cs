using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using FluentAssertions;
using Lab07.UnitTesting.Models;
using NUnit.Framework;

namespace Lab07.UnitTesting.Tests.Models
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class StoreViewModelTest
    {
        [Test]
        public void StoreViewModel_Test_Property()
        {
            //arrange
            var fixture = new StoreViewModel();

            //act
            var listItem = new List<SelectListItem>();
            fixture.Id = 1;
            fixture.Name = "test";
            fixture.Promocode = "test promocode";
            fixture.StoreTypeId = 1;
            fixture.StoreTypeList = listItem;

            //assert
            fixture.Id.Should().Be(1);
            fixture.Name.Should().BeEquivalentTo("test");
            fixture.Promocode.Should().BeEquivalentTo("test promocode");
            fixture.StoreTypeId.Should().Be(1);
            fixture.StoreTypeList.Should().BeEquivalentTo(listItem);
        }
    }
}
