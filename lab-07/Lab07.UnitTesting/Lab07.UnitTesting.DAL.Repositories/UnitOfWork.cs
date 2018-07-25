using Lab07.UnitTesting.DAL.Core.Context;
using Lab07.UnitTesting.DAL.Models.Identity;
using Lab07.UnitTesting.DAL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Lab07.UnitTesting.DAL.Interfaces.Repositories;
using System.Threading.Tasks;
using System;
using System.Diagnostics.CodeAnalysis;
using Autofac;

namespace Lab07.UnitTesting.DAL.Repositories
{
    [ExcludeFromCodeCoverage]
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GodelBenefitContext context;
        private readonly ILifetimeScope lifetimeScope;
        private UserManager<ApplicationUser> userManager;
        private RoleManager<ApplicationRole> roleManager;

        public UnitOfWork(GodelBenefitContext context, ILifetimeScope lifetimeScope)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.lifetimeScope = lifetimeScope ?? throw new ArgumentNullException(nameof(lifetimeScope));
        }

        public RoleManager<ApplicationRole> RoleManager
        {
            get => roleManager ??
                   (roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context)));
        }

        public UserManager<ApplicationUser> UserManager
        {
            get => userManager ??
                   (userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context)));
        }

        public IStoreRepository StoreRepository => GetRepository<IStoreRepository>();

        public IStoreTypeRepository StoreTypeRepository => GetRepository<IStoreTypeRepository>();


        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        private T GetRepository<T>() where T : class
        {
            T repository = lifetimeScope.Resolve<T>();
            return repository;
        }

        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                context.Dispose();
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}