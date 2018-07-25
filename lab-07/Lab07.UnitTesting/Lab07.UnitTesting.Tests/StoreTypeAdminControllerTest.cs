using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using AutoMapper;
using Lab07.UnitTesting.Areas.Admin.Controllers;
using Lab07.UnitTesting.BusinessLogic.Interfaces;
using Lab07.UnitTesting.Controllers;
using Lab07.UnitTesting.DTO;
using Lab07.UnitTesting.Models;
using Moq;
using System.Threading.Tasks;
using Lab07.UnitTesting.BusinessLogic.Infrastructure;

namespace Lab07.UnitTesting.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class StoreTypeAdminControllerTest
    {
        private StoreTypeAdminController storeTypeAdminController;
        private Mock<IStoreTypeService> storeTypeService;
        private Mock<IMapper> mapper;


        [SetUp]
        public void SetUp()
        {
            storeTypeService = new Mock<IStoreTypeService>();
            mapper = new Mock<IMapper>();

            storeTypeAdminController = new StoreTypeAdminController(storeTypeService.Object, mapper.Object);
        }


        private static List<StoreTypeDto> CreateListStoreTypeDto()
        {
            StoreTypeDto storeTypeDto1 = new StoreTypeDto
            {
                Id = 0,
                Name = "Test store"
            };

            StoreTypeDto storeTypeDto2 = new StoreTypeDto
            {
                Id = 0,
                Name = "Test store 2"
            };

            StoreTypeDto storeTypeDto3 = new StoreTypeDto
            {
                Id = 0,
                Name = "Test store 3"
            };

            return new List<StoreTypeDto> { storeTypeDto1, storeTypeDto2, storeTypeDto3 };
        }

        private static List<StoreTypeViewModel> CreateListStoreTypeViewModel()
        {
            StoreTypeViewModel storeTypeViewModel1 = new StoreTypeViewModel
            {
                Id = 0,
                Name = "Test store"
            };

            StoreTypeViewModel storeTypeViewModel2 = new StoreTypeViewModel
            {
                Id = 0,
                Name = "Test store 2"
            };

            StoreTypeViewModel storeTypeViewModel3 = new StoreTypeViewModel
            {
                Id = 0,
                Name = "Test store 3"
            };

            return new List<StoreTypeViewModel> { storeTypeViewModel1, storeTypeViewModel2, storeTypeViewModel3 };
        }


        [Test]
        public void StoreTypeAdminController_When_StoreTypeService_Null_Should_ThrowArgumentNullException()
        {
            //assert
            Assert.Throws<ArgumentNullException>(() => new StoreTypeAdminController(null, mapper.Object));
        }

        [Test]
        public void StoreTypeAdminController_When_Mapper_Null_Should_ThrowArgumentNullException()
        {
            //assert
            Assert.Throws<ArgumentNullException>(() => new StoreTypeAdminController(storeTypeService.Object, null));
        }

        [Test]
        public void StoreTypeList_When_StoreTypeList_Should_Return_View()
        {
            //arrange
            var listStoreTypeDto = CreateListStoreTypeDto();
            var listStoreTypeViewModel = CreateListStoreTypeViewModel();
            storeTypeService.Setup(x => x.GetAllStoreType()).Returns(listStoreTypeDto);
            mapper.Setup(x => x.Map<IList<StoreTypeDto>, IList<StoreTypeViewModel>>(listStoreTypeDto)).Returns(listStoreTypeViewModel);

            //act
            var actualResult = storeTypeAdminController.StoreTypeList() as ViewResult;

            //assert
            Assert.IsNotNull(actualResult);
            Assert.IsTrue(storeTypeAdminController.ModelState.IsValid);
            storeTypeService.Verify(x => x.GetAllStoreType(), Times.Once);
        }

        [Test]
        public void AddStoreType_Get_Should_Return_View()
        {
            //act
            var actualResult = storeTypeAdminController.AddStoreType() as ViewResult;

            //assert
            Assert.IsNotNull(actualResult);
        }

        [Test]
        public void AddStore_Post_When_Model_Is_Not_Valid_Should_Return_View()
        {
            //arrange
            StoreTypeViewModel storeTypeViewModel = new StoreTypeViewModel
            {
                Id = 0,
                Name = "test"
            };

            storeTypeAdminController.ModelState.AddModelError("key", "error message");

            //act
            var actualResult = storeTypeAdminController.AddStoreType(storeTypeViewModel);

            //assert
            Assert.IsNotNull(actualResult);
            Assert.IsFalse(storeTypeAdminController.ModelState.IsValid);
        }

        [Test]
        public async Task AddStore_Post_When_OperationDetails_Is_Not_Succedeed_Should_Return_View_With_Error()
        {
            //arrange
            StoreTypeViewModel storeTypeViewModel = new StoreTypeViewModel
            {
                Id = 0,
                Name = "test"
            };

            StoreTypeDto storetypeDto = new StoreTypeDto
            {
                Id = 0,
                Name = "test"
            };

            OperationDetails operationDetails = new OperationDetails(false);

            mapper.Setup(x => x.Map<StoreTypeViewModel, StoreTypeDto>(storeTypeViewModel)).Returns(storetypeDto);
            storeTypeService.Setup(x => x.AddStoreTypeAsync(storetypeDto)).ReturnsAsync(operationDetails);
            storeTypeService.Setup(x => x.GetAllStoreType()).Returns(new List<StoreTypeDto>());

            //act
            var actualResult = await storeTypeAdminController.AddStoreType(storeTypeViewModel);

            //assert
            Assert.IsNotNull(actualResult);
            Assert.IsFalse(storeTypeAdminController.ModelState.IsValid);
            storeTypeService.Verify(x => x.AddStoreTypeAsync(storetypeDto), Times.Once);
        }

        [Test]
        public async Task AddStore_Post_When_OperationDetails_Is_Succedeed_Should_Return_View_SuccessAdd()
        {
            //arrange
            StoreTypeViewModel storeTypeViewModel = new StoreTypeViewModel
            {
                Id = 0,
                Name = "test"
            };

            StoreTypeDto storetypeDto = new StoreTypeDto
            {
                Id = 0,
                Name = "test"
            };

            OperationDetails operationDetails = new OperationDetails(true);

            mapper.Setup(x => x.Map<StoreTypeViewModel, StoreTypeDto>(storeTypeViewModel)).Returns(storetypeDto);
            storeTypeService.Setup(x => x.AddStoreTypeAsync(storetypeDto)).ReturnsAsync(operationDetails);
            storeTypeService.Setup(x => x.GetAllStoreType()).Returns(new List<StoreTypeDto>());

            //act
            var actualResult = await storeTypeAdminController.AddStoreType(storeTypeViewModel) as ViewResult;

            //assert
            Assert.IsNotNull(actualResult);
            Assert.IsTrue(storeTypeAdminController.ModelState.IsValid);
            Assert.AreEqual(actualResult.ViewName, "SuccessAdd");
            storeTypeService.Verify(x => x.AddStoreTypeAsync(storetypeDto), Times.Once);
        }

        [Test]
        public async Task DeleteStoreType_When_OperationDetails_Is_Not_Succedeed_Should_RedirectToAction_StoreTypeList()
        {
            //arrange
            StoreTypeViewModel storeTypeViewModel = new StoreTypeViewModel
            {
                Id = 0,
                Name = "test"
            };

            StoreTypeDto storetypeDto = new StoreTypeDto
            {
                Id = 0,
                Name = "test"
            };

            OperationDetails operationDetails = new OperationDetails(false);
            mapper.Setup(x => x.Map<StoreTypeViewModel, StoreTypeDto>(storeTypeViewModel)).Returns(storetypeDto);
            storeTypeService.Setup(x => x.DeleteStoreTypeAsync(storetypeDto)).ReturnsAsync(operationDetails);

            //act
            var actualResult = await storeTypeAdminController.DeleteStoreType(storeTypeViewModel) as RedirectToRouteResult;

            //assert
            Assert.IsNotNull(actualResult);
            Assert.IsFalse(storeTypeAdminController.ModelState.IsValid);
            Assert.That(actualResult.RouteValues["action"], Is.EqualTo("StoreTypeList"));
            storeTypeService.Verify(x => x.DeleteStoreTypeAsync(storetypeDto), Times.Once);
        }


        [Test]
        public async Task DeleteStore_When_OperationDetails_Is_Succedeed_Should_RedirectToAction_StoreTypeList()
        {
            //arrange
            StoreTypeViewModel storeTypeViewModel = new StoreTypeViewModel
            {
                Id = 0,
                Name = "test"
            };

            StoreTypeDto storetypeDto = new StoreTypeDto
            {
                Id = 0,
                Name = "test"
            };

            OperationDetails operationDetails = new OperationDetails(true);
            mapper.Setup(x => x.Map<StoreTypeViewModel, StoreTypeDto>(storeTypeViewModel)).Returns(storetypeDto);
            storeTypeService.Setup(x => x.DeleteStoreTypeAsync(storetypeDto)).ReturnsAsync(operationDetails);

            //act
            var actualResult = await storeTypeAdminController.DeleteStoreType(storeTypeViewModel) as RedirectToRouteResult;

            //assert
            Assert.IsNotNull(actualResult);
            Assert.IsTrue(storeTypeAdminController.ModelState.IsValid);
            Assert.That(actualResult.RouteValues["action"], Is.EqualTo("StoreTypeList"));
            storeTypeService.Verify(x => x.DeleteStoreTypeAsync(storetypeDto), Times.Once);
        }

        [Test]
        public async Task UpdateStoreType_When_Get_Update_Should_Return_View()
        {
            //arrange
            StoreTypeViewModel storeTypeViewModel = new StoreTypeViewModel
            {
                Id = 0,
                Name = "test"
            };

            StoreTypeDto storetypeDto = new StoreTypeDto
            {
                Id = 0,
                Name = "test"
            };
            storeTypeService.Setup(x => x.GetStoreTypeByIdAsync(0)).ReturnsAsync(storetypeDto);
            mapper.Setup(x => x.Map<StoreTypeDto, StoreTypeViewModel>(storetypeDto)).Returns(storeTypeViewModel);

            //act
            var actualResult = await storeTypeAdminController.UpdateStoreType(0) as ViewResult;

            //assert
            Assert.IsNotNull(actualResult);
            Assert.IsTrue(storeTypeAdminController.ModelState.IsValid);
            storeTypeService.Verify(x => x.GetStoreTypeByIdAsync(0), Times.Once);
        }

        [Test]
        public async Task UpdateStoreTypePost_When_OperationDetails_Is_Not_Succedeed_Should_RedirectToAction_StoreTypeList()
        {
            //arrange
            StoreTypeViewModel storeTypeViewModel = new StoreTypeViewModel
            {
                Id = 0,
                Name = "test"
            };

            StoreTypeDto storetypeDto = new StoreTypeDto
            {
                Id = 0,
                Name = "test"
            };

            OperationDetails operationDetails = new OperationDetails(false);
            mapper.Setup(x => x.Map<StoreTypeViewModel, StoreTypeDto>(storeTypeViewModel)).Returns(storetypeDto);
            storeTypeService.Setup(x => x.UpdateStoreTypeAsync(storetypeDto)).ReturnsAsync(operationDetails);

            //act
            var actualResult = await storeTypeAdminController.UpdateStoreType(storeTypeViewModel) as RedirectToRouteResult;

            //assert
            Assert.IsNotNull(actualResult);
            Assert.IsFalse(storeTypeAdminController.ModelState.IsValid);
            Assert.That(actualResult.RouteValues["action"], Is.EqualTo("StoreTypeList"));
            storeTypeService.Verify(x => x.UpdateStoreTypeAsync(storetypeDto), Times.Once);
        }

        [Test]
        public async Task UpdateStoreTypePost_When_OperationDetails_Is_Succedeed_Should_RedirectToAction_StoreTypeList()
        {
            //arrange
            //arrange
            StoreTypeViewModel storeTypeViewModel = new StoreTypeViewModel
            {
                Id = 0,
                Name = "test"
            };

            StoreTypeDto storetypeDto = new StoreTypeDto
            {
                Id = 0,
                Name = "test"
            };

            OperationDetails operationDetails = new OperationDetails(true);
            mapper.Setup(x => x.Map<StoreTypeViewModel, StoreTypeDto>(storeTypeViewModel)).Returns(storetypeDto);
            storeTypeService.Setup(x => x.UpdateStoreTypeAsync(storetypeDto)).ReturnsAsync(operationDetails);

            //act
            var actualResult = await storeTypeAdminController.UpdateStoreType(storeTypeViewModel) as RedirectToRouteResult;

            //assert
            Assert.IsNotNull(actualResult);
            Assert.IsTrue(storeTypeAdminController.ModelState.IsValid);
            Assert.That(actualResult.RouteValues["action"], Is.EqualTo("StoreTypeList"));
            storeTypeService.Verify(x => x.UpdateStoreTypeAsync(storetypeDto), Times.Once);
        }
    }
}