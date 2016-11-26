using System.Web.Mvc;
using SettingsService.Web.Models;

namespace SettingsService.Web.Controllers
{
    public class GeneralSettingsController : Controller
    {
        public ActionResult Index()
        {
            var model = new BaseViewModel {PageCaption = "General Settings" };
            return View(model);
        }
    }
}
