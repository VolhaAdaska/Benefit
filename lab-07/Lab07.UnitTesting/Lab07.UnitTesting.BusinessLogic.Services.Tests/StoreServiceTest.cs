using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using FluentAssertions;
using Lab07.UnitTesting.BusinessLogic.Infrastructure;
using Lab07.UnitTesting.DAL.Interfaces;
using Lab07.UnitTesting.DAL.Models;
using Lab07.UnitTesting.DTO;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using Lab07.UnitTesting.DAL.Interfaces.Repositories;

namespace Lab07.UnitTesting.BusinessLogic.Services.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class StoreServiceTest
    {
        private StoreService storeService;
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IMapper> mapper;
        private Mock<IStoreRepository> storeRepository;

        [SetUp]
        public void SetUp()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            mapper = new Mock<IMapper>();
            storeRepository = new Mock<IStoreRepository>();
            unitOfWork.Setup(x => x.StoreRepository).Returns(storeRepository.Object);

            storeService = new StoreService(unitOfWork.Object, mapper.Object);
        }

        private static List<StoreDto> CreateListStoreDto()
        {
            StoreDto storeDto1 = new StoreDto
            {
                Id = 0,
                Name = "Test store",
                Promocode = "Test Promocode",
                StoreTypeId = 1
            };

            StoreDto storeDto2 = new StoreDto
            {
                Id = 0,
                Name = "Test store",
                Promocode = "Test Promocode",
                StoreTypeId = 1
            };

            StoreDto storeDto3 = new StoreDto
            {
                Id = 0,
                Name = "Test store",
                Promocode = "Test Promocode",
                StoreTypeId = 1
            };

            return new List<StoreDto> { storeDto1, storeDto2, storeDto3 };
        }

        private static List<Store> CreateListStore()
        {
            Store store1 = new Store
            {
                Id = 0,
                Name = "Test store",
                Promocode = "Test Promocode",
                StoreTypeId = 1
            };

            Store store2 = new Store
            {
                Id = 0,
                Name = "Test store",
                Promocode = "Test Promocode",
                StoreTypeId = 1
            };

            Store store3 = new Store
            {
                Id = 0,
                Name = "Test store",
                Promocode = "Test Promocode",
                StoreTypeId = 1
            };

            return new List<Store> { store1, store2, store3 };
        }

        [Test]
        public void StoreService_When_UnitOfWork_Null_Should_ThrowArgumentNullException()
        {
            //assert
            Assert.Throws<ArgumentNullException>(() => new StoreService(null,  mapper.Object));
        }

        [Test]
        public void StoreService_When_Mapper_Null_Should_ThrowArgumentNullException()
        {
            //assert
            Assert.Throws<ArgumentNullException>(() => new StoreService(unitOfWork.Object, null));
        }

        [Test]
        public void BaseService_Dispose()
        {
            //act
            storeService.Dispose();

            //assert
            unitOfWork.Verify(x => x.Dispose(), Times.Once);
        }

        [Test]
        public async Task AddStoreAsync_When_Parameter_Null_Should_Return_Something_Went_Wrong()
        {
            //arrange
            var expectedResult = new OperationDetails(false, "Something went wrong", "Store");

            //act
            var actualResult = await storeService.AddStoreAsync(null);

            //assert
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task AddStoreAsync_When_Duplication_Name_Store_Should_Return_Store_Already_Exists()
        {
            //arrange
            var expectedResult = new OperationDetails(false, "A Store with this name already exists", "StoreType");

            StoreDto storeDto = new StoreDto
            {
                Id = 0,
                Name = "Test store",
                Promocode = "Test Promocode",
                StoreTypeId = 1
            };

            Store store = new Store
            {
                Id = 0,
                Name = "Test store",
                Promocode = "Test Promocode",
                StoreTypeId = 1
            };

            mapper.Setup(x => x.Map<StoreDto, Store>(storeDto)).Returns(store);
            storeRepository.Setup(x => x.GetByName(store.Name)).Returns(store);

            //act
            var actualResult = await storeService.AddStoreAsync(storeDto);

            //assert
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task AddStoreAsync_When_Correct_Add_Store_Should_Return_True()
        {
            //arrange
            var expectedResult = new OperationDetails(true);

            StoreDto storeDto = new StoreDto
            {
                Id = 0,
                Name = "Test store",
                Promocode = "Test Promocode",
                StoreTypeId = 1
            };

            Store store = new Store
            {
                Id = 0,
                Name = "Test store",
                Promocode = "Test Promocode",
                StoreTypeId = 1
            };

            mapper.Setup(x => x.Map<StoreDto, Store>(storeDto)).Returns(store);

            //act
            var actualResult = await storeService.AddStoreAsync(storeDto);

            //assert
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task AddStoreAsync_When_Add_Store_Should_Return_Exception()
        {
            //arrange
            var expectedResult = new OperationDetails(false, "Unfortunately, something went wrong....", "Store");

            StoreDto storeDto = new StoreDto
            {
                Id = 0,
                Name = "Test store",
                Promocode = "Test Promocode",
                StoreTypeId = 1
            };

            Store store = new Store
            {
                Id = 0,
                Name = "Test store",
                Promocode = "Test Promocode",
                StoreTypeId = 1
            };

            mapper.Setup(x => x.Map<StoreDto, Store>(storeDto)).Returns(store);
            storeRepository.Setup(x => x.CreateAsync(store)).Throws(new Exception("Exception message"));

            //act
            var actualResult = await storeService.AddStoreAsync(storeDto);

            //assert
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task DeleteStoreAsync_When_Parameter_Null_Should_Return_Something_Went_Wrong()
        {
            //arrange
            var expectedResult = new OperationDetails(false, "Something went wrong", "Store");

            //act
            var actualResult = await storeService.DeleteStoreAsync(null);

            //assert
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task DeleteStoreAsync_When_Store_Null_Should_Return_Store_Not_Found()
        {
            //arrange
            var expectedResult = new OperationDetails(false, "Store not found", "Store");

            StoreDto storeDto = new StoreDto
            {
                Id = 0,
                Name = "Test store",
                Promocode = "Test Promocode",
                StoreTypeId = 1
            };

            Store store = new Store
            {
                Id = 0,
                Name = "Test store",
                Promocode = "Test Promocode",
                StoreTypeId = 1
            };

            mapper.Setup(x => x.Map<StoreDto, Store>(storeDto)).Returns(store);

            //act
            var actualResult = await storeService.DeleteStoreAsync(storeDto);

            //assert
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task DeleteStoreAsync_When_Correct_Delete_Should_Return_True()
        {
            //arrange
            var expectedResult = new OperationDetails(true);

            StoreDto storeDto = new StoreDto
            {
                Id = 0,
                Name = "Test store",
                Promocode = "Test Promocode",
                StoreTypeId = 1
            };

            Store store = new Store
            {
                Id = 0,
                Name = "Test store",
                Promocode = "Test Promocode",
                StoreTypeId = 1
            };

            mapper.Setup(x => x.Map<StoreDto, Store>(storeDto)).Returns(store);
            storeRepository.Setup(x => x.GetByIdAsync(0)).ReturnsAsync(store);
            
            //act
            await storeService.DeleteStoreAsync(storeDto);

            //assert
            storeRepository.Verify(x => x.DeleteAsync(store), Times.Once);
        }

        [Test]
        public async Task DeleteStoreAsync_When_Delete_Store_Should_Return_Exception()
        {
            //arrange
            var expectedResult = new OperationDetails(false, "Exception message");

            StoreDto storeDto = new StoreDto
            {
                Id = 0,
                Name = "Test store",
                Promocode = "Test Promocode",
                StoreTypeId = 1
            };

            Store store = new Store
            {
                Id = 0,
                Name = "Test store",
                Promocode = "Test Promocode",
                StoreTypeId = 1
            };

            mapper.Setup(x => x.Map<StoreDto, Store>(storeDto)).Returns(store);
            storeRepository.Setup(x => x.GetByIdAsync(0)).ReturnsAsync(store);
            storeRepository.Setup(x => x.DeleteAsync(store)).Throws(new Exception("Exception message"));

            //act
            var actualResult = await storeService.DeleteStoreAsync(storeDto);

            //assert
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task UpdateStoreAsync_When_Parameter_Null_Should_Return_Something_Went_Wrong()
        {
            //arrange
            var expectedResult = new OperationDetails(false, "Something went wrong", "Store");

            //act
            var actualResult = await storeService.UpdateStoreAsync(null);

            //assert
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task UpdateStoreAsync_When_Correct_Update_Should_Return_True()
        {
            //arrange
            StoreDto storeDto = new StoreDto
            {
                Id = 0,
                Name = "Test store",
                Promocode = "Test Promocode",
                StoreTypeId = 1
            };

            Store store = new Store
            {
                Id = 0,
                Name = "Test store",
                Promocode = "Test Promocode",
                StoreTypeId = 1
            };

            mapper.Setup(x => x.Map<StoreDto, Store>(storeDto)).Returns(store);

            //act
            await storeService.UpdateStoreAsync(storeDto);

            //assert
            storeRepository.Verify(x => x.UpdateAsync(store), Times.Once);
        }

        [Test]
        public async Task UpdateStoreAsync_When_Update_Store_Should_Return_Exception()
        {
            //arrange
            var expectedResult = new OperationDetails(false, "Exception message");

            StoreDto storeDto = new StoreDto
            {
                Id = 0,
                Name = "Test store",
                Promocode = "Test Promocode",
                StoreTypeId = 1
            };

            Store store = new Store
            {
                Id = 0,
                Name = "Test store",
                Promocode = "Test Promocode",
                StoreTypeId = 1
            };

            mapper.Setup(x => x.Map<StoreDto, Store>(storeDto)).Returns(store);
            storeRepository.Setup(x => x.UpdateAsync(store)).Throws(new Exception("Exception message"));

            //act
            var actualResult = await storeService.UpdateStoreAsync(storeDto);

            //assert
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void GetAllStore_When_Correct_Should_Return_List_Store()
        {
            //arrange
            var listStoreDto = CreateListStoreDto();
            var listStore = CreateListStore();

            storeRepository.Setup(x => x.GetAllByStoreTypeId(1)).Returns(listStore);
            mapper.Setup(x => x.Map<IList<Store>, IList<StoreDto>>(listStore)).Returns(listStoreDto);

            //act
            var actualResult = storeService.GetAllStore(1);

            //assert
            actualResult.Should().BeEquivalentTo(listStoreDto);
            storeRepository.Verify(x => x.GetAllByStoreTypeId(1), Times.Once);
        }

        [Test]
        public async Task GetStoreByIdAsync_When_Correct_Should_return_StoreDto()
        {
            //arrange
            StoreDto storeDto = new StoreDto
            {
                Id = 0,
                Name = "Test store",
                Promocode = "Test Promocode",
                StoreTypeId = 1
            };

            Store store = new Store
            {
                Id = 0,
                Name = "Test store",
                Promocode = "Test Promocode",
                StoreTypeId = 1
            };

            storeRepository.Setup(x => x.GetByIdAsync(0)).ReturnsAsync(store);
            mapper.Setup(x => x.Map<Store, StoreDto>(store)).Returns(storeDto);

            //act
            var actualResult = await storeService.GetStoreByIdAsync(storeDto.Id);

            //assert
            actualResult.Should().BeEquivalentTo(storeDto);
            storeRepository.Verify(x => x.GetByIdAsync(storeDto.Id), Times.Once);
        }
    }
}