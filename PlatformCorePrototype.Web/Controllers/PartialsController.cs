using System.Web.Mvc;

namespace PlatformCorePrototype.Web.Controllers
{
    public class PartialsController : Controller
    {
        public ActionResult TopNavbar()
        {
            return PartialView();
        }

        public ActionResult Sidebar()
        {
            return PartialView();
        }

        public ActionResult Offsidebar()
        {
            return PartialView();
        }

        public ActionResult Footer()
        {
            return PartialView();
        }

        public ActionResult OffsidebarTab1()
        {
            return PartialView();
        }

        public ActionResult OffsidebarTab2()
        {
            return PartialView();
        }
    }
}