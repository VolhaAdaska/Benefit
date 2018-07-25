using AutoMapper;
using Lab07.UnitTesting.Areas.Admin.Controllers;
using Lab07.UnitTesting.BusinessLogic.Infrastructure;
using Lab07.UnitTesting.BusinessLogic.Interfaces;
using Lab07.UnitTesting.DTO;
using Lab07.UnitTesting.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Lab07.UnitTesting.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class StoreAdminControllerTest
    {
        private StoreAdminController storeAdminController;
        private Mock<IStoreService> storeService;
        private Mock<IStoreTypeService> storeTypeService;
        private Mock<IMapper> mapper;

        [SetUp]
        public void SetUp()
        {
            storeService = new Mock<IStoreService>();
            storeTypeService = new Mock<IStoreTypeService>();
            mapper = new Mock<IMapper>();

            storeAdminController = new StoreAdminController(storeService.Object, storeTypeService.Object, mapper.Object);
        }

        [Test]
        public void StoreAdminController_When_StoreService_Null_Should_ThrowArgumentNullException()
        {
            //assert
            Assert.Throws<ArgumentNullException>(() => new StoreAdminController(null, storeTypeService.Object, mapper.Object));
        }

        [Test]
        public void StoreAdminController_When_StoreTypeService_Null_Should_ThrowArgumentNullException()
        {
            //assert
            Assert.Throws<ArgumentNullException>(() => new StoreAdminController(storeService.Object, null, mapper.Object));
        }

        [Test]
        public void StoreAdminController_When_Mapper_Null_Should_ThrowArgumentNullException()
        {
            //assert
            Assert.Throws<ArgumentNullException>(() => new StoreAdminController(storeService.Object, storeTypeService.Object, null));
        }

        [Test]
        public void StoreList_Get_When_List_Store_Count_0_Should_Return_View_Error()
        {
            //arrange
            var listStoreDtos = new List<StoreDto>();
            storeService.Setup(x => x.GetAllStore(0)).Returns(listStoreDtos);

            //act
            var actualResult = storeAdminController.StoreList(0) as ViewResult;

            //assert
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(actualResult.ViewName, "Error");
        }

        [Test]
        public void StoreList_Get_When_List_Store_Count_Not_0_Should_Return_View()
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
            storeService.Setup(x => x.GetAllStore(0)).Returns(listStoreDtos);
            mapper.Setup(x => x.Map<IList<StoreDto>, IList<StoreViewModel>>(listStoreDtos)).Returns(listStoreViewModels);

            //act
            var actualResult = storeAdminController.StoreList(0) as ViewResult;

            //assert
            Assert.IsNotNull(actualResult);
            Assert.IsTrue(storeAdminController.ModelState.IsValid);
            storeService.Verify(x => x.GetAllStore(0), Times.Once);
        }

        [Test]
        public void AddStore_When_Get_AddStore_Should_Return_View()
        {
            List<StoreTypeDto> storeTypeDtos = new List<StoreTypeDto>
            {
                new StoreTypeDto
                {
                    Id = 0,
                    Name = "test"
                }
            };

            //arrange
            storeTypeService.Setup(x => x.GetAllStoreType()).Returns(storeTypeDtos);
            
            //act
            var actualResult = storeAdminController.AddStore() as ViewResult;

            //assert
            Assert.IsNotNull(actualResult);
            Assert.IsTrue(storeAdminController.ModelState.IsValid);
        }

        [Test]
        public void AddStore_Post_When_Model_Is_Not_Valid_Should_Return_View()
        {
            //arrange
            StoreViewModel storeViewModel = new StoreViewModel
            {
                Id = 0,
                Name = "test",
                Promocode = "test promocode",
                StoreTypeId = 0,
                StoreTypeList = new List<SelectListItem>()
            };

            storeAdminController.ModelState.AddModelError("key", "error message");

            //act
            var actualResult = storeAdminController.AddStore(storeViewModel);

            //assert
            Assert.IsNotNull(actualResult);
            Assert.IsFalse(storeAdminController.ModelState.IsValid);
        }

        [Test]
        public async Task AddStore_Post_When_OperationDetails_Is_Not_Succedeed_Should_Return_View_With_Error()
        {
            //arrange
            StoreViewModel storeViewModel = new StoreViewModel
            {
                Id = 0,
                Name = "test",
                Promocode = "test promocode",
                StoreTypeId = 0,
                StoreTypeList = null
            };

            StoreDto storeDto = new StoreDto
            {
                Id = 0,
                Name = "test",
                Promocode = "test promocode",
                StoreTypeId = 0
            };

            OperationDetails operationDetails = new OperationDetails(false);

            mapper.Setup(x => x.Map<StoreViewModel, StoreDto>(storeViewModel)).Returns(storeDto);
            storeService.Setup(x => x.AddStoreAsync(storeDto)).ReturnsAsync(operationDetails);
            storeTypeService.Setup(x => x.GetAllStoreType()).Returns(new List<StoreTypeDto>());

            //act
            var actualResult = await storeAdminController.AddStore(storeViewModel);

            //assert
            Assert.IsNotNull(actualResult);
            Assert.IsFalse(storeAdminController.ModelState.IsValid);
            storeService.Verify(x => x.AddStoreAsync(storeDto), Times.Once);
        }

        [Test]
        public async Task AddStore_Post_When_StoreTypeList_Not_Null_Should_Return_View_With_Error()
        {
            //arrange

            StoreViewModel storeViewModel = new StoreViewModel
            {
                Id = 0,
                Name = "test",
                Promocode = "test promocode",
                StoreTypeId = 0,
                StoreTypeList = new List<SelectListItem>()
            };

            StoreDto storeDto = new StoreDto
            {
                Id = 0,
                Name = "test",
                Promocode = "test promocode",
                StoreTypeId = 0
            };

            OperationDetails operationDetails = new OperationDetails(false);

            mapper.Setup(x => x.Map<StoreViewModel, StoreDto>(storeViewModel)).Returns(storeDto);
            storeService.Setup(x => x.AddStoreAsync(storeDto)).ReturnsAsync(operationDetails);
            storeTypeService.Setup(x => x.GetAllStoreType()).Returns(new List<StoreTypeDto>());

            //act
            var actualResult = await storeAdminController.AddStore(storeViewModel);

            //assert
            Assert.IsNotNull(actualResult);
            Assert.IsFalse(storeAdminController.ModelState.IsValid);
            storeService.Verify(x => x.AddStoreAsync(storeDto), Times.Once);
        }

        [Test]
        public async Task AddStore_Post_When_OperationDetails_Is_Succedeed_Should_Return_View_SuccessAdd()
        {
            //arrange
            StoreViewModel storeViewModel = new StoreViewModel
            {
                Id = 0,
                Name = "test",
                Promocode = "test promocode",
                StoreTypeId = 0,
                StoreTypeList = null
            };

            StoreDto storeDto = new StoreDto
            {
                Id = 0,
                Name = "test",
                Promocode = "test promocode",
                StoreTypeId = 0
            };

            OperationDetails operationDetails = new OperationDetails(true);

            mapper.Setup(x => x.Map<StoreViewModel, StoreDto>(storeViewModel)).Returns(storeDto);
            storeService.Setup(x => x.AddStoreAsync(storeDto)).ReturnsAsync(operationDetails);
            storeTypeService.Setup(x => x.GetAllStoreType()).Returns(new List<StoreTypeDto>());

            //act
            var actualResult = await storeAdminController.AddStore(storeViewModel) as ViewResult;

            //assert
            Assert.IsNotNull(actualResult);
            Assert.IsTrue(storeAdminController.ModelState.IsValid);
            Assert.AreEqual(actualResult.ViewName, "SuccessAdd");
            storeService.Verify(x => x.AddStoreAsync(storeDto), Times.Once);
        }

        [Test]
        public async Task DeleteStore_When_OperationDetails_Is_Not_Succedeed_Should_RedirectToAction_StoreTypeList()
        {
            //arrange
            StoreViewModel storeViewModel = new StoreViewModel
            {
                Id = 0,
                Name = "test",
                Promocode = "test promocode",
                StoreTypeId = 0,
                StoreTypeList = null
            };

            StoreDto storeDto = new StoreDto
            {
                Id = 0,
                Name = "test",
                Promocode = "test promocode",
                StoreTypeId = 0
            };

            OperationDetails operationDetails = new OperationDetails(false);
            mapper.Setup(x => x.Map<StoreViewModel, StoreDto>(storeViewModel)).Returns(storeDto);
            storeService.Setup(x => x.DeleteStoreAsync(storeDto)).ReturnsAsync(operationDetails);

            //act
            var actualResult = await storeAdminController.DeleteStore(storeViewModel) as RedirectToRouteResult;

            //assert
            Assert.IsNotNull(actualResult);
            Assert.IsFalse(storeAdminController.ModelState.IsValid);
            Assert.That(actualResult.RouteValues["action"], Is.EqualTo("StoreTypeList"));
            Assert.That(actualResult.RouteValues["Controller"], Is.EqualTo("StoreTypeAdmin"));
            storeService.Verify(x => x.DeleteStoreAsync(storeDto), Times.Once);
        }

        [Test]
        public async Task DeleteStore_When_OperationDetails_Is_Succedeed_Should_RedirectToAction_StoreTypeList()
        {
            //arrange
            StoreViewModel storeViewModel = new StoreViewModel
            {
                Id = 0,
                Name = "test",
                Promocode = "test promocode",
                StoreTypeId = 0,
                StoreTypeList = null
            };

            StoreDto storeDto = new StoreDto
            {
                Id = 0,
                Name = "test",
                Promocode = "test promocode",
                StoreTypeId = 0
            };

            OperationDetails operationDetails = new OperationDetails(true);
            mapper.Setup(x => x.Map<StoreViewModel, StoreDto>(storeViewModel)).Returns(storeDto);
            storeService.Setup(x => x.DeleteStoreAsync(storeDto)).ReturnsAsync(operationDetails);

            //act
            var actualResult = await storeAdminController.DeleteStore(storeViewModel) as RedirectToRouteResult;

            //assert
            Assert.IsNotNull(actualResult);
            Assert.IsTrue(storeAdminController.ModelState.IsValid);
            Assert.That(actualResult.RouteValues["action"], Is.EqualTo("StoreTypeList"));
            Assert.That(actualResult.RouteValues["Controller"], Is.EqualTo("StoreTypeAdmin"));
            storeService.Verify(x => x.DeleteStoreAsync(storeDto), Times.Once);
        }

        [Test]
        public async Task UpdateStore_When_Get_Update_Should_Return_View()
        {
            //arrange
            StoreViewModel storeViewModel = new StoreViewModel
            {
                Id = 0,
                Name = "test",
                Promocode = "test promocode",
                StoreTypeId = 0,
                StoreTypeList = null
            };

            StoreDto storeDto = new StoreDto
            {
                Id = 0,
                Name = "test",
                Promocode = "test promocode",
                StoreTypeId = 0
            };
            storeService.Setup(x => x.GetStoreByIdAsync(0)).ReturnsAsync(storeDto);
            mapper.Setup(x => x.Map<StoreDto, StoreViewModel>(storeDto)).Returns(storeViewModel);

            //act
            var actualResult = await storeAdminController.UpdateStore(0) as ViewResult;

            //assert
            Assert.IsNotNull(actualResult);
            Assert.IsTrue(storeAdminController.ModelState.IsValid);
            storeService.Verify(x => x.GetStoreByIdAsync(0), Times.Once);
        }

        [Test]
        public async Task UpdateStorePost_When_OperationDetails_Is_Not_Succedeed_Should_RedirectToAction_StoreTypeList()
        {
            //arrange
            StoreViewModel storeViewModel = new StoreViewModel
            {
                Id = 0,
                Name = "test",
                Promocode = "test promocode",
                StoreTypeId = 0,
                StoreTypeList = null
            };

            StoreDto storeDto = new StoreDto
            {
                Id = 0,
                Name = "test",
                Promocode = "test promocode",
                StoreTypeId = 0
            };

            OperationDetails operationDetails = new OperationDetails(false);
            mapper.Setup(x => x.Map<StoreViewModel, StoreDto>(storeViewModel)).Returns(storeDto);
            storeService.Setup(x => x.UpdateStoreAsync(storeDto)).ReturnsAsync(operationDetails);

            //act
            var actualResult = await storeAdminController.UpdateStore(storeViewModel) as RedirectToRouteResult;

            //assert
            Assert.IsNotNull(actualResult);
            Assert.IsFalse(storeAdminController.ModelState.IsValid);
            Assert.That(actualResult.RouteValues["action"], Is.EqualTo("StoreTypeList"));
            Assert.That(actualResult.RouteValues["Controller"], Is.EqualTo("StoreTypeAdmin"));
            storeService.Verify(x => x.UpdateStoreAsync(storeDto), Times.Once);
        }

        [Test]
        public async Task UpdateStorePost_When_OperationDetails_Is_Succedeed_Should_RedirectToAction_StoreTypeList()
        {
            //arrange
            StoreViewModel storeViewModel = new StoreViewModel
            {
                Id = 0,
                Name = "test",
                Promocode = "test promocode",
                StoreTypeId = 0,
                StoreTypeList = null
            };

            StoreDto storeDto = new StoreDto
            {
                Id = 0,
                Name = "test",
                Promocode = "test promocode",
                StoreTypeId = 0
            };

            OperationDetails operationDetails = new OperationDetails(true);
            mapper.Setup(x => x.Map<StoreViewModel, StoreDto>(storeViewModel)).Returns(storeDto);
            storeService.Setup(x => x.UpdateStoreAsync(storeDto)).ReturnsAsync(operationDetails);

            //act
            var actualResult = await storeAdminController.UpdateStore(storeViewModel) as RedirectToRouteResult;

            //assert
            Assert.IsNotNull(actualResult);
            Assert.IsTrue(storeAdminController.ModelState.IsValid);
            Assert.That(actualResult.RouteValues["action"], Is.EqualTo("StoreTypeList"));
            Assert.That(actualResult.RouteValues["Controller"], Is.EqualTo("StoreTypeAdmin"));
            storeService.Verify(x => x.UpdateStoreAsync(storeDto), Times.Once);
        }
    }
}

