using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using System.Web.Routing;

namespace Lab07.UnitTesting
{
    [ExcludeFromCodeCoverage]
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}