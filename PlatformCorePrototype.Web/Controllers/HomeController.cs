using System.Web.Mvc;

namespace PlatformCorePrototype.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Scratch()
        {
            return View();
        }
    }
}