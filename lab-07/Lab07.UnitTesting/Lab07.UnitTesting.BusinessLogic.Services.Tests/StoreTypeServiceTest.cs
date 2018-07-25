using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Lab07.UnitTesting.BusinessLogic.Infrastructure;
using Lab07.UnitTesting.DAL.Interfaces;
using Lab07.UnitTesting.DAL.Interfaces.Repositories;
using Lab07.UnitTesting.DAL.Models;
using Lab07.UnitTesting.DTO;
using Moq;
using NUnit.Framework;

namespace Lab07.UnitTesting.BusinessLogic.Services.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class StoreTypeServiceTest
    {
        private StoreTypeService storeTypeService;
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IMapper> mapper;
        private Mock<IStoreTypeRepository> storeTypeRepository;

        [SetUp]
        public void SetUp()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            mapper = new Mock<IMapper>();
            storeTypeRepository = new Mock<IStoreTypeRepository>();
            unitOfWork.Setup(x => x.StoreTypeRepository).Returns(storeTypeRepository.Object);

            storeTypeService = new StoreTypeService(unitOfWork.Object, mapper.Object);
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

        private static List<StoreType> CreateListStoreType()
        {
            StoreType storeType1 = new StoreType
            {
                Id = 0,
                Name = "Test store"
            };

            StoreType storeType2 = new StoreType
            {
                Id = 0,
                Name = "Test store 2"
            };

            StoreType storeType3 = new StoreType
            {
                Id = 0,
                Name = "Test store 3"
            };

            return new List<StoreType> { storeType1, storeType2, storeType3 };
        }

        [Test]
        public void StoreTypeService_When_UnitOfWork_Null_Should_ThrowArgumentNullException()
        {
            //assert
            Assert.Throws<ArgumentNullException>(() => new StoreTypeService(null, mapper.Object));
        }

        [Test]
        public void StoreTypeService_When_Mapper_Null_Should_ThrowArgumentNullException()
        {
            //assert
            Assert.Throws<ArgumentNullException>(() => new StoreTypeService(unitOfWork.Object, null));
        }

        [Test]
        public void BaseService_Dispose()
        {
            //act
            storeTypeService.Dispose();

            //assert
            unitOfWork.Verify(x => x.Dispose(), Times.Once);
        }

        [Test]
        public async Task AddStoreTypeAsync_When_Parameter_Null_Should_Return_Something_Went_Wrong()
        {
            //arrange
            var expectedResult = new OperationDetails(false, "Something went wrong", "StoreType");

            //act
            var actualResult = await storeTypeService.AddStoreTypeAsync(null);

            //assert
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task AddStoreTypeAsync_When_Duplication_Name_Store_Should_Return_StoreType_Already_Exists()
        {
            //arrange
            var expectedResult = new OperationDetails(false, "A Store type with this name already exists", "StoreType");

            StoreTypeDto storeTypeDto = new StoreTypeDto
            {
                Id = 0,
                Name = "Test store"
            };

            StoreType storeType = new StoreType
            {
                Id = 0,
                Name = "Test store"
            };

            mapper.Setup(x => x.Map<StoreTypeDto, StoreType>(storeTypeDto)).Returns(storeType);
            storeTypeRepository.Setup(x => x.GetByName(storeType.Name)).Returns(storeType);

            //act
            var actualResult = await storeTypeService.AddStoreTypeAsync(storeTypeDto);

            //assert
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task AddStoreTypeAsync_When_Correct_Add_StoreType_Should_Return_True()
        {
            //arrange
            var expectedResult = new OperationDetails(true);
            StoreTypeDto storeTypeDto = new StoreTypeDto
            {
                Id = 0,
                Name = "Test store"
            };

            StoreType storeType = new StoreType
            {
                Id = 0,
                Name = "Test store"
            };

            mapper.Setup(x => x.Map<StoreTypeDto, StoreType>(storeTypeDto)).Returns(storeType);

            //act
            var actualResult = await storeTypeService.AddStoreTypeAsync(storeTypeDto);

            //assert
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task AddStoreTypeAsync_When_Add_StoreType_Should_Return_Exception()
        {
            //arrange
            var expectedResult = new OperationDetails(false, "Unfortunately, something went wrong....", "StoreType");

            StoreTypeDto storeTypeDto = new StoreTypeDto
            {
                Id = 0,
                Name = "Test store"
            };

            StoreType storeType = new StoreType
            {
                Id = 0,
                Name = "Test store"
            };

            mapper.Setup(x => x.Map<StoreTypeDto, StoreType>(storeTypeDto)).Returns(storeType);
            storeTypeRepository.Setup(x => x.CreateAsync(storeType)).Throws(new Exception("Exception message"));

            //act
            var actualResult = await storeTypeService.AddStoreTypeAsync(storeTypeDto);

            //assert
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task DeleteStoreTypeAsync_When_Parameter_Null_Should_Return_Something_Went_Wrong()
        {
            //arrange
            var expectedResult = new OperationDetails(false, "Something went wrong", "StoreType");

            //act
            var actualResult = await storeTypeService.DeleteStoreTypeAsync(null);

            //assert
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task DeleteStoreTypeAsync_When_StoreType_Null_Should_Return_StoreType_Not_Found()
        {
            //arrange
            var expectedResult = new OperationDetails(false, "Store type not found", "StoreType");

            StoreTypeDto storeTypeDto = new StoreTypeDto
            {
                Id = 0,
                Name = "Test store"
            };

            StoreType storeType = new StoreType
            {
                Id = 0,
                Name = "Test store"
            };

            mapper.Setup(x => x.Map<StoreTypeDto, StoreType>(storeTypeDto)).Returns(storeType);

            //act
            var actualResult = await storeTypeService.DeleteStoreTypeAsync(storeTypeDto);

            //arrange
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Test]

        public async Task DeleteStoreTypeAsync_When_Correct_Delete_Should_Return_True()
        {
            //arrange
            StoreTypeDto storeTypeDto = new StoreTypeDto
            {
                Id = 0,
                Name = "Test store"
            };

            StoreType storeType = new StoreType
            {
                Id = 0,
                Name = "Test store"
            };

            mapper.Setup(x => x.Map<StoreTypeDto, StoreType>(storeTypeDto)).Returns(storeType);
            storeTypeRepository.Setup(x => x.GetByIdAsync(0)).ReturnsAsync(storeType);

            //act
            await storeTypeService.DeleteStoreTypeAsync(storeTypeDto);

            //assert
            storeTypeRepository.Verify(x => x.DeleteAsync(storeType), Times.Once);
        }

        [Test]
        public async Task DeleteStoreTypeAsync_When_Delete_StoreType_Should_Return_Exception()
        {
            //arrange
            var expectedResult = new OperationDetails(false, "Exception message");
            StoreTypeDto storeTypeDto = new StoreTypeDto
            {
                Id = 0,
                Name = "Test store"
            };

            StoreType storeType = new StoreType
            {
                Id = 0,
                Name = "Test store"
            };

            mapper.Setup(x => x.Map<StoreTypeDto, StoreType>(storeTypeDto)).Returns(storeType);
            storeTypeRepository.Setup(x => x.GetByIdAsync(0)).ReturnsAsync(storeType);
            storeTypeRepository.Setup(x => x.DeleteAsync(storeType)).Throws(new Exception("Exception message"));

            //act
            var actualResult = await storeTypeService.DeleteStoreTypeAsync(storeTypeDto);

            //assert
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task UpdateStoreTypeAsync_When_Parameter_Null_Should_Return_Something_Went_Wrong()
        {
            //arrange
            var expectedResult = new OperationDetails(false, "Something went wrong", "StoreType");

            //act
            var actualResult = await storeTypeService.UpdateStoreTypeAsync(null);

            //assert
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task UpdateStoreTypeAsync_When_Correct_Update_Should_Return_True()
        {
            //arrange
            StoreTypeDto storeTypeDto = new StoreTypeDto
            {
                Id = 0,
                Name = "Test store"
            };

            StoreType storeType = new StoreType
            {
                Id = 0,
                Name = "Test store"
            };

            mapper.Setup(x => x.Map<StoreTypeDto, StoreType>(storeTypeDto)).Returns(storeType);

            //act
            await storeTypeService.UpdateStoreTypeAsync(storeTypeDto);

            //assert
            storeTypeRepository.Verify(x => x.UpdateAsync(storeType), Times.Once);
        }

        [Test]
        public async Task UpdateStoreTypeAsync_When_Update_StoreType_Should_Return_Exception()
        {
            //arrange
            var expectedResult = new OperationDetails(false, "Exception message");

            StoreTypeDto storeTypeDto = new StoreTypeDto
            {
                Id = 0,
                Name = "Test store"
            };

            StoreType storeType = new StoreType
            {
                Id = 0,
                Name = "Test store"
            };

            mapper.Setup(x => x.Map<StoreTypeDto, StoreType>(storeTypeDto)).Returns(storeType);
            storeTypeRepository.Setup(x => x.UpdateAsync(storeType)).Throws(new Exception("Exception message"));

            //act
            var actualResult = await storeTypeService.UpdateStoreTypeAsync(storeTypeDto);

            //assert
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void GetAllStore_When_Correct_Should_Return_List_Store()
        {
            //arrange
            var listStoreTypeDto = CreateListStoreTypeDto();
            var listStoreType = CreateListStoreType();
            storeTypeRepository.Setup(x => x.GetAllStoreType()).Returns(listStoreType);
            mapper.Setup(x => x.Map<IList<StoreType>, IList<StoreTypeDto>>(listStoreType)).Returns(listStoreTypeDto);

            //act
            var actualResult = storeTypeService.GetAllStoreType();

            //assert
            actualResult.Should().BeEquivalentTo(listStoreTypeDto);
        }

        [Test]
        public async Task GetStoreTypeByIdAsync_When_Correct_Should_return_StoreDto()
        {
            //arrange
            StoreTypeDto storeTypeDto = new StoreTypeDto
            {
                Id = 0,
                Name = "Test store"
            };

            StoreType storeType = new StoreType
            {
                Id = 0,
                Name = "Test store"
            };

            storeTypeRepository.Setup(x => x.GetByIdAsync(0)).ReturnsAsync(storeType);
            mapper.Setup(x => x.Map<StoreType, StoreTypeDto>(storeType)).Returns(storeTypeDto);

            //act
            var actualResult = await storeTypeService.GetStoreTypeByIdAsync(storeTypeDto.Id);

            //assert
            actualResult.Should().BeEquivalentTo(storeTypeDto);
            storeTypeRepository.Verify(x => x.GetByIdAsync(storeTypeDto.Id), Times.Once);
        }
    }
}