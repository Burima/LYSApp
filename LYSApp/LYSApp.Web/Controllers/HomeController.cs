using LYSApp.Domain.OwnerPropertyListingRequestManagement;
using LYSApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LYSApp.Domain.NotificationManagement;

namespace LYSApp.Web.Controllers
{
    public delegate void delegateOwnerPropertyListingRequest(OwnerPropertyListingRequestViewModel model);
    public class HomeController : Controller
    {
        private IOwnerPropertyListingRequestManagement ownerPropertyListingRequestManagement;
        public HomeController(OwnerPropertyListingRequestManagement ownerPropertyListingRequestManagement)
        {
            this.ownerPropertyListingRequestManagement = ownerPropertyListingRequestManagement;
        }
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
        public ActionResult ListYourProperty(OwnerPropertyListingRequestViewModel model)
        {            
            try
            {
                OwnerPropertyListingRequestMailer mailer = new OwnerPropertyListingRequestMailer();
                delegateOwnerPropertyListingRequest del = new delegateOwnerPropertyListingRequest(mailer.NotifyUser);
                del += mailer.NotifySuperAdmin;
                del += ownerPropertyListingRequestManagement.AddOwnerPropertyListingRequest;
                return Content("SUCCESS");
                
            }
            catch (Exception ex)
            {
                return Content("ERROR");
            }
           
        }
    }
}