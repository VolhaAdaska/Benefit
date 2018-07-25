using System;
using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Lab07.UnitTesting.BusinessLogic.Infrastructure;
using Lab07.UnitTesting.BusinessLogic.Interfaces;
using Lab07.UnitTesting.Controllers;
using Lab07.UnitTesting.DTO;
using Lab07.UnitTesting.Models;
using Microsoft.Owin.Security;
using Moq;
using NUnit.Framework;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Lab07.UnitTesting.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class AccountControllerTest
    {
        private AccountController accountController;
        private Mock<IUserService> userService;
        private Mock<IAuthenticationManager> authManager;
        private Mock<IMapper> mapper;
        private UserDto userDto;
        private LoginModel loginModel;
        private RegisterModel registerModel;

        private static UserDto CreateUserDto()
        {
            return new UserDto
            {
                Email = "test@godeltech.com",
                Password = "test",
                UserName = "test name",
                Role = "test Role"
            };
        }

        private static LoginModel CreateLoginModel()
        {
            return new LoginModel()
            {
                Email = "test@godeltech.com",
                Password = "test"
            };
        }

        private static RegisterModel CreateRegisterModel()
        {
            return new RegisterModel()
            {
                Email = "test@godeltech.com",
                Password = "test",
                ConfirmPassword = "test",
                UserName = "test name"
            };
        }

        [SetUp]
        public void SetUp()
        {
            userService = new Mock<IUserService>();
            authManager = new Mock<IAuthenticationManager>();
            mapper = new Mock<IMapper>();
            userDto = CreateUserDto();
            loginModel = CreateLoginModel();
            registerModel = CreateRegisterModel();
            mapper.Setup(x => x.Map<LoginModel, UserDto>(loginModel)).Returns(userDto);
            mapper.Setup(x => x.Map<RegisterModel, UserDto>(registerModel)).Returns(userDto);

            accountController = new AccountController(userService.Object, authManager.Object, mapper.Object);
        }

        [Test]
        public void AccountController_When_UserService_Null_Should_ThrowArgumentNullException()
        {
            //assert
            Assert.Throws<ArgumentNullException>(() => new AccountController(null, authManager.Object, mapper.Object));
        }

        [Test]
        public void AccountController_When_AuthManager_Null_Should_ThrowArgumentNullException()
        {
            //assert
            Assert.Throws<ArgumentNullException>(() => new AccountController(userService.Object, null, mapper.Object));
        }

        [Test]
        public void AccountController_When_Mapper_Null_Should_ThrowArgumentNullException()
        {
            //assert
            Assert.Throws<ArgumentNullException>(() => new AccountController(userService.Object, authManager.Object, null));
        }

        [Test]
        public void Login_Get()
        {
            //act
            var result = accountController.Login() as ViewResult;

            //assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Login_Post_When_Model_Not_Valid_Should_Return_View()
        {
            //arrange
            accountController.ModelState.AddModelError("key", "error message");

            //act
            var actualResult = accountController.Login(loginModel);

            //assert
            Assert.IsNotNull(actualResult);
        }

        [Test]
        public async Task Login_Post_When_Claim_Null_Should_Return_WrongEmailOrPassword()
        {
            //act
            var actualResult = await accountController.Login(loginModel);

            //assert
            Assert.IsNotNull(actualResult);
            Assert.IsFalse(accountController.ModelState.IsValid);
        }

        [Test]
        public async Task Login_Post_When_Correct_Login_Should_RedirectToHomePage()
        {
            //arrange
            userService.Setup(x => x.CheckUserCredentialsAsync(userDto)).ReturnsAsync(new ClaimsIdentity());

            //act
            var actualResult = await accountController.Login(loginModel) as RedirectToRouteResult;

            //assert
            Assert.IsNotNull(actualResult);
            authManager.Verify(x => x.SignOut(), Times.Once);
            Assert.That(actualResult.RouteValues["action"], Is.EqualTo("Index"));
            Assert.That(actualResult.RouteValues["Controller"], Is.EqualTo("Home"));
        }

        [Test]
        public void Logout()
        {
            //act
            var actualResult = accountController.Logout() as RedirectToRouteResult;

            //assert
            authManager.Verify(x => x.SignOut(), Times.Once);
            Assert.That(actualResult.RouteValues["action"], Is.EqualTo("Index"));
            Assert.That(actualResult.RouteValues["Controller"], Is.EqualTo("Home"));
        }

        [Test]
        public void Register_Get()
        {
            //act
            var expectedResult = accountController.Register() as ViewResult;

            //assert
            Assert.IsNotNull(expectedResult);
        }

        [Test]
        public void Register_Post_When_Model_Not_Valid_Should_Return_View()
        {
            //arrange
            accountController.ModelState.AddModelError("key", "error message");

            //act
            var actualResult = accountController.Register(registerModel);

            //assert
            Assert.IsNotNull(actualResult);
        }

        [Test]
        public async Task Register_When_Not_Success_Register_Should_Return_View_LoginModel()
        {
            //arrange
            OperationDetails operationDetails = new OperationDetails(false);
            userService.Setup(x => x.AddUserAsync(userDto)).ReturnsAsync(operationDetails);

            //act
            var actualResult = await accountController.Register(registerModel);

            //assert
            Assert.IsNotNull(actualResult);
            Assert.IsFalse(accountController.ModelState.IsValid);
        }

        [Test]
        public async Task Register_When_Success_Register_Should_Return_View_SuccessRegister()
        {
            //arrange
            OperationDetails operationDetails = new OperationDetails(true);
            userService.Setup(x => x.AddUserAsync(userDto)).ReturnsAsync(operationDetails);

            //act
            var actualResult = await accountController.Register(registerModel) as ViewResult;

            //assert
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(actualResult.ViewName, "SuccessRegister");
        }
    }
}