using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using Lab07.UnitTesting.DAL.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Lab07.UnitTesting.DAL.Models;

namespace Lab07.UnitTesting.DAL.Core.Context
{
    [ExcludeFromCodeCoverage]
    public class GodelBenefitContextInitializer : CreateDatabaseIfNotExists<GodelBenefitContext>
    {
        protected override void Seed(GodelBenefitContext context)
        {
            var roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context));
            roleManager.Create(new ApplicationRole { Name = UserRoles.Admin });
            roleManager.Create(new ApplicationRole { Name = UserRoles.User });

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var user = new ApplicationUser { Email = "admin@godeltech.com", UserName = UserRoles.Admin };
            userManager.Create(user, "!13InsAdmin");

            var admin = userManager.FindByEmail(user.Email);
            userManager.AddToRole(admin.Id, UserRoles.Admin);
            userManager.AddToRole(admin.Id, UserRoles.User);

            var simpleUser = new ApplicationUser { Email = "adaska@godeltech.com", UserName = UserRoles.User };
            userManager.Create(simpleUser, "!13InsAdaska");
            var simpleUserRole = userManager.FindByEmail(simpleUser.Email);
            userManager.AddToRole(simpleUserRole.Id, UserRoles.User);

            StoreType storeTypeBook = new StoreType { Name = "Book" };
            StoreType storeTypeFood = new StoreType { Name = "Food" };
            context.StoreTypes.Add(storeTypeBook);
            context.StoreTypes.Add(storeTypeFood);

            Store storeBook = new Store
            {
                Name = "oz.by",
                Promocode = "4GTE1",
                StoreType = storeTypeBook
            };

            Store storeFood = new Store
            {
                Name = "e-dostavka",
                Promocode = "4GTE1234",
                StoreType = storeTypeFood
            };

            context.Stores.Add(storeBook);
            context.Stores.Add(storeFood);

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
