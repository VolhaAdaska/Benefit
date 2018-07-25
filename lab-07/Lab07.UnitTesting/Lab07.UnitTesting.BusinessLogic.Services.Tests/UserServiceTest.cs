using System;
using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using FluentAssertions;
using Lab07.UnitTesting.BusinessLogic.Infrastructure;
using Lab07.UnitTesting.DAL.Interfaces;
using Lab07.UnitTesting.DAL.Models.Identity;
using Lab07.UnitTesting.DTO;
using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Lab07.UnitTesting.BusinessLogic.Services.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class UserServiceTest
    {
        private UserService userService;
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IMapper> mapper;
        private Mock<IUserStore<ApplicationUser>> userStore;
        private Mock<UserManager<ApplicationUser>> userManager;


        [SetUp]
        public void SetUp()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            mapper = new Mock<IMapper>();
            userStore = new Mock<IUserStore<ApplicationUser>>();
            userManager = new Mock<UserManager<ApplicationUser>>(userStore.Object);
            unitOfWork.Setup(x => x.UserManager).Returns(userManager.Object);

            userService = new UserService(unitOfWork.Object, mapper.Object);
        }

        [Test]
        public void UserService_When_UnitOfWork_Null_Should_ThrowArgumentNullException()
        {
            //assert
            Assert.Throws<ArgumentNullException>(() => new UserService(null, mapper.Object));
        }

        [Test]
        public void UserService_When_Mapper_Null_Should_ThrowArgumentNullException()
        {
            //assert
            Assert.Throws<ArgumentNullException>(() => new UserService(unitOfWork.Object, null));
        }

        [Test]
        public void AddUserAsync_When_Parameter_Null_Should_ThrowArgumentNullException()
        {
            //assert
            Assert.That(() => userService.AddUserAsync(null), Throws.ArgumentNullException);
        }

        [Test]
        public async Task AddUserAsync_When_User_NotNull_Should_OperationDetails()
        {
            //arrange
            var expectedResult = new OperationDetails(false, "A user with this e-mail already exists", "Email");

            UserDto userDto = new UserDto
            {
                Email = "test@godeltech.com"
            };

            ApplicationUser user = new ApplicationUser
            {
                Email = "test@godeltech.com"
            };

            userManager.Setup(x => x.FindByEmailAsync(userDto.Email)).ReturnsAsync(user);

            //act
            var actualResult = await userService.AddUserAsync(userDto);

            //assert
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task AddUserAsync_When_Not_Found_User_Should_Add_Correct_User()
        {
            //arrange
            var expectedResult = new OperationDetails(true, "Registration successful");

            UserDto userDto = new UserDto
            {
                Email = "test@godeltech.com"
            };

            ApplicationUser user = new ApplicationUser
            {
                Email = "test@godeltech.com"
            };

            mapper.Setup(x => x.Map<UserDto, ApplicationUser>(userDto)).Returns(user);

            //act
            var actualResult = await userService.AddUserAsync(userDto);

            //assert
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void CheckUserCredentialsAsync_When_Parameter_Null_Should_ThrowArgumentNullException()
        {
            Assert.That(() => userService.CheckUserCredentialsAsync(null), Throws.ArgumentNullException);
        }

        [Test]
        public async Task CheckUserCredentialsAsync_When_User_Null_Should_Return_Claim_Null()
        {
            //arrange
            ClaimsIdentity expectedResult = null;

            UserDto userDto = new UserDto
            {
                Email = "test@godeltech.com"
            };

            //act
            var actualResult = await userService.CheckUserCredentialsAsync(userDto);

            //assert
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task CheckUserCredentialsAsync_When_CheckPasswordAsync_False_Should_Return_Claim_Null()
        {
            //arrange
            ClaimsIdentity expectedResult = null;

            UserDto userDto = new UserDto
            {
                Email = "test@godeltech.com"
            };

            ApplicationUser user = new ApplicationUser
            {
                Email = "test@godeltech.com"
            };

            userManager.Setup(x => x.FindByEmailAsync(userDto.Email)).ReturnsAsync(user);

            //act
            var actualResult = await userService.CheckUserCredentialsAsync(userDto);

            //assert
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task CheckUserCredentialsAsync_When_Correct_Should_Claim()
        {
            //arrange
            UserDto userDto = new UserDto
            {
                Email = "test@godeltech.com",
                Password = "test"
            };

            ApplicationUser user = new ApplicationUser
            {
                Email = "test@godeltech.com"
            };

            userManager.Setup(x => x.FindByEmailAsync(userDto.Email)).ReturnsAsync(user);
            userManager.Setup(x => x.CheckPasswordAsync(user, userDto.Password)).ReturnsAsync(true);

            //act
            await userService.CheckUserCredentialsAsync(userDto);

            //assert
            userManager.Verify(x => x.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie), Times.Once);
        }
    }
}