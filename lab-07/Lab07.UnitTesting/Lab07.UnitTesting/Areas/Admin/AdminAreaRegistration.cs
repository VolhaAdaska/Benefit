using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;

namespace Lab07.UnitTesting.Areas.Admin
{
    [ExcludeFromCodeCoverage]
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Admin";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}