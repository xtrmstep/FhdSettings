using System.Web.Mvc;
using SettingsService.Web.Types;

namespace SettingsService.Web.Controllers
{
    public class GeneralSettingsController : Controller
    {
        public GeneralSettingsController()
        {
            ViewBag.PageCaption = "General Settings";
            ViewBag.Section = SiteSections.GereralSections;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details()
        {
            ViewBag.PageSubCaption = "Details";
            return View();
        }
    }
}
