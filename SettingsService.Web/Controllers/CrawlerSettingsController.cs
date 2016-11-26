using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SettingsService.Web.Models;

namespace SettingsService.Web.Controllers
{
    public class CrawlerSettingsController : Controller
    {
        public ActionResult Index()
        {
            var model = new BaseViewModel { PageCaption = "Crawler Settings" };
            return View(model);
        }
    }
}