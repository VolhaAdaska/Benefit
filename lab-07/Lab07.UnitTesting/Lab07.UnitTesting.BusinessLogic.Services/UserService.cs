using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Lab07.UnitTesting.DTO;
using Lab07.UnitTesting.BusinessLogic.Infrastructure;
using Lab07.UnitTesting.BusinessLogic.Interfaces;
using Lab07.UnitTesting.DAL.Models.Identity;
using Lab07.UnitTesting.DAL.Interfaces;
using Microsoft.AspNet.Identity;

namespace Lab07.UnitTesting.BusinessLogic.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public async Task<OperationDetails> AddUserAsync(UserDto userDto)
        {
            if (userDto == null)
            {
                throw new ArgumentNullException(nameof(userDto));
            }

            ApplicationUser user = await unitOfWork.UserManager.FindByEmailAsync(userDto.Email);

            if (user != null)
            {
                return new OperationDetails(false, "A user with this e-mail already exists", "Email");
            }

            user = mapper.Map<UserDto, ApplicationUser>(userDto);

            await unitOfWork.UserManager.CreateAsync(user, userDto.Password);
            await unitOfWork.UserManager.AddToRoleAsync(user.Id, userDto.Role);
            await unitOfWork.SaveAsync();

            return new OperationDetails(true, "Registration successful");
        }

        public async Task<ClaimsIdentity> CheckUserCredentialsAsync(UserDto userDto)
        {
            if (userDto == null)
            {
                throw new ArgumentNullException(nameof(userDto));
            }

            ClaimsIdentity claim = null;

            ApplicationUser user = await unitOfWork.UserManager.FindByEmailAsync(userDto.Email);

            if (user != null && await unitOfWork.UserManager.CheckPasswordAsync(user, userDto.Password))
            {
                claim = await unitOfWork.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            }
            return claim;
        }
    }
}