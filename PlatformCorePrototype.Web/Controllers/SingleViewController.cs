using System.Web.Mvc;

namespace PlatformCorePrototype.Web.Controllers
{
    public class SingleViewController : Controller
    {
        // GET: SingleView
        public ActionResult Index()
        {
            return View();
        }

        // GET: SingleView
        public ActionResult MenuView()
        {
            return View();
        }
    }
}