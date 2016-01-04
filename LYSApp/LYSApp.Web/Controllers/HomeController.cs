using LYSApp.Domain.OwnerPropertyListingRequestManagement;
using LYSApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LYSApp.Domain.NotificationManagement;
using LYSApp.Model.ViewModel;
using System.Text;

namespace LYSApp.Web.Controllers
{    
    public class HomeController : Controller
    {
        private IOwnerPropertyListingRequestManagement ownerPropertyListingRequestManagement;
        private IMandrillMailer mandrillMailer;
        public HomeController(OwnerPropertyListingRequestManagement ownerPropertyListingRequestManagement,MandrillMailer mandrillMailer)
        {
            this.ownerPropertyListingRequestManagement = ownerPropertyListingRequestManagement;
            this.mandrillMailer = mandrillMailer;
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

        [HttpPost]
        public ActionResult Contact(ContactViewModel model)
        {
            //Thank you mailer for user
            mandrillMailer.NotifyUser(model.Email,model.Name, "Thank you for contacting LockYourStay.", "Thank you for contacting Lockyourstay.We will get back to you at earliest.", "Notify User");
            //send mail to admin
            string body = model.Name + " has contacted you. Please find the details below, <br><br> <table><tr><td>Name : </td><td>" + model.Name + "</td></tr><tr><td>Email : </td><td>" + model.Email + "</td></tr><tr><td>Subject : </td><td>" + model.Subject + "</td></tr><tr><td>Message : </td><td>" + model.Message + "</td></tr></table>";
            mandrillMailer.NotifySuperAdmin(model.Email,model.Name + " have contacted LockYourStay", body, "Notify Admin");

            return Content("Thank you for contacting LockYourStay.");
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
            
                //send user notification
                mandrillMailer.NotifyUser(model.Email, model.FirstName + " " + model.LastName, "Thank you for choosing Lockyoustay for listing your property", "We have received request for listing your property.We have forwarded your request to our concern team and will update you soon.", "Notify User");
                //send admin notification
                string body = model.FirstName + " " + model.LastName + " has contacted you for listing property.Please find the details below,<br><br> <table><tr><td>Email: </td><td>"+model.Email+"</td></tr><tr><td>Mobile: </td><td>"+model.Mobile+"</td></tr><tr><td>Address: </td><td>"+model.Address+"</td></tr></table>";
                mandrillMailer.NotifySuperAdmin(model.Email,model.Email+ " have contacted you for listing his/her property",body,"Notify Admin");

                if (ownerPropertyListingRequestManagement.AddOwnerPropertyListingRequest(model) > 0)
                {

                    return Content("Thank you for reaching us. We'll get back to you soon.");
                }
                else
                {
                    return Content("Something went wrong! Please contact support@lockyourstay.com.");
                }
                
           
                
           
           
        }
    }
}