using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LYSApp.Web.Utilities;
using LYSApp.Domain.SearchManagement;
using LYSApp.Model;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace LYSApp.Web.Controllers
{
    public class SearchController : Controller
    {
        
       private ISearchManagement searchManagement;

       public SearchController(SearchManagement searchManagement)
       {
           this.searchManagement = searchManagement;
       }

       // [HttpPost]
       // [ChildActionOnly]
       //public ActionResult Index()
       // {
       //     return View();
       // }

       public JsonResult GetArea(string term)
       {
           IList<SearchAreaViewModel> searchAreaList = searchManagement.GetAreas(term);
           string result = JsonConvert.SerializeObject(searchAreaList); 
           return Json(result, JsonRequestBehavior.AllowGet);

       }
        
       public ActionResult GetPGs(SearchViewModel searchViewModel)
       {
           if (searchViewModel !=null)
           {
               Session["SearchCriteria"] = searchViewModel;
               return View("Index", searchManagement.GetPGsBySearchCriteria(searchViewModel));
           }
           else
           {
               return RedirectToAction("Index", "Account");
           }
       }
      
      public ActionResult PropertyDetails(int PGID)
       {
           if (Session["SearchCriteria"] != null)
           {
               return View(searchManagement.GetPropertyDetails(PGID, Session["SearchCriteria"] as SearchViewModel));

           }
           else
           {
               return RedirectToAction("Index","Account");
           }
       }


      public ActionResult ReviewBooking(int RoomID)
      {
          BookingDetailsViewModel bookingDetailsViewModel = searchManagement.GetBookingDetails(RoomID);
          //BookingDetailsViewModel.User = LYSApp.Web.Services.SessionManager.GetSessionUser();
          return View(bookingDetailsViewModel);
      }
    }
}