using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LYSApp.Domain.StaticContentManagement;

namespace LYSApp.Web
{
    public class StaticContentConfig
    {
        public static void GetStaticContents()
        {
            StaticContentManagement staticContentManagement = new StaticContentManagement();
            //storing all Cities and Areas in Application variable
            HttpContext.Current.Application["Cities"] = staticContentManagement.GetAllCities();
            HttpContext.Current.Application["Areas"] = staticContentManagement.GetAllAreas();
        }

    }
}