using System.Security.Claims;
using System.Threading.Tasks;
using Lab07.UnitTesting.DTO;
using Lab07.UnitTesting.BusinessLogic.Infrastructure;
using System;

namespace Lab07.UnitTesting.BusinessLogic.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> AddUserAsync(UserDto userDto);

        Task<ClaimsIdentity> CheckUserCredentialsAsync(UserDto userDto);
    }
}