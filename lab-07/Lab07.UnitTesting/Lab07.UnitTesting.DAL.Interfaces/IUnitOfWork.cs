using Lab07.UnitTesting.DAL.Interfaces.Repositories;
using Lab07.UnitTesting.DAL.Models;
using Lab07.UnitTesting.DAL.Models.Identity;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;

namespace Lab07.UnitTesting.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        RoleManager<ApplicationRole> RoleManager { get; }

        UserManager<ApplicationUser> UserManager { get; }

        IStoreRepository StoreRepository { get; }

        IStoreTypeRepository StoreTypeRepository { get; }

        Task SaveAsync();
    }
}