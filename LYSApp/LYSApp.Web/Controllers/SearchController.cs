using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LYSApp.Web.Utilities;
using LYSApp.Domain.SearchManagement;
using LYSApp.Model;
using Newtonsoft.Json;

namespace LYSApp.Web.Controllers
{
    public class SearchController : Controller
    {
        
       private ISearchManagement searchManagement;

       public SearchController(SearchManagement searchManagement)
       {
           this.searchManagement = searchManagement;
       }

       public ActionResult Index()
        {
            return View();
        }

       public JsonResult GetArea(string term)
       {
           IList<SearchAreaViewModel> searchAreaList = searchManagement.GetAreas(term);
           string result = JsonConvert.SerializeObject(searchAreaList); 
           return Json(result, JsonRequestBehavior.AllowGet);

       }

       public void GetHouses(SearchViewModel searchViewModel)
       {
           searchManagement.GetHouses(searchViewModel);
       }

       public ActionResult PropertyDetails()
       {

           return View(searchManagement.GetPropertyDetails(2));//default value passed as 1 to avoid error as of now
       }
    }
}