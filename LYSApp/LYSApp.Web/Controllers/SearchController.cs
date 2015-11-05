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

       public JsonResult getArea(string term)
       {
           IList<SearchAreaViewModel> searchAreaList = searchManagement
           string result = JsonConvert.SerializeObject(searchAreaList); 
           return Json(result, JsonRequestBehavior.AllowGet);

       }
    }
}