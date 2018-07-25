using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Lab07.UnitTesting.BusinessLogic.Interfaces;
using Microsoft.Owin.Security;
using System.Web.Mvc;
using AutoMapper;
using Lab07.UnitTesting.DTO;
using Lab07.UnitTesting.BusinessLogic.Infrastructure;
using Lab07.UnitTesting.Models;
using Lab07.UnitTesting.DAL.Core;

namespace Lab07.UnitTesting.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;
        private readonly IAuthenticationManager authManager;
        private readonly IMapper mapper;

        public AccountController(IUserService userService, IAuthenticationManager authManager, IMapper mapper)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.authManager = authManager ?? throw new ArgumentNullException(nameof(authManager));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            UserDto userDto = mapper.Map<LoginModel, UserDto>(model);
            ClaimsIdentity claim = await userService.CheckUserCredentialsAsync(userDto);
            if (claim == null)
            {
                ModelState.AddModelError(string.Empty, "Wrong email or password");
            }
            else
            {
                authManager.SignOut();
                authManager.SignIn(new AuthenticationProperties { IsPersistent = true }, claim);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            authManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            UserDto userDto = mapper.Map<RegisterModel, UserDto>(model);
            userDto.Role = UserRoles.User;

            OperationDetails operationDetails = await userService.AddUserAsync(userDto);
            if (operationDetails.Succedeed)
            {
                return View("SuccessRegister");
            }
            ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            return View(model);
        }
    }
}