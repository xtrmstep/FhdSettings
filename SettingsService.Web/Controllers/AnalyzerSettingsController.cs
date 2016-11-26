using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SettingsService.Web.Types;

namespace SettingsService.Web.Controllers
{
    public class AnalyzerSettingsController : Controller
    {
        public AnalyzerSettingsController()
        {
            ViewBag.PageCaption = "Analyzer Settings";
            ViewBag.Section = SiteSections.AnalyzerRules;
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}