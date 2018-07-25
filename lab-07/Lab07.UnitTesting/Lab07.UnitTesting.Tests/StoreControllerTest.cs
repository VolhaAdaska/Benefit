using System;
using AutoMapper;
using Lab07.UnitTesting.BusinessLogic.Interfaces;
using Lab07.UnitTesting.Controllers;
using Lab07.UnitTesting.DTO;
using Lab07.UnitTesting.Models;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using FluentAssertions;

namespace Lab07.UnitTesting.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class StoreControllerTest
    {
        private StoreController storeController;
        private Mock<IStoreService> storeService;
        private Mock<IStoreTypeService> storeTypeService;
        private Mock<IMapper> mapper;

        [SetUp]
        public void SetUp()
        {
            storeService = new Mock<IStoreService>();
            storeTypeService = new Mock<IStoreTypeService>();
            mapper = new Mock<IMapper>();

            storeController = new StoreController(storeService.Object, storeTypeService.Object, mapper.Object);
        }

        [Test]
        public void StoreController_When_StoreService_Null_Should_ThrowArgumentNullException()
        {
            //assert
            Assert.Throws<ArgumentNullException>(() => new StoreController(null, storeTypeService.Object, mapper.Object));
        }

        [Test]
        public void StoreController_When_StoreTypeService_Null_Should_ThrowArgumentNullException()
        {
            //assert
            Assert.Throws<ArgumentNullException>(() => new StoreController(storeService.Object, null, mapper.Object));
        }

        [Test]
        public void StoreController_When_Mapper_Null_Should_ThrowArgumentNullException()
        {
            //assert
            Assert.Throws<ArgumentNullException>(() => new StoreController(storeService.Object, storeTypeService.Object, null));
        }

        [Test]
        public void StoreTypeList_Get()
        {
            //arrange
            var listStoreTypeDtos = new List<StoreTypeDto>();
            var listStoreTypeViewModels = new List<StoreTypeViewModel>();
            storeTypeService.Setup(x => x.GetAllStoreType()).Returns(listStoreTypeDtos);
            mapper.Setup(x => x.Map<IList<StoreTypeDto>, IList<StoreTypeViewModel>>(listStoreTypeDtos)).Returns(listStoreTypeViewModels);

            //act
            var actualResult = storeController.StoreTypeList() as ViewResult;

            //assert
            Assert.IsNotNull(actualResult);
        }

        [Test]
        public void StoreList_Get_When_GetAllStore_Count_Equal_0_Should_RedirectToError()
        {
            //arrange
            var listStoreDtos = new List<StoreDto>();
            storeService.Setup(x => x.GetAllStore(0)).Returns(listStoreDtos);

            //act
            var actualResult = storeController.StoreList(0) as RedirectToRouteResult;

            //assert
            Assert.That(actualResult.RouteValues["action"], Is.EqualTo("Error"));
            Assert.That(actualResult.RouteValues["Controller"], Is.EqualTo("Error"));
        }

        [Test]
        public void StoreList_Get_When_GetAllStore_Count_Not_Equal_0_Should_Return_View()
        {
            //arrange
            var listStoreDtos = new List<StoreDto>
            {
                new StoreDto
                {
                    Name = "test"
                }
            };

            var listStoreViewModels = new List<StoreViewModel>
            {
                new StoreViewModel
                {
                    Name = "test"
                }
            };

            mapper.Setup(x => x.Map<IList<StoreDto>, IList<StoreViewModel>>(listStoreDtos)).Returns(listStoreViewModels);
            storeService.Setup(x => x.GetAllStore(0)).Returns(listStoreDtos);

            //act
            var actualResult = storeController.StoreList(0) as ViewResult;

            //assert
            Assert.IsNotNull(actualResult);
        }
    }
}