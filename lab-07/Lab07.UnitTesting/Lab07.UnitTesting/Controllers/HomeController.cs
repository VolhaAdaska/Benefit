using System.Web.Mvc;

namespace Lab07.UnitTesting.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}