using System.Web.Mvc;
using SettingsService.Web.Models;

namespace SettingsService.Web.Controllers
{
    public class GeneralSettingsController : Controller
    {
        public GeneralSettingsController()
        {
            ViewBag.PageCaption = "General Settings";
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}
