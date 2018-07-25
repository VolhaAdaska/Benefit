using System;
using System.Data.Entity;
using Lab07.UnitTesting.DAL.Core.Context.Configurations;
using Lab07.UnitTesting.DAL.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Lab07.UnitTesting.DAL.Models.Identity;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace Lab07.UnitTesting.DAL.Core.Context
{
    [ExcludeFromCodeCoverage]
    public class GodelBenefitContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreType> StoreTypes { get; set; }

        public GodelBenefitContext()
         : base("GodelBenefitDatabase")
        {
            Database.SetInitializer(new GodelBenefitContextInitializer());
            Database.Initialize(false);

            Type providerService = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
        }

        public GodelBenefitContext(DbConnection connection)
            : base(connection, true)
        {
            Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer(new GodelBenefitContextInitializer());
            Database.Initialize(false);
        }

        public static GodelBenefitContext Create()
        {
            return new GodelBenefitContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new StoreConfigurations());
            modelBuilder.Configurations.Add(new StoreTypeConfigurations());

            base.OnModelCreating(modelBuilder);
        }
    }
}