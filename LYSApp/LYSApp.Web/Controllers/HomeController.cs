using LYSApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LYSApp.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Jobs()
        {
            return View();
        }
        public ActionResult TermsAndCondition()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ListYourProperty(ListYourPropertyViewModel model)
        {
            //var listYourPropertyViewModel = JsonConvert.DeserializeObject<ListYourPropertyViewModel>(model);
            return View();
        }
    }
}